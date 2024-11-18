using System;
using System.Collections.Generic;
using SmartTestExam.DAL.Repositories;

namespace SmartTestExam.BLL
{
    public class UserService
    {
        private readonly IUsersRepository _usersRepository;

        public UserService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        // Метод для аутентифікації користувача
        public bool AuthenticateUser(string username, string password)
        {
            var user = _usersRepository.GetAll().FirstOrDefault(u => u.Username == username);
            if (user != null && user.Password == password) // Замість цього додайте хешування пароля для безпеки
            {
                return true;
            }
            return false;
        }

        // Метод для отримання всіх користувачів
        public IEnumerable<User> GetAllUsers()
        {
            return _usersRepository.GetAll();
        }

        // Метод для зміни ролі користувача
        public void UpdateUserRole(int userId, string newRole)
        {
            _usersRepository.UpdateRole(userId, newRole);
        }

        // Метод для видалення користувача
        public void DeleteUser(int userId)
        {
            _usersRepository.Delete(userId);
        }

        // Метод для додавання нового користувача
        public void RegisterUser(User user)
        {
            _usersRepository.Add(user);
        }
    }
}
