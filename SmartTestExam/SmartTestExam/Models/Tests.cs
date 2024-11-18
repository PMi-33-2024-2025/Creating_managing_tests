using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTestExam.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }

        public User Author { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<TestResult> TestResults { get; set; }
    }
}
