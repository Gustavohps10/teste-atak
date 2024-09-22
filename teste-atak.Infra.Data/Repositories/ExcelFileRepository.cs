using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using teste_atak.Domain.Entities;
using teste_atak.Domain.Contracts;

namespace teste_atak.Infra.Data.Repositories
{
    public class ExcelFileRepository : IExcelFileRepository
    {
        public async Task<MemoryStream> Generate(IEnumerable<Customer> customers)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var memoryStream = new MemoryStream();

            using (var package = new ExcelPackage(memoryStream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Clientes");
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Email";
                worksheet.Cells[1, 4].Value = "Phone";

                int row = 2;
                foreach (var customer in customers)
                {
                    worksheet.Cells[row, 1].Value = customer.Id;
                    worksheet.Cells[row, 2].Value = customer.Name;
                    worksheet.Cells[row, 3].Value = customer.Email;
                    worksheet.Cells[row, 4].Value = customer.Phone;
                    row++;
                }

                await package.SaveAsync();
            }

            // Reseta a posição do MemoryStream para o início
            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}
