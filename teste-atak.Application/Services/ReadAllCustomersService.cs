using AutoMapper;
using System.Threading.Tasks;
using teste_atak.Application.DTOs;
using teste_atak.Domain.Contracts;

namespace teste_atak.Application.UseCases
{
    public class ReadAllCustomersService : IReadAllCustomersUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public ReadAllCustomersService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResultDTO<CustomerDTO>> Execute(string? name, string? phone, string? sortBy, bool sortDescending, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentException("O número da página deve ser maior que 0.");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException("O tamanho da página deve ser maior que 0.");
            }

            var validSortFields = new[] { "Name", "Phone" }; // Adicione mais campos permitidos conforme necessário
            if (!string.IsNullOrEmpty(sortBy) && !validSortFields.Contains(sortBy))
            {
                throw new ArgumentException($"Campo de ordenação '{sortBy}' inválido. Os campos permitidos são: {string.Join(", ", validSortFields)}.");
            }

            var (customers, totalCount, totalPages) = await _customerRepository.GetAll(name, phone, sortBy, sortDescending, pageNumber, pageSize);

            var customerDTOs = _mapper.Map<IEnumerable<CustomerDTO>>(customers);

            return new PaginatedResultDTO<CustomerDTO>
            {
                Items = customerDTOs,
                TotalCount = totalCount,
                PageIndex = pageNumber,
                TotalPages = totalPages
            };
        }
    }
}
