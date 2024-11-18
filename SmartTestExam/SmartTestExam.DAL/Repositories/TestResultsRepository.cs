using Microsoft.EntityFrameworkCore;
using SmartTestExam.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace SmartTestExam.DAL.Repositories
{
    public interface ITestResultsRepository
    {
        Task<IEnumerable<TestResult>> GetAll();
        Task<TestResult> GetById(int id);
        Task Add(TestResult testResult);
        Task Update(TestResult testResult);
        Task Delete(int id);
        Task<IEnumerable<TestResult>> GetByUserId(int userId);
    }

    public class TestResultsRepository : ITestResultsRepository
    {
        private readonly SmartTestExamDbContext _context;

        public TestResultsRepository(SmartTestExamDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TestResult>> GetAll()
        {
            var testResultEntities = await _context.TestResults.ToListAsync();
            return testResultEntities.Select(MapToModel);
        }

        public async Task<TestResult> GetById(int id)
        {
            var entity = await _context.TestResults.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"TestResult with ID {id} not found.");

            return MapToModel(entity); 
        }

        public async Task Add(TestResult testResult)
        {
            if (testResult == null)
                throw new ArgumentNullException(nameof(testResult), "TestResult cannot be null.");

            var entity = MapToEntity(testResult);
            await _context.TestResults.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TestResult testResult)
        {
            if (testResult == null)
                throw new ArgumentNullException(nameof(testResult), "TestResult cannot be null.");

            var existingTestResult = await GetById(testResult.Id);
            if (existingTestResult == null)
                throw new KeyNotFoundException($"TestResult with ID {testResult.Id} not found.");

            var entity = MapToEntity(testResult); 
            _context.TestResults.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.TestResults.FindAsync(id);
            if (entity != null)
            {
                _context.TestResults.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"TestResult with ID {id} not found.");
            }
        }

        public async Task<IEnumerable<TestResult>> GetByUserId(int userId)
        {
            var testResultEntities = await _context.TestResults
                .Where(tr => tr.UserId == userId)
                .ToListAsync();
            return testResultEntities.Select(MapToModel); 
        }

        // Мапінг: Entity -> Model
        private TestResult MapToModel(TestResultEntity entity)
        {
            return new TestResult
            {
                Id = entity.Id,
                TestId = entity.TestId,
                UserId = entity.UserId,
                Score = entity.Score,
                CompletedAt = entity.CompletedAt
            };
        }

        // Мапінг: Model -> Entity
        private TestResultEntity MapToEntity(TestResult model)
        {
            return new TestResultEntity
            {
                Id = model.Id,
                TestId = model.TestId,
                UserId = model.UserId,
                Score = model.Score ?? 0,
                CompletedAt = model.CompletedAt
            };
        }
    }
}
