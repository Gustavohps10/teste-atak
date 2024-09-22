using System.Threading.Tasks;

namespace teste_atak.Domain.Contracts
{
    public interface IMailerRepository
    {
        Task Send(string to, string subject, string body, string? attachmentPath);
    }
}
