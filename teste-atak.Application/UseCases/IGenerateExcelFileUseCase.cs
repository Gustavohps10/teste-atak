using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using teste_atak.Domain.Entities;

namespace teste_atak.Application.UseCases
{
    public interface IGenerateExcelFileUseCase
    {
        Task<MemoryStream> Execute(IEnumerable<string> customerIds);
    }
}
