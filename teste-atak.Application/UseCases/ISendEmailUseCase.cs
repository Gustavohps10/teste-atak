using System.Threading.Tasks;
using teste_atak.Application.DTOs;

namespace teste_atak.Domain.Contracts
{
    public interface ISendEmailUseCase
    {
        Task Execute(EmailDTO emailDTO);
    }
}
