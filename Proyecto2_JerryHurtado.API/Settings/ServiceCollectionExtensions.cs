using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Proyecto2_JerryHurtado.API.Database;
using Proyecto2_JerryHurtado.API.Models.Dtos.Canton;
using Proyecto2_JerryHurtado.API.Models.Dtos.Customer;
using Proyecto2_JerryHurtado.API.Models.Dtos.District;
using Proyecto2_JerryHurtado.API.Models.Dtos.Employee;
using Proyecto2_JerryHurtado.API.Models.Dtos.Pet;
using Proyecto2_JerryHurtado.API.Models.Dtos.PetProcedure;
using Proyecto2_JerryHurtado.API.Models.Dtos.Province;
using Proyecto2_JerryHurtado.API.Services;
using Proyecto2_JerryHurtado.API.Services.Interfaces;
using Proyecto2_JerryHurtado.API.Validators.Customer;
using Proyecto2_JerryHurtado.API.Validators.Employee;
using Proyecto2_JerryHurtado.API.Validators.Pet;
using Proyecto2_JerryHurtado.API.Validators.PetProcedure;
using System.Reflection;

namespace Proyecto2_JerryHurtado.API.Settings
{
    /// <summary>
    /// Esta clase contiene métodos de extensión para configurar servicios en la API.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configura Swagger para la documentación de la API.
        /// </summary>
        /// <param name="services">Colección de servicios de la aplicación.</param>
        /// <returns>La colección de servicios actualizada.</returns>
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Proyecto 3 UNED",
                    Description = "API para Veterinaria, Fundamentos de programación web",
                    Contact = new OpenApiContact
                    {
                        Name = "Jerry Alonso Hurtado Castillo",
                        Email = string.Empty,
                        Url = new Uri("https://www.google.com"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        /// <summary>
        /// Registra los servicios de aplicación y sus interfaces.
        /// </summary>
        /// <param name="services">Colección de servicios de la aplicación.</param>
        /// <returns>La colección de servicios actualizada.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            #region Ubicaciones

            services.AddScoped<IGetAllService<ProvinceDto>, ProvinceService>();
            services.AddScoped<IGetAllByParentService<CantonDto>, CantonService>();
            services.AddScoped<IGetAllByParentService<DistrictDto>, DistrictService>();

            #endregion Ubicaciones

            services.AddScoped<IService<CustomerCreateDto, CustomerUpdateDto, CustomerDto>, CustomerService>();
            services.AddScoped<IService<EmployeeCreateDto, EmployeeUpdateDto, EmployeeDto>, EmployeeService>();
            services.AddScoped<IService<PetCreateDto, PetUpdateDto, PetDto>, PetService>();
            services.AddScoped<IService<PetProcedureCreateDto, PetProcedureUpdateDto, PetProcedureDto>, PetProcedureService>();
            services.AddScoped<IVaccinationAnnualService, ReportService>();

            return services;
        }

        /// <summary>
        /// Registra los validadores de FluentValidation.
        /// </summary>
        /// <param name="services">Colección de servicios de la aplicación.</param>
        /// <returns>La colección de servicios actualizada.</returns>
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CustomerCreateDto>, CreateCustomerValidator>();
            services.AddScoped<IValidator<CustomerUpdateDto>, UpdateCustomerValidator>();

            services.AddScoped<IValidator<EmployeeCreateDto>, CreateEmployeeValidator>();
            services.AddScoped<IValidator<EmployeeUpdateDto>, UpdateEmployeeValidator>();

            services.AddScoped<IValidator<PetCreateDto>, CreatePetValidator>();
            services.AddScoped<IValidator<PetUpdateDto>, UpdatePetValidator>();

            services.AddScoped<IValidator<PetProcedureCreateDto>, CreatePetProcedureValidator>();
            services.AddScoped<IValidator<PetProcedureUpdateDto>, UpdatePetProcedureValidator>();

            return services;
        }

        /// <summary>
        /// Registra el servicio de persistencia configurando el DbContext de la aplicación.
        /// </summary>
        /// <param name="services">Colección de servicios de la aplicación.</param>
        /// <param name="configuration">Configuración de la aplicación que contiene la cadena de conexión.</param>
        /// <returns>La colección de servicios actualizada con el DbContext registrado.</returns>
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("SQLConnectionString")
                ),
                ServiceLifetime.Transient
            );
            return services;
        }
    }
}