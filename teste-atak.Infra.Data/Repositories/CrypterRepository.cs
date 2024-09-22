using BCrypt.Net;
using System.Threading.Tasks;
using teste_atak.Domain.Contracts;

namespace teste_atak.Infra.Data.Repositories
{
    public class CrypterRepository : ICrypterRepository
    {
        public Task<string> Encrypt(string plainText)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainText);
            return Task.FromResult(hashedPassword);
        }

        public Task<bool> Compare(string plainText, string encryptedText)
        {
            var isMatch = BCrypt.Net.BCrypt.Verify(plainText, encryptedText);
            return Task.FromResult(isMatch);
        }
    }
}
