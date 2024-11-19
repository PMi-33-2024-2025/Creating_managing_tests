using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTestExam.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public ICollection<Test> CreatedTests { get; set; }
        public ICollection<TestResult> TestResults { get; set; }
    }

    public enum Role
    {
        Admin,
        Creator,
        Student
    }
}
