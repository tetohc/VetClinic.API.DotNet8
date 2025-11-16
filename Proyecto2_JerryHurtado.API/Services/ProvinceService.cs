using Microsoft.EntityFrameworkCore;
using Proyecto2_JerryHurtado.API.Database;
using Proyecto2_JerryHurtado.API.Models.Dtos.Province;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Services
{
    public class ProvinceService : IGetAllService<ProvinceDto>
    {
        private readonly DatabaseContext _databaseContext;

        public ProvinceService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<ProvinceDto>> GetAll() =>
            await _databaseContext.Province.Select(x => new ProvinceDto { Id = x.Id, Name = x.Name }).ToListAsync();
    }
}