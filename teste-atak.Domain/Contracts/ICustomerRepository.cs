using System.Collections.Generic;
using System.Threading.Tasks;
using teste_atak.Domain.Entities;

namespace teste_atak.Domain.Contracts
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetById(string id);
        Task Insert(Customer customer);
        Task InserRange(IEnumerable<Customer> customers);
        Task<IEnumerable<Customer>> GetByMultipleIds(IEnumerable<string> ids);
    }
}
