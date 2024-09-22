using System.Threading.Tasks;
using teste_atak.Application.DTOs;

namespace teste_atak.Application.UseCases
{
    public interface IUserAuthenticationUseCase
    {
        Task<(string token, UserDTO user)> Execute(LoginDTO loginDTO);
    }
}
