using Microsoft.EntityFrameworkCore;
using Proyecto2_JerryHurtado.API.Database;
using Proyecto2_JerryHurtado.API.Helpers;
using Proyecto2_JerryHurtado.API.Mappers;
using Proyecto2_JerryHurtado.API.Models.Dtos.Pet;
using Proyecto2_JerryHurtado.API.Models.Entities;
using Proyecto2_JerryHurtado.API.Models.Enums;
using Proyecto2_JerryHurtado.API.Services.Interfaces;

namespace Proyecto2_JerryHurtado.API.Services
{
    /// <summary>
    /// Servicio para gestionar operaciones relacionadas con mascotas.
    /// </summary>
    public class PetService : IService<PetCreateDto, PetUpdateDto, PetDto>
    {
        private readonly DatabaseContext _databaseContext;

        public PetService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        #region CRUD

        public async Task<bool> Create(PetCreateDto createDto)
        {
            try
            {
                PetEntity entity = createDto.FromCreateDtoToEntity();
                await _databaseContext.Pet.AddAsync(entity);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(PetUpdateDto updateDto)
        {
            try
            {
                var existingEntity = await _databaseContext.Pet.FirstOrDefaultAsync(x => x.Id == updateDto.Id);
                if (existingEntity is null)
                    return false;

                existingEntity.PetSpecies = updateDto.PetSpecies;
                existingEntity.Race = updateDto.Race.Trim();
                existingEntity.Age = updateDto.Age;
                existingEntity.Color = updateDto.Color.Trim();
                existingEntity.LastVisitDate = updateDto.LastVisitDate;

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
            var entityToRemove = await _databaseContext.Pet.FirstOrDefaultAsync(x => x.Id == id);
            if (entityToRemove is null)
                return false;

            try
            {
                _databaseContext.Pet.Remove(entityToRemove);
                await _databaseContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<PetDto?> GetById(Guid id)
        {
            var entities = await _databaseContext.Pet
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);
            return entities?.ToDto();
        }

        public async Task<List<PetDto>> GetAll()
        {
            var entities = await _databaseContext.Pet
                .Include(p => p.Customer)
                .ToListAsync();
            return entities.ToDtosList();
        }

        public async Task<List<PetDto>> Search(string query)
        {
            query = query.Trim();
            var result = await _databaseContext.Pet.Include(x => x.Customer)
                .Where(x => x.Name.Contains(query) ||
                            x.Race.Contains(query) ||
                            x.Color.Contains(query))
                .ToListAsync();

            if (!result.Any())
            {
                var allPets = await _databaseContext.Pet.Include(x => x.Customer).ToListAsync();
                result = allPets
                    .Where(x => EnumExtensions.GetDisplayName((PetSpecies)x.PetSpecies)
                        .Contains(query, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            return result.ToDtosList();
        }

        public async Task<int> Count() => await _databaseContext.Pet.CountAsync();

        #endregion CRUD
    }
}