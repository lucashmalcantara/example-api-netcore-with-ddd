using DddExample.Domain.Core;
using DddExample.Domain.CustomerContext.Entities;
using System.Threading.Tasks;

namespace DddExample.Domain.CustomerContext.Services
{
    public interface ICustomerService
    {
        Task<SimpleResult> CreateAsync(Customer customer);
    }
}
