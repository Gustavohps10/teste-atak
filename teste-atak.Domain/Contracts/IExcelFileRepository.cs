using teste_atak.Domain.Entities;

namespace teste_atak.Domain.Contracts
{
    public interface IExcelFileRepository
    {
        Task<MemoryStream> Generate(IEnumerable<Customer> customers);
    }
}
