using System.Data;

namespace SmartTestExam.DAL.Repositories
{
    public enum Role
    {
        Admin,
        Creator,
        Student
    }

    public interface IUsersRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Add(User user);
        void UpdateRole(int userId, string newRole);
        void Delete(int id);
    }

    public class UsersRepository : IUsersRepository
    {
        private readonly SmartTestExamDbContext _context;

        public UsersRepository(SmartTestExamDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateRole(int userId, string newRole)
        {
            if (!Enum.IsDefined(typeof(Role), newRole))
            {
                throw new ArgumentException("Invalid role");
            }

            var user = GetById(userId);
            if (user != null)
            {
                user.Role = newRole;
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
