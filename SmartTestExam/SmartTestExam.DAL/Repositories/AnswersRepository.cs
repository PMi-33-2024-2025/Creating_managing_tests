using Microsoft.EntityFrameworkCore;
using SmartTestExam.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartTestExam.DAL.Repositories
{
    public interface IAnswersRepository
    {
        Task<IEnumerable<Answer>> GetAll();
        Task<Answer> GetById(int id);
        Task Add(Answer answer);
        Task Update(Answer answer);
        Task Delete(int id);
        Task<IEnumerable<Answer>> GetByQuestionId(int questionId);
    }

    public class AnswersRepository : IAnswersRepository
    {
        private readonly SmartTestExamDbContext _context;

        public AnswersRepository(SmartTestExamDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Answer>> GetAll()
        {
            return await _context.Answers
                .Select(a => MapToModel(a))
                .ToListAsync();
        }

        public async Task<Answer> GetById(int id)
        {
            var entity = await _context.Answers.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Answer with ID {id} not found.");

            return MapToModel(entity);
        }

        public async Task Add(Answer answer)
        {
            if (answer == null)
                throw new ArgumentNullException(nameof(answer), "Answer cannot be null.");

            var entity = MapToEntity(answer);
            await _context.Answers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Answer answer)
        {
            if (answer == null)
                throw new ArgumentNullException(nameof(answer), "Answer cannot be null.");

            var existingEntity = await _context.Answers.FindAsync(answer.Id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Answer with ID {answer.Id} not found.");

            existingEntity.QuestionId = answer.QuestionId;
            existingEntity.AnswerText = answer.AnswerText;
            existingEntity.IsCorrect = answer.IsCorrect;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Answers.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Answer with ID {id} not found.");

            _context.Answers.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Answer>> GetByQuestionId(int questionId)
        {
            return await _context.Answers
                .Where(a => a.QuestionId == questionId)
                .Select(a => MapToModel(a))
                .ToListAsync();
        }

        // Маппінг: Entity -> Model
        private Answer MapToModel(AnswerEntity entity)
        {
            return new Answer
            {
                Id = entity.Id,
                QuestionId = entity.QuestionId,
                AnswerText = entity.AnswerText,
                IsCorrect = entity.IsCorrect
            };
        }

        // Маппінг: Model -> Entity
        private AnswerEntity MapToEntity(Answer model)
        {
            return new AnswerEntity
            {
                Id = model.Id,
                QuestionId = model.QuestionId,
                AnswerText = model.AnswerText,
                IsCorrect = model.IsCorrect
            };
        }
    }
}
