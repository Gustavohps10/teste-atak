using Bogus;
using teste_atak.Domain.Entities;
using teste_atak.Domain.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace teste_atak.Infra.Data.Seed
{
    public class BogusDataGenerator
    {
        private readonly ICustomerRepository _customerRepository;

        public BogusDataGenerator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        private string GetRandomColor()
        {
            var random = new Random();
            return $"#{random.Next(0x1000000):X6}";
        }

        private string GetRandomFont()
        {
            var fonts = new[] { "Roboto", "Open Sans", "Lato", "Montserrat", "Oswald", "Source Sans Pro" };
            var random = new Random();
            return fonts[random.Next(fonts.Length)];
        }

        public async Task SeedCustomers(int count = 2000)
        {
            var faker = new Faker();
            var customers = new List<Customer>();

            for (int i = 0; i < count; i++)
            {
                var companyName = faker.Company.CompanyName().Substring(0, Math.Min(5, faker.Company.CompanyName().Length));
                var isBackgroundWhite = faker.Random.Bool();
                var bgColor = isBackgroundWhite ? "FFFFFF" : "000000";
                var textColor = isBackgroundWhite ? GetRandomColor().TrimStart('#') : "FFFFFF";
                var font = GetRandomFont();

                var customer = new Customer(
                    name: companyName,
                    email: faker.Internet.Email(),
                    phone: faker.Phone.PhoneNumber("(##) #####-####"),
                    imageUrl: $"https://placehold.co/200x200/{bgColor}/{textColor}?font={font}&text={Uri.EscapeDataString(companyName)}");

                customers.Add(customer);
            }

            await _customerRepository.InserRange(customers);
        }
    }
}
