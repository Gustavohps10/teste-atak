using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teste_atak.Application.Mappings;
using teste_atak.Domain.Contracts;
using teste_atak.Infra.Data.Context;
using teste_atak.Infra.Data.Repositories;
using teste_atak.Infra.Data.Seed;

namespace teste_atak.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION")
                ?? config.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString,
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            /*
            * Repositories
            */
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            /*
             * Services - Use Cases
             */

            //Users
            services.AddScoped<ICreateUserUseCase, CreateUserService>();

            services.AddScoped<BogusDataGenerator>();
            return services;
        }
    }
}
