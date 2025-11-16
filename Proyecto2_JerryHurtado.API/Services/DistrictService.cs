using Microsoft.EntityFrameworkCore;
using Proyecto2_JerryHurtado.API.Database;
using Proyecto2_JerryHurtado.API.Models.Dtos.District;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Services
{
    public class DistrictService : IGetAllByParentService<DistrictDto>
    {
        private readonly DatabaseContext _databaseContext;

        public DistrictService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<DistrictDto>> GetAllById(int cantonId) =>
            await _databaseContext.District.Where(x => x.CantonId == cantonId)
                .Select(x => new DistrictDto { Id = x.Id, Name = x.Name })
                .ToListAsync();
    }
}