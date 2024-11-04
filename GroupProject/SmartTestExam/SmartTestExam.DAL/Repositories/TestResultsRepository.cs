namespace SmartTestExam.DAL.Repositories
{
    public interface ITestResultsRepository
    {
        IEnumerable<TestResult> GetAll();
        TestResult GetById(int id);
        void Add(TestResult testResult);
        void Update(TestResult testResult);
        void Delete(int id);
        IEnumerable<TestResult> GetByUserId(int userId);
    }

    public class TestResultsRepository : ITestResultsRepository
    {
        private readonly SmartTestExamDbContext _context;

        public TestResultsRepository(SmartTestExamDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TestResult> GetAll()
        {
            return _context.TestResults.ToList();
        }

        public TestResult GetById(int id)
        {
            return _context.TestResults.Find(id);
        }

        public void Add(TestResult testResult)
        {
            _context.TestResults.Add(testResult);
            _context.SaveChanges();
        }

        public void Update(TestResult testResult)
        {
            var existingTestResult = GetById(testResult.Id);
            if (existingTestResult != null)
            {
                existingTestResult.TestId = testResult.TestId;
                existingTestResult.UserId = testResult.UserId;
                existingTestResult.Score = testResult.Score;
                existingTestResult.CompletedAt = testResult.CompletedAt;

                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"TestResult with ID {testResult.Id} not found.");
            }
        }

        public void Delete(int id)
        {
            var testResult = GetById(id);
            if (testResult != null)
            {
                _context.TestResults.Remove(testResult);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"TestResult with ID {id} not found.");
            }
        }

        public IEnumerable<TestResult> GetByUserId(int userId)
        {
            return _context.TestResults.Where(tr => tr.UserId == userId).ToList();
        }
    }
}
