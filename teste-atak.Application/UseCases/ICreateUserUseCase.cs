using System.Threading.Tasks;
using teste_atak.Application.DTOs;

public interface ICreateUserUseCase
{
    Task Execute(UserDTO userDTO);
}
