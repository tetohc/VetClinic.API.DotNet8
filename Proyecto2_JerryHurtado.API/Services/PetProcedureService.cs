using Microsoft.EntityFrameworkCore;
using Proyecto2_JerryHurtado.API.Database;
using Proyecto2_JerryHurtado.API.Helpers;
using Proyecto2_JerryHurtado.API.Mappers;
using Proyecto2_JerryHurtado.API.Models.Dtos.PetProcedure;
using Proyecto2_JerryHurtado.API.Models.Entities;
using Proyecto2_JerryHurtado.API.Models.Enums;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Services
{
    public class PetProcedureService : IService<PetProcedureCreateDto, PetProcedureUpdateDto, PetProcedureDto>
    {
        private readonly DatabaseContext _databaseContext;

        public PetProcedureService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        #region CRUD

        public async Task<bool> Create(PetProcedureCreateDto createDto)
        {
            try
            {
                PetProcedureEntity entity = createDto.FromCreateDtoToEntity();
                await _databaseContext.PetProcedure.AddAsync(entity);
                await _databaseContext.SaveChangesAsync(); ;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(PetProcedureUpdateDto updateDto)
        {
            try
            {
                var existingEntity = _databaseContext.PetProcedure
                    .FirstOrDefault(x => x.Id == updateDto.Id);

                if (existingEntity is null)
                    return false;

                existingEntity.ProcedureTypeId = updateDto.ProcedureTypeId;
                existingEntity.Status = updateDto.Status;

                _databaseContext.Entry(existingEntity).State = EntityState.Modified;
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var entityToRemove = _databaseContext.PetProcedure.FirstOrDefault(x => x.Id == id);
            if (entityToRemove is null)
                return false;

            _databaseContext.PetProcedure.Remove(entityToRemove);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<PetProcedureDto?> GetById(Guid id)
        {
            var entity = await _databaseContext.PetProcedure
                .Include(x => x.Customer)
                .Include(x => x.Pet)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity?.ToDto();
        }

        public async Task<List<PetProcedureDto>> GetAll()
        {
            var entities = await _databaseContext.PetProcedure
                .Include(x => x.Customer)
                .Include(x => x.Pet)
                .ToListAsync();
            return entities.ToDtosList();
        }

        public async Task<List<PetProcedureDto>> Search(string query)
        {
            query = query.Trim();
            var result = await _databaseContext.PetProcedure
                .Include(x => x.Customer)
                .Include(x => x.Pet)
                .Where(x => x.Customer.Name.Contains(query) ||
                            x.Pet.Name.Contains(query))
                .ToListAsync();

            if (!result.Any())
            {
                var allProcedures = await _databaseContext.PetProcedure
                    .Include(x => x.Customer)
                    .Include(x => x.Pet)
                    .ToListAsync();

                result = allProcedures
                    .Where(x => EnumExtensions.GetDisplayName((PetProcedureStatus)x.Status)
                        .Contains(query, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            return result.ToDtosList();
        }

        public async Task<int> Count() => await _databaseContext.PetProcedure.CountAsync();

        #endregion CRUD
    }
}