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

        public async Task SeedCustomers(int count = 2000)
        {
            var faker = new Faker();

            var customers = new List<Customer>();
            for (int i = 0; i < count; i++)
            {
                var customer = new Customer(
                    name: faker.Person.FullName,
                    email: faker.Internet.Email(),
                    phone: faker.Phone.PhoneNumber("+55 (##) #####-####"),
                    imageUrl: faker.Image.PicsumUrl());

                customers.Add(customer);
            }

            await _customerRepository.InserRange(customers);
        }
    }
}
