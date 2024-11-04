namespace SmartTestExam.DAL.Repositories
{
    public interface ITestsRepository
    {
        IEnumerable<Test> GetAll();
        Test GetById(int id);
        void Add(Test test);
        void Update(Test test);
        void Delete(int id);
    }

    public class TestsRepository : ITestsRepository
    {
        private readonly SmartTestExamDbContext _context;

        public TestsRepository(SmartTestExamDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Test> GetAll()
        {
            return _context.Tests.ToList();
        }

        public Test GetById(int id)
        {
            return _context.Tests.Find(id);
        }

        public void Add(Test test)
        {
            _context.Tests.Add(test);
            _context.SaveChanges();
        }

        public void Update(Test test)
        {
            var existingTest = GetById(test.Id);
            if (existingTest != null)
            {
                existingTest.Title = test.Title;
                existingTest.Code = test.Code;
                existingTest.AuthorId = test.AuthorId;
                existingTest.CreatedAt = test.CreatedAt;

                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"Test with ID {test.Id} not found.");
            }
        }

        public void Delete(int id)
        {
            var test = GetById(id);
            if (test != null)
            {
                _context.Tests.Remove(test);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"Test with ID {id} not found.");
            }
        }
    }
}
