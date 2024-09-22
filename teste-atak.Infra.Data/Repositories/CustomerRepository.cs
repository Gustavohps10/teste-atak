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

        public async Task<(IEnumerable<Customer> Items, int TotalCount, int TotalPages)> GetAll(
            string? name,
            string? phone,
            string? sortBy,
            bool sortDescending,
            int pageNumber,
            int pageSize)
        {
            var customersQuery = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                customersQuery = customersQuery.Where(c => c.Name.ToLower().Contains(name.ToLower()));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                customersQuery = customersQuery.Where(c => c.Phone.ToLower().Contains(phone.ToLower()));
            }

            sortBy = sortBy?.ToLower();

            if (sortBy != null && sortBy != "name" && sortBy != "phone")
            {
                throw new ArgumentException($"Campo de ordenação '{sortBy}' inválido. Os campos permitidos são: Name, Phone.");
            }

            if (sortBy == "name")
            {
                return await Paginate(
                    sortDescending
                        ? customersQuery.OrderByDescending(c => c.Name)
                        : customersQuery.OrderBy(c => c.Name),
                    pageNumber,
                    pageSize);
            }

            if (sortBy == "phone")
            {
                return await Paginate(
                    sortDescending
                        ? customersQuery.OrderByDescending(c => c.Phone)
                        : customersQuery.OrderBy(c => c.Phone),
                    pageNumber,
                    pageSize);
            }

            return await Paginate(
                sortDescending
                    ? customersQuery.OrderByDescending(c => c.CreatedAt)
                    : customersQuery.OrderBy(c => c.CreatedAt),
                pageNumber,
                pageSize);
        }

        public async Task<Customer?> GetById(string id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task Insert(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task InsertRange(IEnumerable<Customer> customers)
        {
            await _context.Customers.AddRangeAsync(customers);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetByMultipleIds(IEnumerable<string> ids)
        {
            return await _context.Customers.Where(c => ids.Contains(c.Id)).ToListAsync();
        }

        private async Task<(IEnumerable<Customer> Items, int TotalCount, int TotalPages)> Paginate(IQueryable<Customer> query, int pageNumber, int pageSize)
        {
            var totalCount = await query.CountAsync();
            var totalPages = (int)System.Math.Ceiling((double)totalCount / pageSize);
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (items, totalCount, totalPages);
        }
    }
}
