using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class StateRepository(ApplicationDbContext context) : IStateRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<CatStateEntity>? Create(CatStateEntity entity)
        {
            try
            {
                var result = await _context.CatStates
                    .Where(s => s.NameState == entity.NameState)
                    .FirstOrDefaultAsync();

                if (result != null)
                    throw new InvalidOperationException("El estado ya existe");

                var state = new CatState
                {
                    NameState = entity.NameState,
                    TableRelation = entity.TableRelation
                };

                await _context.CatStates.AddAsync(state);
                int rowsAffected = await _context.SaveChangesAsync();
                if (rowsAffected == 0)
                    throw new InvalidOperationException("No se pudo crear el estado");

                entity.IdState = state.IdState;
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el estado");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await _context.CatStates.FindAsync(id);
                if (entity == null)
                    throw new Exception("El estado no fue encontrado.");

                _context.CatStates.Remove(entity);
                int rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el estado");
            }
        }

        public async Task<IEnumerable<CatStateEntity>?> GetAll()
        {
            try
            {
                var states = await _context.CatStates
                    .Select(state => new CatStateEntity
                    {
                        IdState = state.IdState,
                        NameState = state.NameState ?? string.Empty,
                        TableRelation = state.TableRelation ?? string.Empty
                    })
                    .ToListAsync();

                return states;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los estados");
            }
        }

        public async Task<CatStateEntity>? GetById(int id)
        {
            try
            {
                var result = await _context.CatStates.FirstOrDefaultAsync(s => s.IdState == id);

                if (result == null)
                    throw new Exception("El estado no fue encontrado.");

                var state = new CatStateEntity
                {
                    IdState = result.IdState,
                    NameState = result.NameState ?? string.Empty,
                    TableRelation = result.TableRelation ?? string.Empty
                };

                return state;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el estado con ID {id}");
            }
        }

        public async Task<bool> Update(CatStateEntity entity)
        {
            try
            {
                var existingEntity = await _context.CatStates.FindAsync(entity.IdState);

                if (existingEntity == null)
                    throw new Exception("El estado no fue encontrado.");

                existingEntity.NameState = entity.NameState;
                existingEntity.TableRelation = entity.TableRelation;

                _context.CatStates.Update(existingEntity);
                int rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el estado");
            }
        }

        public Task<IEnumerable<CatStateEntity>?> GetStatesByTableRelation(string tableRelation)
        {
            throw new NotImplementedException();
        }
    }
}
