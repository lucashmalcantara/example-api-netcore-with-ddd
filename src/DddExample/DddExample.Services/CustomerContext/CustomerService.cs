using DddExample.Domain.Core.Results;
using DddExample.Domain.CustomerContext.Entities;
using DddExample.Domain.CustomerContext.Repositories;
using DddExample.Domain.CustomerContext.Services;
using DddExample.Domain.CustomerContext.Validators;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DddExample.Domain.Core.Results.Extensions;

namespace DddExample.Services.CustomerContext
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(
            ILogger<CustomerService> logger,
            ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task<SimpleResult> CreateAsync(Customer customer)
        {
            var customerValidator = GetCustomerValidator();
            var validationResult = customerValidator.Validate(customer);

            if (!validationResult.IsValid)
                return validationResult.Errors.ToErrorResult();

            var createResult = await _customerRepository.CreateAsync(customer);

            if (createResult.HasError)
                return SimpleResult.Error(createResult.Errors);

            return await Task.FromResult(SimpleResult.Ok());
        }

        //TODO Change to factory pattern.
        private CustomerValidator GetCustomerValidator()
        {
            var personNameValidator = new PersonNameValidator();
            var cpfValidator = new CpfValidator();
            var emailValidator = new EmailValidator();

            var customerValidator = new CustomerValidator(
                personNameValidator, cpfValidator, emailValidator);

            return customerValidator;
        }
    }
}
