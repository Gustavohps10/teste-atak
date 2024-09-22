using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teste_atak.Domain.Entities;
using teste_atak.Domain.Contracts;
using teste_atak.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace teste_atak.Infra.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(string id)
        {
            Customer? customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }
            return customer;
        }

        public async Task Insert(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetByIds(IEnumerable<string> ids)
        {
            return await _context.Customers.Where(c => ids.Contains(c.Id)).ToListAsync();
        }
    }
}
