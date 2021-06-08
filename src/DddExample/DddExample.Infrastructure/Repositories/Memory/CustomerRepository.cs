using DddExample.Domain.Core;
using DddExample.Domain.CustomerContext.Entities;
using DddExample.Domain.CustomerContext.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DddExample.Infrastructure.Repositories.Memory
{
    public class CustomerRepository : ICustomerRepository
    {
        public Task<SimpleResult> CreateAsync(Customer cliente)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<Customer>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<Customer>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
