using Microsoft.EntityFrameworkCore;
using SmartTestExam.Models;

namespace SmartTestExam.DAL.Repositories
{
    public interface IQuestionsRepository
    {
        Task<IEnumerable<Question>> GetAll();
        Task<Question> GetById(int id);
        Task Add(Question question);
        Task Update(Question question);
        Task Delete(int id);
        Task<IEnumerable<Question>> GetByTestId(int testId);
    }

    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly SmartTestExamDbContext _context;

        public QuestionsRepository(SmartTestExamDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Question>> GetAll()
        {
            return await _context.Questions
                .Select(q => MapToModel(q))
                .ToListAsync();
        }

        public async Task<Question> GetById(int id)
        {
            var questionEntity = await _context.Questions.FindAsync(id);
            if (questionEntity == null)
                return null;

            return MapToModel(questionEntity);
        }

        public async Task Add(Question question)
        {
            var entity = MapToEntity(question);
            await _context.Questions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Question question)
        {
            var existingEntity = await _context.Questions.FindAsync(question.Id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Question with ID {question.Id} not found.");

            existingEntity.TestId = question.TestId;
            existingEntity.QuestionText = question.QuestionText;
            existingEntity.QuestionType = question.QuestionType.ToString();

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Questions.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Question with ID {id} not found.");

            _context.Questions.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Question>> GetByTestId(int testId)
        {
            return await _context.Questions
                .Where(q => q.TestId == testId)
                .Select(q => MapToModel(q))
                .ToListAsync();
        }

        // Маппінг: Entity -> Model
        private Question MapToModel(QuestionEntity entity)
        {
            return new Question
            {
                Id = entity.Id,
                TestId = entity.TestId,
                QuestionText = entity.QuestionText,
                QuestionType = Enum.TryParse<QuestionType>(entity.QuestionType, out var type) ? type : throw new InvalidCastException($"Invalid QuestionType value: {entity.QuestionType}")
            };
        }

        // Маппінг: Model -> Entity
        private QuestionEntity MapToEntity(Question model)
        {
            return new QuestionEntity
            {
                Id = model.Id,
                TestId = model.TestId,
                QuestionText = model.QuestionText,
                QuestionType = model.QuestionType.ToString()
            };
        }
    }
}
