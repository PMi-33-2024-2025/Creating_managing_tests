using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SmartTestExam.DAL.Repositories;
using SmartTestExam.Models;

namespace SmartTestExam.BLL
{
    public class UsersLogic
    {
        private readonly IUsersRepository _usersRepository;

        public UsersLogic(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            var user = (await _usersRepository.GetAll()).FirstOrDefault(u => u.Email == username);
            if (user != null && VerifyPassword(password, user.Password))
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _usersRepository.GetAll();
        }

        public async Task UpdateUserRole(int userId, Role newRole)
        {
            await _usersRepository.UpdateRole(userId, newRole);
        }

        public async Task DeleteUser(int userId)
        {
            await _usersRepository.Delete(userId);
        }

        public async Task RegisterUser(User user, string password)
        {
            var hashedPassword = HashPassword(password); // Хешування пароля перед збереженням
            user.Password = hashedPassword;
            await _usersRepository.Add(user);
        }

        // Хешування пароля
        private string HashPassword(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(password, 16, 10000))
            {
                byte[] hash = algorithm.GetBytes(20);
                return Convert.ToBase64String(hash);
            }
        }

        // Перевірка хешованого пароля
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            byte[] storedHashBytes = Convert.FromBase64String(storedHash);
            using (var algorithm = new Rfc2898DeriveBytes(enteredPassword, 16, 10000))
            {
                byte[] enteredHash = algorithm.GetBytes(20);
                return storedHashBytes.SequenceEqual(enteredHash);
            }
        }
    }
}
