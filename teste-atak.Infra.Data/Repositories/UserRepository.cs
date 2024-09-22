using teste_atak.Domain.Entities;
using teste_atak.Domain.Contracts;
using teste_atak.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace teste_atak.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(string id)
        {
            User? user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }
            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            User? user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }
            return user;
        }

        public async Task Insert(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
