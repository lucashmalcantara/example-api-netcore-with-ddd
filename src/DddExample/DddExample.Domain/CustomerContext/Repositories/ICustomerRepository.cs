using DddExample.Domain.Core.Results;
using DddExample.Domain.CustomerContext.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DddExample.Domain.CustomerContext.Repositories
{
    public interface ICustomerRepository
    {
        Task<SimpleResult> CreateAsync(Customer cliente);
        Task<Result<Customer>> GetByIdAsync(Guid id);
        Task<Result<IList<Customer>>> GetAllAsync();
    }
}
