using Proyecto2_JerryHurtado.API.Settings;

namespace Proyecto2_JerryHurtado.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // Scaffold-DbContext Name=SQLConnectionString Microsoft.EntityFrameworkCore.SqlServer -NoPluralize -OutputDir Database -Context DatabaseService
            builder.Services
                .AddSwaggerConfiguration()
                .AddApplicationServices()
                .AddFluentValidation()
                .AddPersistence(configuration: builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}