using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teste_atak.Domain.Entities;

namespace teste_atak.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(string id);
        Task<User?> GetByEmail(string email);
        Task Insert(User user);
    }
}
