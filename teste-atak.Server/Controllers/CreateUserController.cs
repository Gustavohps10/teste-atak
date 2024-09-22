using Microsoft.AspNetCore.Mvc;
using teste_atak.Application.DTOs;

namespace teste_atak.Server.Controllers
{
    [Route("users")]
    [ApiController]
    public class CreateUserController : ControllerBase
    {
        private readonly ICreateUserUseCase _createUserService;

        public CreateUserController(ICreateUserUseCase createUserService)
        {
            _createUserService = createUserService;
        }

        [HttpPost]
        public async Task<IActionResult> Handle([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
               await _createUserService.Execute(userDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
