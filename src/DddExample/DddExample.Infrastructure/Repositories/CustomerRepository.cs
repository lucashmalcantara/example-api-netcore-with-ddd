using DddExample.Domain.Core.Results;
using DddExample.Domain.CustomerContext.Entities;
using DddExample.Domain.CustomerContext.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DddExample.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly IList<Customer> _customerDatabase;

        public CustomerRepository(
            ILogger<CustomerRepository> logger,
            IList<Customer> customerDatabase)
        {
            _logger = logger;
            _customerDatabase = customerDatabase;
        }

        public async Task<SimpleResult> CreateAsync(Customer cliente)
        {
            _customerDatabase.Add(cliente);
            return await Task.FromResult(SimpleResult.Ok());
        }

        public async Task<Result<IList<Customer>>> GetAllAsync()
        {
            var result = Result<IList<Customer>>.Ok(_customerDatabase);
            return await Task.FromResult(result);
        }

        public async Task<Result<Customer>> GetByIdAsync(Guid id)
        {
            var customer = _customerDatabase.FirstOrDefault(c => c.Id == id);
            var result = Result<Customer>.Ok(customer);
            return await Task.FromResult(result);
        }
    }
}
