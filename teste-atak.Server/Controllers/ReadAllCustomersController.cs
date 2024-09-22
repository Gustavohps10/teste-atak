using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using teste_atak.Application.DTOs;
using teste_atak.Application.UseCases;

namespace teste_atak.Server.Controllers
{
    [Route("customers")]
    [ApiController]
    public class ReadAllCustomersController : ControllerBase
    {
        private readonly IReadAllCustomersUseCase _readAllCustomersService;

        public ReadAllCustomersController(IReadAllCustomersUseCase readAllCustomersService)
        {
            _readAllCustomersService = readAllCustomersService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Handle(
            [FromQuery] string? name = null,
            [FromQuery] string? phone = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool sortDescending = false,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _readAllCustomersService.Execute(name, phone, sortBy, sortDescending, pageNumber, pageSize);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Erro = ex.Message });
            }
        }
    }
}
