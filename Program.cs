using AutoMapper;
using eMix.ConsultaCEP.Configurations;
using eMix.ConsultaCEP.Configurations.MapProfiles;
using eMix.ConsultaCEP.Contracts.Repositories;
using eMix.ConsultaCEP.Contracts.Services;
using eMix.ConsultaCEP.Repositories;
using eMix.ConsultaCEP.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Refit;

namespace eMix.ConsultaCEP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton(register => new MapperConfiguration(config =>
            {
                config.AllowNullDestinationValues = true;

                config.AddProfile<ViaCepProfile>();
            }).CreateMapper());

            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("eMix.InMemoryDB"));

            builder.Services
                .AddRefitClient<IViaCepHttpService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ViaCepApiUrl"] ?? ""));

            builder.Services.AddTransient<IAddressRepository, AddressRepository>();
            builder.Services.AddTransient<IAddressService, AddressService>();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "e.Mix Consulta CEP",
                    Description = "Aplicação para consultar dados de endereço e salva-los em um banco em memória",
                    Contact = new OpenApiContact
                    {
                        Name = "Luca Guiraldello",
                        Email = "lucaguiraldello@hotmail.com",
                    }
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
