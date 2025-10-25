using Proyecto2_JerryHurtado.API.Models.Dtos.Province;
using Proyecto2_JerryHurtado.API.Models.Entities;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Services
{
    public class ProvinceService : IGetAllService<ProvinceDto>
    {
        private readonly List<ProvinceEntity> _entities = new()
        {
            new() { Id = 1, Name = "San José" },
            new() { Id = 2, Name = "Alajuela" },
            new() { Id = 3, Name = "Cartago" },
            new() { Id = 4, Name = "Heredia" },
            new() { Id = 5, Name = "Guanacaste" },
            new() { Id = 6, Name = "Puntarenas" },
            new() { Id = 7, Name = "Limón" }
        };

        public List<ProvinceDto> GetAll() =>
            _entities.Select(x => new ProvinceDto { Id = x.Id, Name = x.Name }).ToList();
    }
}