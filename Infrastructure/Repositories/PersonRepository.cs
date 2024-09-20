using Biokudi_Backend.Domain.Entities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biokudi_Backend.Infrastructure.Repositories
{
    public class PersonRepository(ApplicationDbContext context) : IPersonRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<PersonEntity>? Create(PersonEntity user)
        {
            try
            {
                if (_context.People.Any(u => u.Email == user.Email && u.AccountDeleted == false))
                    throw new KeyNotFoundException($"El correo ya se encuentra registrado");
                var person = new Person
                {
                    NameUser = user.NameUser,
                    Email = user.Email,
                    Password = user.Password,  
                    Hash = user.Hash,          
                    RoleId = user.RoleId,
                    StateId = user.StateId,
                    Telephone = user.Telephone,
                    EmailNotification = user.EmailNotification,
                    EmailPost = user.EmailPost,
                    EmailList = user.EmailList,
                    DateCreated = PersonEntity.DateNowColombia(),
                    DateModified = PersonEntity.DateNowColombia()
                };
                _context.People.Add(person);
                await _context.SaveChangesAsync();
                user.IdUser = person.IdUser;
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception($"Error al crear la cuenta");
            }
        }

        public async Task<PersonEntity?> GetAccountByEmail(string email)
        {
            try
            {
                var result = await _context.People.Include(p => p.Role).FirstOrDefaultAsync(u => u.Email == email) 
                    ?? throw new KeyNotFoundException($"Correo invalido");
                var person = new PersonEntity
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
                    },
                };
                return person;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> Update(PersonEntity entity)
        {
            throw new NotImplementedException();
        }
        public Task<bool> Delete(PersonEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PersonEntity>?> GetAccountsByRole(int role)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PersonEntity>?> GetAccountsByState(int role)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PersonEntity>?> GetAccountsDeleted()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PersonEntity>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PersonEntity>? GetById(int id)
        {
            try
            {
                var result = await _context.People.Include(p => p.Role).FirstOrDefaultAsync(u => u.IdUser == id)
                    ?? throw new KeyNotFoundException($"Usuario no encontrado");
                var person = new PersonEntity
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
                    },
                };
                return person;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
