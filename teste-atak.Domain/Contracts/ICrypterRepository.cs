using System.Threading.Tasks;

namespace teste_atak.Domain.Contracts
{
    public interface ICrypterRepository
    {
        Task<string> EncryptAsync(string plainText);
        Task<bool> CompareAsync(string plainText, string encryptedText);
    }
}
