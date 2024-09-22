using System.Collections.Generic;
using System.Threading.Tasks;
using teste_atak.Domain.Entities;

namespace teste_atak.Domain.Contracts
{
    public interface ICustomerRepository
    {
        Task<(IEnumerable<Customer> Items, int TotalCount, int TotalPages)> GetAll(
            string? name,
            string? phone,
            string? sortBy,
            bool sortDescending,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Customer?> GetById(string id);
        Task Insert(Customer customer);
        Task InsertRange(IEnumerable<Customer> customers);
        Task<IEnumerable<Customer>> GetByMultipleIds(IEnumerable<string> ids);
    }
}
