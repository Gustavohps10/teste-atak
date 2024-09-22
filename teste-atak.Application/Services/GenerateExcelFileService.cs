using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using teste_atak.Domain.Contracts;
using teste_atak.Domain.Entities;

namespace teste_atak.Application.UseCases
{
    public class GenerateExcelFileService : IGenerateExcelFileUseCase
    {
        private readonly IExcelFileRepository _excelFileRepository;
        private readonly ICustomerRepository _customerRepository;

        public GenerateExcelFileService(IExcelFileRepository excelFileRepository, ICustomerRepository customerRepository)
        {
            _excelFileRepository = excelFileRepository;
            _customerRepository = customerRepository;
        }

        public async Task<MemoryStream> Execute(IEnumerable<string> customerIds)
        {
            var idCount = customerIds.Count();
            if (idCount < 10 || idCount > 1000)
            {
                throw new ArgumentException("A lista de IDs deve conter no mínimo 10 e no máximo 1000 IDs.");
            }

            var customers = await _customerRepository.GetByMultipleIds(customerIds);
            return await _excelFileRepository.Generate(customers);
        }
    }
}
