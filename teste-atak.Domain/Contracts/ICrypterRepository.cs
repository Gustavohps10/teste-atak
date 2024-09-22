using System.Threading.Tasks;

namespace teste_atak.Domain.Contracts
{
    public interface ICrypterRepository
    {
        Task<string> Encrypt(string plainText);
        Task<bool> Compare(string plainText, string encryptedText);
    }
}
