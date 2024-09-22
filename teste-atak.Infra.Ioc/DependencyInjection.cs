using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teste_atak.Application.Mappings;
using teste_atak.Application.UseCases;
using teste_atak.Domain.Contracts;
using teste_atak.Infra.Data.Config;
using teste_atak.Infra.Data.Context;
using teste_atak.Infra.Data.Repositories;
using teste_atak.Infra.Data.Seed;

namespace teste_atak.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            //Database
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION")
                ?? config.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString,
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });
            // Faker
            services.AddScoped<BogusDataGenerator>();
            
            //SMTP Mail
            var smtpConfig = new SmtpConfig
            {
                Host = config["SmtpSettings:Host"]!,
                Port = int.TryParse(config["SmtpSettings:Port"], out var port) ? port : 587,
                Username = config["SmtpSettings:Username"]!,
                Password = config["SmtpSettings:Password"]!,
                From = config["SmtpSettings:From"]!
            };
            services.AddSingleton(smtpConfig);

            //AutoMapper
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            /*
             * Repositories
             */
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICrypterRepository, CrypterRepository>();
            services.AddScoped<IMailerRepository, MailerRepository>();

            /*
             * Services - Use Cases
             */

            //Users
            services.AddScoped<ICreateUserUseCase, CreateUserService>();

            //Customers
            services.AddScoped<IReadAllCustomersUseCase, ReadAllCustomersService>();

            //Email
            services.AddScoped<ISendEmailUseCase, SendEmailService>();

            return services;
        }
    }
}
