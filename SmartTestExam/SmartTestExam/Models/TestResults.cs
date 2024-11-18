using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTestExam.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
        public int? Score { get; set; }
        public DateTime CompletedAt { get; set; }

        public Test Test { get; set; }
        public User User { get; set; }
    }
}
