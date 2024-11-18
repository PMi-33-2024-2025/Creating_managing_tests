using Microsoft.EntityFrameworkCore;
using SmartTestExam.Models;

namespace SmartTestExam.DAL.Repositories
{
    public interface ITestsRepository
    {
        Task<IEnumerable<Test>> GetAll();
        Task<Test> GetById(int id);
        Task Add(Test test);
        Task Update(Test test);
        Task Delete(int id);
    }

    public class TestsRepository : ITestsRepository
    {
        private readonly SmartTestExamDbContext _context;

        public TestsRepository(SmartTestExamDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Test>> GetAll()
        {
            var testEntities = await _context.Tests.ToListAsync();
            return testEntities.Select(MapToModel);
        }

        public async Task<Test> GetById(int id)
        {
            var entity = await _context.Tests.FindAsync(id); 
            if (entity == null)
                throw new KeyNotFoundException($"Test with ID {id} not found.");

            return MapToModel(entity);
        }

        public async Task Add(Test test)
        {
            if (test == null)
                throw new ArgumentNullException(nameof(test), "Test cannot be null.");

            var entity = MapToEntity(test);
            await _context.Tests.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Test test)
        {
            if (test == null)
                throw new ArgumentNullException(nameof(test), "Test cannot be null.");

            var existingTest = await GetById(test.Id);
            if (existingTest != null)
            {
                existingTest.Title = test.Title;
                existingTest.Code = test.Code;
                existingTest.AuthorId = test.AuthorId;
                existingTest.CreatedAt = test.CreatedAt;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Test with ID {test.Id} not found.");
            }
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Tests.FindAsync(id);
            if (entity != null)
            {
                _context.Tests.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Test with ID {id} not found.");
            }
        }

        // Мапінг: Entity -> Model
        private Test MapToModel(TestEntity entity)
        {
            return new Test
            {
                Id = entity.Id,
                Title = entity.Title,
                Code = entity.Code,
                AuthorId = entity.AuthorId,
                CreatedAt = entity.CreatedAt
            };
        }

        // Мапінг: Model -> Entity
        private TestEntity MapToEntity(Test model)
        {
            return new TestEntity
            {
                Id = model.Id,
                Title = model.Title,
                Code = model.Code,
                AuthorId = model.AuthorId,
                CreatedAt = model.CreatedAt
            };
        }
    }
}
