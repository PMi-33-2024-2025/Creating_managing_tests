using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SmartTestExam.DAL
{
    public class SmartTestExamDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TestEntity> Tests { get; set; }
        public DbSet<QuestionEntity> Questions { get; set; }
        public DbSet<AnswerEntity> Answers { get; set; }
        public DbSet<TestResultEntity> TestResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password='12345';Database=SmartTestExam");
        }
    }

    public class UserEntity
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

    public class TestEntity
    {
        public int Id { get; set; }
        [MaxLength(80)]
        public string Title { get; set; }
        public string Code { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class QuestionEntity
    {
        public int Id { get; set; }
        public int TestId { get; set; }

        [MaxLength(150)]
        public string QuestionText { get; set; }

        [Required]
        public string QuestionType { get; set; }
    }

    public class AnswerEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        [Required]
        public string AnswerText { get; set; }
        [Required]
        public bool IsCorrect { get; set; }
    }

    public class TestResultEntity
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
