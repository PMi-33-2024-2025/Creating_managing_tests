using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SmartTestExam.DAL
{
    public class SmartTestExamDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<TestResult> TestResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password='12345';Database=SmartTestExam");
        }
    }

    public class User
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        public string Password { get; set; }

        [MaxLength(20)]
        [RegularExpression("^(admin|creator|student)$", ErrorMessage = "Role must be either admin, creator, or student.")]
        public string Role { get; set; }
    }

    public class Test
    {
        public int Id { get; set; }
        [MaxLength(80)]
        public string Title { get; set; }
        public string Code { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum QuestionType
    {
        MultipleChoice,
        Open,
        ShortAnswer,
        TrueFalse,
        SingleCorrectAnswer
    }

    public class Question
    {
        public int Id { get; set; }
        public int TestId { get; set; }

        [MaxLength(150)]
        public string QuestionText { get; set; }

        [Required]
        public QuestionType QuestionType { get; set; }
    }

    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        [Required]
        public string AnswerText { get; set; }
        [Required]
        public bool IsCorrect { get; set; }
    }

    public class TestResult
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100.")]
        public int Score { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
