using FluentValidation;
using Microsoft.OpenApi.Models;
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
                    Title = "Proyecto 2 UNED",
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

            services.AddSingleton<IGetAllService<ProvinceDto>, ProvinceService>();
            services.AddSingleton<IGetAllByParentService<CantonDto>, CantonService>();
            services.AddSingleton<IGetAllByParentService<DistrictDto>, DistrictService>();

            #endregion Ubicaciones

            #region Resolución diferida (Lazy<T>)

            // Se registra Lazy<IProcedureCascadeDeletionService> para resolver un ciclo de dependencia indirecto entre PetService, PetProcedureService y CustomerService,
            // permitiendo la resolución diferida de la dependencia y evitando errores de activación en tiempo de ejecución.
            // Esto surge debido a las operaciones en cascada de eliminación que se realizan entre estos servicios.
            services.AddSingleton(provider =>
                new Lazy<IProcedureCascadeDeletionService>(() =>
                    provider.GetRequiredService<IProcedureCascadeDeletionService>()));

            // Se registra Lazy<IReadOnlyCustomerService> para resolver un ciclo de dependencia indirecto entre PetService y PetProcedureService.
            // Este ciclo surge debido a la necesidad de acceder a datos de clientes desde ambos servicios, mientras que PetProcedureService también depende de PetService.
            // Al utilizar Lazy<T>, se difiere la resolución de IReadOnlyCustomerService hasta el momento en que realmente se necesita,
            // evitando errores de activación en tiempo de ejecución y permitiendo una inyección segura y controlada.
            services.AddSingleton(provider =>
                new Lazy<IReadOnlyCustomerService>(() =>
                    provider.GetRequiredService<IReadOnlyCustomerService>()));

            #endregion Resolución diferida (Lazy<T>)

            #region Clientes

            services.AddSingleton<CustomerService>();
            services.AddSingleton<IService<CustomerCreateDto, CustomerUpdateDto, CustomerDto>>(provider =>
                provider.GetRequiredService<CustomerService>());

            services.AddSingleton<IReadOnlyCustomerService>(provider =>
                provider.GetRequiredService<CustomerService>());

            #endregion Clientes

            #region Empleados

            services.AddSingleton<IService<EmployeeCreateDto, EmployeeUpdateDto, EmployeeDto>, EmployeeService>();

            #endregion Empleados

            #region Mascotas

            services.AddSingleton<PetService>();
            services.AddSingleton<IService<PetCreateDto, PetUpdateDto, PetDto>>(provider =>
                provider.GetRequiredService<PetService>());

            services.AddSingleton<IPetCascadeDeletionService>(provider =>
                provider.GetRequiredService<PetService>());

            services.AddSingleton<IReadOnlyPetService>(provider =>
                provider.GetRequiredService<PetService>());

            #endregion Mascotas

            #region Procedimientos de mascotas

            services.AddSingleton<PetProcedureService>();
            services.AddSingleton<IService<PetProcedureCreateDto, PetProcedureUpdateDto, PetProcedureDto>>(provider =>
                provider.GetRequiredService<PetProcedureService>());

            services.AddSingleton<IProcedureCascadeDeletionService>(provider =>
                provider.GetRequiredService<PetProcedureService>());

            #endregion Procedimientos de mascotas

            #region Reportes

            services.AddScoped<IProjectedVaccinationReportService, ReportService>();

            #endregion Reportes

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
    }
}