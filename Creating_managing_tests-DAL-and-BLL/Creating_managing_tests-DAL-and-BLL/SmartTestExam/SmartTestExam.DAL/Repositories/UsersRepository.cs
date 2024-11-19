using Microsoft.EntityFrameworkCore;
using SmartTestExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartTestExam.DAL.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task Add(User user);
        Task UpdateRole(int userId, Role newRole);
        Task Delete(int id);
    }

    public class UsersRepository : IUsersRepository
    {
        private readonly SmartTestExamDbContext _context;

        public UsersRepository(SmartTestExamDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var userEntities = await _context.Users.ToListAsync();
            return userEntities.Select(MapToModel);
        }

        public async Task<User> GetById(int id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            return MapToModel(userEntity);
        }

        public async Task Add(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null.");

            var userEntity = MapToEntity(user);
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRole(int userId, Role newRole)
        {
            var userEntity = await _context.Users.FindAsync(userId);
            if (userEntity == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            userEntity.Role = newRole.ToString();
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
        }

        // Мапінг: Model -> Entity
        private UserEntity MapToEntity(User user)
        {
            return new UserEntity
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role.ToString()
            };
        }

        // Мапінг: Entity -> Model
        private User MapToModel(UserEntity userEntity)
        {
            return new User
            {
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email,
                Password = userEntity.Password,
                Role = Enum.TryParse(userEntity.Role, out Role role) ? role : Role.Student
            };
        }
    }
}
