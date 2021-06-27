using DddExample.Domain.Core.Results;
using DddExample.Domain.Core.Results.Extensions;
using DddExample.Domain.CustomerContext.Entities;
using DddExample.Domain.CustomerContext.Repositories;
using DddExample.Domain.CustomerContext.Services;
using FluentValidation;
using System.Threading.Tasks;

namespace DddExample.Services.CustomerContext
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IValidator<Customer> _customerValidator;

        public CustomerService(
            ICustomerRepository customerRepository,
            IValidator<Customer> customerValidator)
        {
            _customerRepository = customerRepository;
            _customerValidator = customerValidator;
        }

        public async Task<SimpleResult> CreateAsync(Customer customer)
        {
            var validationResult = _customerValidator.Validate(customer);

            if (!validationResult.IsValid)
                return validationResult.Errors.ToErrorResult();

            var createResult = await _customerRepository.CreateAsync(customer);

            if (createResult.HasError)
                return SimpleResult.Error(createResult.Errors);

            return await Task.FromResult(SimpleResult.Ok());
        }
    }
}
