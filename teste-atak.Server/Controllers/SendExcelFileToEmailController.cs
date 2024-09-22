using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using teste_atak.Application.DTOs;
using teste_atak.Application.UseCases;
using teste_atak.Domain.Contracts;

namespace teste_atak.Server.Controllers
{
    [Route("email")]
    [ApiController]
    public class SendExcelFileToEmailController : ControllerBase
    {
        private readonly ISendEmailUseCase _sendEmailService;
        private readonly IGenerateExcelFileUseCase _generateExcelFileService;

        public SendExcelFileToEmailController(ISendEmailUseCase sendEmailService, IGenerateExcelFileUseCase generateExcelFileService)
        {
            _sendEmailService = sendEmailService;
            _generateExcelFileService = generateExcelFileService;
        }

        [HttpPost("send")]
        [Authorize]
        public async Task<IActionResult> Handle([FromBody] string[] customerIds)
        {
            if (!ModelState.IsValid || customerIds == null || customerIds.Length == 0)
            {
                return BadRequest("A lista de IDs de clientes não pode estar vazia.");
            }

            try
            {
                using var excelFileStream = await _generateExcelFileService.Execute(customerIds);

                var emailClaim = HttpContext.User.FindFirst(ClaimTypes.Email);
                var nameClaim = HttpContext.User.FindFirst(ClaimTypes.Name);

                if (emailClaim == null || nameClaim == null)
                {
                    return BadRequest("E-mail ou nome do usuário não encontrado.");
                }

                var userEmail = emailClaim.Value;
                var userName = nameClaim.Value;

                var emailDTO = new EmailDTO
                {
                    To = userEmail,
                    Subject = "[QuickExcel] - Dados Gerados",
                    Body = $"Olá, {userName}!\n\nEste e-mail contém os dados gerados pelo projeto QuickExcel. " +
                           "Você pode verificar o repositório do projeto no GitHub em: " +
                           "https://github.com/Gustavohps10/teste-atak\n\n" +
                           "Atenciosamente,\nGustavo Henrique.",
                    AttachmentStream = excelFileStream
                };

                await _sendEmailService.Execute(emailDTO);

                return Ok("E-mail enviado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
