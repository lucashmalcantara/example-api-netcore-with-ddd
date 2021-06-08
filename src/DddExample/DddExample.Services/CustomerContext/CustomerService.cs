using DddExample.Domain.Core;
using DddExample.Domain.CustomerContext.Entities;
using DddExample.Domain.CustomerContext.Services;
using System;
using System.Threading.Tasks;

namespace DddExample.Services.CustomerContext
{
    public class CustomerService : ICustomerService
    {
        public Task<Result<Customer>> CreateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
