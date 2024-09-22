using Microsoft.AspNetCore.Mvc;
using teste_atak.Application.DTOs;
using teste_atak.Application.UseCases;
using System.Threading.Tasks;

namespace teste_atak.Server.Controllers
{
    [ApiController]
    [Route("login")]
    public class UserAuthenticationController : ControllerBase
    {
        private readonly IUserAuthenticationUseCase _userAuthenticationUseCase;

        public UserAuthenticationController(IUserAuthenticationUseCase userAuthenticationUseCase)
        {
            _userAuthenticationUseCase = userAuthenticationUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Handle([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return BadRequest("Dados inválidos.");
            }

            try
            {
                var (token, user) = await _userAuthenticationUseCase.Execute(loginDTO);
                return Ok(new { token, user });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
