using System.Threading.Tasks;
using teste_atak.Application.DTOs;

namespace teste_atak.Application.UseCases
{
    public interface IReadAllCustomersUseCase
    {
        Task<PaginatedResultDTO<CustomerDTO>> Execute(string? name, string? phone, string? sortBy, bool sortDescending, int pageNumber = 1, int pageSize = 10);
    }
}