using Biokudi_Backend.Application.Utilities;
using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Domain.ValueObject;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class PersonRepository(ApplicationDbContext context) : IPersonRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<PersonEntity>> Create(PersonEntity user)
        {
            try
            {
                var existingUser = await _context.People.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (existingUser != null)
                    return Result<PersonEntity>.Failure("El correo ya se encuentra registrado.");

                var person = new Person
                {
                    NameUser = user.NameUser,
                    Email = user.Email,
                    Password = user.Password,
                    RoleId = user.RoleId,
                    StateId = user.StateId,
                    Telephone = user.Telephone,
                    EmailNotification = user.EmailNotification,
                    EmailPost = user.EmailPost,
                    EmailList = user.EmailList,
                    DateCreated = DateUtility.DateNowColombia(),
                    DateModified = DateUtility.DateNowColombia()
                };

                await _context.People.AddAsync(person);
                int success = await _context.SaveChangesAsync();

                if (success == 0)
                    return Result<PersonEntity>.Failure("Error al guardar los datos en la base de datos.");

                user.IdUser = person.IdUser;
                return Result<PersonEntity>.Success(user);
            }
            catch (Exception ex)
            {
                return Result<PersonEntity>.Failure($"Error al crear el usuario: {ex.Message}");
            }
        }

        public async Task<Result<PersonEntity>> GetAccountByEmail(string email)
        {
            try
            {
                var result = await _context.People
                    .AsNoTracking()
                    .Include(p => p.Role)
                    .FirstOrDefaultAsync(u => u.Email == email);
                if (result == null)
                    return Result<PersonEntity>.Failure("Correo no encontrado.");

                return Result<PersonEntity>.Success(new PersonEntity
                {
                    IdUser = result.IdUser,
                    NameUser = result.NameUser,
                    Email = result.Email,
                    Password = result.Password,
                    RoleId = result.RoleId,
                    StateId = result.StateId,
                    Telephone = result.Telephone,
                    EmailNotification = result.EmailNotification,
                    EmailPost = result.EmailPost,
                    EmailList = result.EmailList,
                    DateCreated = result.DateCreated,
                    DateModified = result.DateModified,
                    AccountDeleted = result.AccountDeleted,
                    Role = new CatRole
                    {
                        IdRole = result.Role.IdRole,
                        NameRole = result.Role.NameRole
                    }
                });
            }
            catch (Exception ex)
            {
                return Result<PersonEntity>.Failure($"Error al obtener la cuenta por email: {ex.Message}");
            }
        }

        public async Task<Result<PersonEntity>> GetById(int id)
        {
            try
            {
                var result = await _context.People
                    .AsNoTracking()
                    .Include(p => p.Role)
                    .Include(p => p.State)
                    .FirstOrDefaultAsync(u => u.IdUser == id);
                if (result == null)
                    return Result<PersonEntity>.Failure("Usuario no encontrado.");

                return Result<PersonEntity>.Success(new PersonEntity
                {
                    IdUser = result.IdUser,
                    NameUser = result.NameUser,
                    Email = result.Email,
                    RoleId = result.RoleId,
                    StateId = result.StateId,
                    Telephone = result.Telephone,
                    EmailNotification = result.EmailNotification,
                    EmailPost = result.EmailPost,
                    EmailList = result.EmailList,
                    DateCreated = result.DateCreated,
                    DateModified = result.DateModified,
                    AccountDeleted = result.AccountDeleted,
                    Role = new CatRole
                    {
                        IdRole = result.Role.IdRole,
                        NameRole = result.Role.NameRole
                    },
                    State = new CatState
                    {
                        IdState = result.State.IdState,
                        NameState = result.State.NameState
                    }
                });
            }
            catch (Exception ex)
            {
                return Result<PersonEntity>.Failure($"Error al obtener el usuario por ID: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Update(PersonEntity user)
        {
            try
            {
                var existingUser = await _context.People.FindAsync(user.IdUser);
                if (existingUser == null)
                    return Result<bool>.Failure("Usuario no encontrado.");

                existingUser.NameUser = user.NameUser != null && user.NameUser != existingUser.NameUser ? user.NameUser : existingUser.NameUser;
                existingUser.Email = user.Email != null && user.Email != existingUser.Email ? user.Email : existingUser.Email;
                existingUser.RoleId = user.RoleId != 0 && user.RoleId != existingUser.RoleId ? user.RoleId : existingUser.RoleId;
                existingUser.StateId = user.StateId != null && user.StateId != existingUser.StateId ? user.StateId : existingUser.StateId;
                existingUser.Telephone = user.Telephone != null && user.Telephone != existingUser.Telephone ? user.Telephone : existingUser.Telephone;
                existingUser.EmailNotification = user.EmailNotification != null && user.EmailNotification != existingUser.EmailNotification ? user.EmailNotification : existingUser.EmailNotification;
                existingUser.EmailPost = user.EmailPost != null && user.EmailPost != existingUser.EmailPost ? user.EmailPost : existingUser.EmailPost;
                existingUser.EmailList = user.EmailList != null && user.EmailList != existingUser.EmailList ? user.EmailList : existingUser.EmailList;

                existingUser.DateModified = DateUtility.DateNowColombia();

                _context.People.Update(existingUser);
                int rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Error al actualizar el usuario.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al actualizar el usuario: {ex.Message}");
            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            try
            {
                var user = await _context.People.FindAsync(id);
                if (user == null)
                    return Result<bool>.Failure("Usuario no encontrado.");

                _context.People.Remove(user);
                await _context.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al eliminar el usuario: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<PersonEntity>>> GetAll()
        {
            try
            {
                var result = await _context.People
                    .AsNoTracking()
                    .Include(p => p.Role)
                    .Include(p => p.State)
                    .ToListAsync();
                return Result<IEnumerable<PersonEntity>>.Success(result.Select(p => new PersonEntity
                {
                    IdUser = p.IdUser,
                    NameUser = p.NameUser,
                    Email = p.Email,
                    RoleId = p.RoleId,
                    StateId = p.StateId,
                    Telephone = p.Telephone,
                    EmailNotification = p.EmailNotification,
                    EmailPost = p.EmailPost,
                    EmailList = p.EmailList,
                    DateCreated = p.DateCreated,
                    DateModified = p.DateModified,
                    AccountDeleted = p.AccountDeleted,
                    Role = new CatRole
                    {
                        IdRole = p.Role.IdRole,
                        NameRole = p.Role.NameRole
                    },
                    State = new CatState
                    {
                        IdState = p.State.IdState,
                        NameState = p.State.NameState
                    }
                }));
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<PersonEntity>>.Failure($"Error al obtener la lista de usuarios: {ex.Message}");
            }
        }

        public async Task<Result<bool>> UpdateUserPassword(PersonEntity user)
        {
            try
            {
                var existingUser = await _context.People.FindAsync(user.IdUser);
                if (existingUser == null)
                    return Result<bool>.Failure("Usuario no encontrado.");

                existingUser.Password = user.Password;
                existingUser.DateModified = DateUtility.DateNowColombia();

                _context.People.Update(existingUser);
                int rowsAffected = await _context.SaveChangesAsync();

                return rowsAffected > 0
                    ? Result<bool>.Success(true)
                    : Result<bool>.Failure("Error al actualizar la contraseña.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al actualizar la contraseña: {ex.Message}");
            }
        }
    }
}
