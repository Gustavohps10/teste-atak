using Microsoft.AspNetCore.Mvc;
using teste_atak.Application.DTOs;
using teste_atak.Domain.Contracts;

namespace teste_atak.Server.Controllers
{
    [Route("email")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private readonly ISendEmailUseCase _sendEmailService;

        public SendEmailController(ISendEmailUseCase sendEmailService)
        {
            _sendEmailService = sendEmailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Handle([FromBody] EmailDTO emailDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _sendEmailService.Execute(emailDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
