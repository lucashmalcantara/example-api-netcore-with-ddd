using AutoMapper;
using DddExample.Api.Constants;
using DddExample.Api.V1.Mapping;
using DddExample.Api.V1.Models.CustomerContext;
using DddExample.Domain.Core.Results;
using DddExample.Domain.CustomerContext.Entities;
using DddExample.Domain.CustomerContext.Repositories;
using DddExample.Domain.CustomerContext.Services;
using DddExample.IntegrationTests.Base;
using DddExample.IntegrationTests.Builders.V1.Models.CustomerContext;
using DddExample.IntegrationTests.Container;
using DddExample.Tests.Shared.Builders.Domain.CustomerContext.Entities;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DddExample.IntegrationTests.Tests.Api.V1.Controllers
{
    public class CustomersControllerTests : ControllerTestBase
    {
        #region GET by Id
        [Test]
        public async Task GetAsync_should_return_200Ok_when_found_a_customer_with_equivalent_Id()
        {
            // Arrange
            var customerBuilder = new CustomerBuilder();
            var customer = customerBuilder.Generate();

            var customerRepository = FakeServiceProvider.Instance.GetOrAddIfNotExists<ICustomerRepository>();

            A.CallTo(() => customerRepository.GetByIdAsync(A<Guid>.Ignored))
                .Returns(Result<Customer>.Ok(customer));

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CustomerMapProfile>())
                .CreateMapper();

            var expectedCustomerModel = mapper.Map<CustomerGetResultModel>(customer);

            var customerId = Guid.NewGuid();

            // Act
            var response = await HttpClient.GetAsync($"{ApplicationConstants.ApplicationPathBase}/v1/customers/{customerId}");

            // Assert
            response.Should().Be200Ok()
                .And.BeAs(expectedCustomerModel);
        }

        [Test]
        public async Task GetAsync_should_return_204NoContent_when_there_is_no_Customer_with_equivalent_Id()
        {
            // Arrange
            var customerRepository = FakeServiceProvider.Instance.GetOrAddIfNotExists<ICustomerRepository>();

            A.CallTo(() => customerRepository.GetByIdAsync(A<Guid>.Ignored))
                .Returns(Result<Customer>.Ok(null));

            var customerId = Guid.NewGuid();

            // Act
            var response = await HttpClient.GetAsync($"{ApplicationConstants.ApplicationPathBase}/v1/customers/{customerId}");

            // Assert
            response.Should().Be204NoContent();
        }

        [Test]
        public async Task GetAsync_should_return_400BadRequest_when_the_repository_returns_any_error()
        {
            // Arrange
            var customerRepository = FakeServiceProvider.Instance.GetOrAddIfNotExists<ICustomerRepository>();

            var someError = new Error("Some error");
            var errorResult = Result<Customer>.Error(someError);

            A.CallTo(() => customerRepository.GetByIdAsync(A<Guid>.Ignored))
                .Returns(errorResult);

            var expectedErrorList = errorResult.Errors;

            var customerId = Guid.NewGuid();

            // Act
            var response = await HttpClient.GetAsync($"{ApplicationConstants.ApplicationPathBase}/v1/customers/{customerId}");

            // Assert
            response.Should().Be400BadRequest()
                .And.BeAs(expectedErrorList);
        }
        #endregion

        #region GET All
        [Test]
        public async Task GetAllAsync_should_return_200Ok_when_called_correctly()
        {
            // Arrange
            var customerBuilder = new CustomerBuilder();
            var customers = customerBuilder.Generate(count: 10);

            var customerRepository = FakeServiceProvider.Instance.GetOrAddIfNotExists<ICustomerRepository>();

            A.CallTo(() => customerRepository.GetAllAsync())
                .Returns(Result<IList<Customer>>.Ok(customers));

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<CustomerMapProfile>())
                .CreateMapper();

            var expectedCustomerListModel = mapper.Map<IEnumerable<CustomerGetResultModel>>(customers);

            // Act
            var response = await HttpClient.GetAsync($"{ApplicationConstants.ApplicationPathBase}/v1/customers");

            // Assert
            response.Should().Be200Ok()
                .And.BeAs(expectedCustomerListModel);
        }

        [Test]
        public async Task GetAllAsync_should_return_204NoContent_when_repository_returns_an_empty_Customer_list()
        {
            // Arrange
            var customerRepository = FakeServiceProvider.Instance.GetOrAddIfNotExists<ICustomerRepository>();

            var emptyCustomerList = new List<Customer>();

            A.CallTo(() => customerRepository.GetAllAsync())
                .Returns(Result<IList<Customer>>.Ok(emptyCustomerList));

            // Act
            var response = await HttpClient.GetAsync($"{ApplicationConstants.ApplicationPathBase}/v1/customers");

            // Assert
            response.Should().Be204NoContent();
        }

        [Test]
        public async Task GetAllAsync_should_return_204NoContent_when_repository_returns_null()
        {
            // Arrange
            var customerRepository = FakeServiceProvider.Instance.GetOrAddIfNotExists<ICustomerRepository>();

            A.CallTo(() => customerRepository.GetAllAsync())
                .Returns(Result<IList<Customer>>.Ok(null));

            // Act
            var response = await HttpClient.GetAsync($"{ApplicationConstants.ApplicationPathBase}/v1/customers");

            // Assert
            response.Should().Be204NoContent();
        }

        [Test]
        public async Task GetAllAsync_should_return_400BadRequest_when_the_repository_returns_any_error()
        {
            // Arrange
            var customerRepository = FakeServiceProvider.Instance.GetOrAddIfNotExists<ICustomerRepository>();

            var someError = new Error("Some error");
            var errorResult = Result<IList<Customer>>.Error(someError);

            A.CallTo(() => customerRepository.GetAllAsync())
                .Returns(errorResult);

            var expectedErrorList = errorResult.Errors;

            var customerId = Guid.NewGuid();

            // Act
            var response = await HttpClient.GetAsync($"{ApplicationConstants.ApplicationPathBase}/v1/customers");

            // Assert
            response.Should().Be400BadRequest()
                .And.BeAs(expectedErrorList);
        }
        #endregion

        #region POST
        [Test]
        public async Task PostAsync_should_return_201Created_when_all_parameters_are_valid()
        {
            // Arrange
            var customerService = FakeServiceProvider.Instance.GetOrAddIfNotExists<ICustomerService>();

            A.CallTo(() => customerService.CreateAsync(A<Customer>.Ignored))
                .Returns(SimpleResult.Ok());

            var customerModelBuilder = new CustomerPostRequestModelBuilder();
            var customerModel = customerModelBuilder.Generate();

            var body = JsonSerializer.Serialize(customerModel);
            var requestContent = new StringContent(body, Encoding.UTF8, "application/json");

            // Act
            var response = await HttpClient.PostAsync($"{ApplicationConstants.ApplicationPathBase}/v1/customers", requestContent);
            var content = await response.Content.ReadAsStringAsync();
            var customerGetResultModel = JsonSerializer.Deserialize<CustomerPostRequestModel>(content);

            // Assert
            response.Should().Be201Created()
                .And.HaveHeader("Location");

            customerGetResultModel.Should().NotBeNull();
        }

        [Test]
        public async Task PostAsync_should_return_400BadRequest_when_the_service_returns_any_error()
        {
            // Arrange
            var customerService = FakeServiceProvider.Instance.GetOrAddIfNotExists<ICustomerService>();

            var someError = new Error("Some error");
            var errorResult = SimpleResult.Error(someError);

            A.CallTo(() => customerService.CreateAsync(A<Customer>.Ignored))
                .Returns(errorResult);

            var customerModelBuilder = new CustomerPostRequestModelBuilder();
            var customerModel = customerModelBuilder.Generate();

            var body = JsonSerializer.Serialize(customerModel);
            var requestContent = new StringContent(body, Encoding.UTF8, "application/json");

            var expectedErrorList = errorResult.Errors;

            // Act
            var response = await HttpClient.PostAsync($"{ApplicationConstants.ApplicationPathBase}/v1/customers", requestContent);

            // Assert
            response.Should().Be400BadRequest()
                .And.BeAs(expectedErrorList);
        }
        #endregion
    }
}
