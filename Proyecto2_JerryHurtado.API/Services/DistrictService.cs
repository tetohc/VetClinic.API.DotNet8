using Proyecto2_JerryHurtado.API.Models.Dtos.Canton;
using Proyecto2_JerryHurtado.API.Models.Dtos.District;
using Proyecto2_JerryHurtado.API.Models.Entities;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Services
{
    public class DistrictService : IGetAllByParentService<DistrictDto>
    {
        private readonly List<DistrictEntity> _entities = new()
        {
            new() { Id = 1, Name = "Carmen", CantonId = 1 },
            new() { Id = 2, Name = "Merced", CantonId = 1 },
            new() { Id = 3, Name = "Hospital", CantonId = 1 },

            new() { Id = 4, Name = "San Rafael", CantonId = 2 },
            new() { Id = 5, Name = "Escazú Centro", CantonId = 2 },
            new() { Id = 6, Name = "San Antonio", CantonId = 2 },

            new() { Id = 7, Name = "San Miguel", CantonId = 3 },
            new() { Id = 8, Name = "Los Guido", CantonId = 3 },
            new() { Id = 9, Name = "Rosario", CantonId = 3 },

            new() { Id = 10, Name = "Aserrí Centro", CantonId = 4 },
            new() { Id = 11, Name = "Tarbaca", CantonId = 4 },
            new() { Id = 12, Name = "Vuelta de Jorco", CantonId = 4 },

            new() { Id = 13, Name = "Colón", CantonId = 5 },
            new() { Id = 14, Name = "Guayabo", CantonId = 5 },
            new() { Id = 15, Name = "Tabarcia", CantonId = 5 },

            new() { Id = 16, Name = "Alajuela Centro", CantonId = 6 },
            new() { Id = 17, Name = "Carrizal", CantonId = 6 },
            new() { Id = 18, Name = "San Antonio", CantonId = 6 },

            new() { Id = 19, Name = "San Ramón Centro", CantonId = 7 },
            new() { Id = 20, Name = "San Juan", CantonId = 7 },
            new() { Id = 21, Name = "Piedades Norte", CantonId = 7 },

            new() { Id = 22, Name = "Grecia Centro", CantonId = 8 },
            new() { Id = 23, Name = "Bolívar", CantonId = 8 },
            new() { Id = 24, Name = "Puente de Piedra", CantonId = 8 },

            new() { Id = 25, Name = "Atenas Centro", CantonId = 9 },
            new() { Id = 26, Name = "Jesús", CantonId = 9 },
            new() { Id = 27, Name = "Mercedes", CantonId = 9 },

            new() { Id = 28, Name = "Naranjo Centro", CantonId = 10 },
            new() { Id = 29, Name = "San Miguel", CantonId = 10 },
            new() { Id = 30, Name = "San José", CantonId = 10 },

            new() { Id = 31, Name = "Cartago Centro", CantonId = 11 },
            new() { Id = 32, Name = "Guadalupe", CantonId = 11 },
            new() { Id = 33, Name = "Dulce Nombre", CantonId = 11 },

            new() { Id = 34, Name = "Paraíso Centro", CantonId = 12 },
            new() { Id = 35, Name = "Orosi", CantonId = 12 },
            new() { Id = 36, Name = "Cachí", CantonId = 12 },

            new() { Id = 37, Name = "Tres Ríos", CantonId = 13 },
            new() { Id = 38, Name = "San Diego", CantonId = 13 },
            new() { Id = 39, Name = "San Juan", CantonId = 13 },

            new() { Id = 40, Name = "Juan Viñas", CantonId = 14 },
            new() { Id = 41, Name = "Pejibaye", CantonId = 14 },
            new() { Id = 42, Name = "Tucurrique", CantonId = 14 },

            new() { Id = 43, Name = "Turrialba Centro", CantonId = 15 },
            new() { Id = 44, Name = "Santa Cruz", CantonId = 15 },
            new() { Id = 45, Name = "Pavones", CantonId = 15 },

            new() { Id = 46, Name = "Heredia Centro", CantonId = 16 },
            new() { Id = 47, Name = "Mercedes", CantonId = 16 },
            new() { Id = 48, Name = "San Francisco", CantonId = 16 },

            new() { Id = 49, Name = "Barva Centro", CantonId = 17 },
            new() { Id = 50, Name = "San Pedro", CantonId = 17 },
            new() { Id = 51, Name = "San Pablo", CantonId = 17 },

            new() { Id = 52, Name = "Santo Domingo Centro", CantonId = 18 },
            new() { Id = 53, Name = "Paracito", CantonId = 18 },
            new() { Id = 54, Name = "Tures", CantonId = 18 },

            new() { Id = 55, Name = "Santa Bárbara Centro", CantonId = 19 },
            new() { Id = 56, Name = "San Juan", CantonId = 19 },
            new() { Id = 57, Name = "Jesús", CantonId = 19 },

            new() { Id = 58, Name = "San Rafael Centro", CantonId = 20 },
            new() { Id = 59, Name = "Concepción", CantonId = 20 },
            new() { Id = 60, Name = "San Josecito", CantonId = 20 },

            new() { Id = 61, Name = "Liberia Centro", CantonId = 21 },
            new() { Id = 62, Name = "Cañas Dulces", CantonId = 21 },
            new() { Id = 63, Name = "Mayorga", CantonId = 21 },

            new() { Id = 64, Name = "Nicoya Centro", CantonId = 22 },
            new() { Id = 65, Name = "Mansión", CantonId = 22 },
            new() { Id = 66, Name = "San Antonio", CantonId = 22 },

            new() { Id = 67, Name = "Santa Cruz Centro", CantonId = 23 },
            new() { Id = 68, Name = "Veintisiete de Abril", CantonId = 23 },
            new() { Id = 69, Name = "Bolson", CantonId = 23 },

            new() { Id = 70, Name = "Bagaces Centro", CantonId = 24 },
            new() { Id = 71, Name = "Fortuna", CantonId = 24 },
            new() { Id = 72, Name = "Mogote", CantonId = 24 },

            new() { Id = 73, Name = "Carrillo Centro", CantonId = 25 },
            new() { Id = 74, Name = "Filadelfia", CantonId = 25 },
            new() { Id = 75, Name = "Palmira", CantonId = 25 },

            new() { Id = 76, Name = "Puntarenas Centro", CantonId = 26 },
            new() { Id = 77, Name = "Chacarita", CantonId = 26 },
            new() { Id = 78, Name = "Barranca", CantonId = 26 },

            new() { Id = 79, Name = "Esparza Centro", CantonId = 27 },
            new() { Id = 80, Name = "Caldera", CantonId = 27 },
            new() { Id = 81, Name = "San Rafael", CantonId = 27 },

            new() { Id = 82, Name = "Buenos Aires Centro", CantonId = 28 },
            new() { Id = 83, Name = "Potrero Grande", CantonId = 28 },
            new() { Id = 84, Name = "Volcán", CantonId = 28 },

            new() { Id = 85, Name = "Montes de Oro Centro", CantonId = 29 },
            new() { Id = 86, Name = "Miramar", CantonId = 29 },
            new() { Id = 87, Name = "La Unión", CantonId = 29 },

            new() { Id = 88, Name = "Osa Centro", CantonId = 30 },
            new() { Id = 89, Name = "Palmar", CantonId = 30 },
            new() { Id = 90, Name = "Bahía Ballena", CantonId = 30 },

            new() { Id = 91, Name = "Limón Centro", CantonId = 31 },
            new() { Id = 92, Name = "Valle La Estrella", CantonId = 31 },
            new() { Id = 93, Name = "Río Blanco", CantonId = 31 },

            new() { Id = 94, Name = "Guápiles", CantonId = 32 },
            new() { Id = 95, Name = "Jiménez", CantonId = 32 },
            new() { Id = 96, Name = "La Rita", CantonId = 32 },

            new() { Id = 97, Name = "Siquirres Centro", CantonId = 33 },
            new() { Id = 98, Name = "Pacuarito", CantonId = 33 },
            new() { Id = 99, Name = "Reventazón", CantonId = 33 },

            new() { Id = 100, Name = "Bribrí", CantonId = 34 },
            new() { Id = 101, Name = "Sixaola", CantonId = 34 },
            new() { Id = 102, Name = "Cahuita", CantonId = 34 },

            new() { Id = 103, Name = "Matina Centro", CantonId = 35 },
            new() { Id = 104, Name = "Batán", CantonId = 35 },
            new() { Id = 105, Name = "Carrandi", CantonId = 35 }
        };

        public DistrictDto? GetById(int id) => _entities.Where(x => x.Id == id)
            .Select(x => new DistrictDto { Id = x.Id, Name = x.Name })
            .FirstOrDefault();

        public List<DistrictDto> GetAllById(int cantonId) =>
            _entities.Where(x => x.CantonId == cantonId)
                .Select(x => new DistrictDto { Id = x.Id, Name = x.Name })
                .ToList();
    }
}