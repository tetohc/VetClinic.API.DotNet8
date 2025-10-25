using Proyecto2_JerryHurtado.API.Models.Dtos.Canton;
using Proyecto2_JerryHurtado.API.Models.Entities;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Services
{
    public class CantonService : IGetAllByParentService<CantonDto>
    {
        private readonly List<CantonEntity> _entities = new()
        {
            new() { Id = 1, Name = "Central", ProvinceId = 1 },
            new() { Id = 2, Name = "Escazú", ProvinceId = 1 },
            new() { Id = 3, Name = "Desamparados", ProvinceId = 1 },
            new() { Id = 4, Name = "Aserrí", ProvinceId = 1 },
            new() { Id = 5, Name = "Mora", ProvinceId = 1 },

            new() { Id = 6, Name = "Central", ProvinceId = 2 },
            new() { Id = 7, Name = "San Ramón", ProvinceId = 2 },
            new() { Id = 8, Name = "Grecia", ProvinceId = 2 },
            new() { Id = 9, Name = "Atenas", ProvinceId = 2 },
            new() { Id = 10, Name = "Naranjo", ProvinceId = 2 },

            new() { Id = 11, Name = "Cartago", ProvinceId = 3 },
            new() { Id = 12, Name = "Paraíso", ProvinceId = 3 },
            new() { Id = 13, Name = "La Unión", ProvinceId = 3 },
            new() { Id = 14, Name = "Jiménez", ProvinceId = 3 },
            new() { Id = 15, Name = "Turrialba", ProvinceId = 3 },

            new() { Id = 16, Name = "Heredia", ProvinceId = 4 },
            new() { Id = 17, Name = "Barva", ProvinceId = 4 },
            new() { Id = 18, Name = "Santo Domingo", ProvinceId = 4 },
            new() { Id = 19, Name = "Santa Bárbara", ProvinceId = 4 },
            new() { Id = 20, Name = "San Rafael", ProvinceId = 4 },

            new() { Id = 21, Name = "Liberia", ProvinceId = 5 },
            new() { Id = 22, Name = "Nicoya", ProvinceId = 5 },
            new() { Id = 23, Name = "Santa Cruz", ProvinceId = 5 },
            new() { Id = 24, Name = "Bagaces", ProvinceId = 5 },
            new() { Id = 25, Name = "Carrillo", ProvinceId = 5 },

            new() { Id = 26, Name = "Puntarenas", ProvinceId = 6 },
            new() { Id = 27, Name = "Esparza", ProvinceId = 6 },
            new() { Id = 28, Name = "Buenos Aires", ProvinceId = 6 },
            new() { Id = 29, Name = "Montes de Oro", ProvinceId = 6 },
            new() { Id = 30, Name = "Osa", ProvinceId = 6 },

            new() { Id = 31, Name = "Limón", ProvinceId = 7 },
            new() { Id = 32, Name = "Pococí", ProvinceId = 7 },
            new() { Id = 33, Name = "Siquirres", ProvinceId = 7 },
            new() { Id = 34, Name = "Talamanca", ProvinceId = 7 },
            new() { Id = 35, Name = "Matina", ProvinceId = 7 },
        };

        public CantonDto? GetById(int id) => _entities.Where(x => x.Id == id)
            .Select(x => new CantonDto { Id = x.Id, Name = x.Name })
            .FirstOrDefault();

        public List<CantonDto> GetAllById(int parentId) =>
            _entities.Where(x => x.ProvinceId == parentId)
                .Select(x => new CantonDto { Id = x.Id, Name = x.Name })
                .ToList();
    }
}