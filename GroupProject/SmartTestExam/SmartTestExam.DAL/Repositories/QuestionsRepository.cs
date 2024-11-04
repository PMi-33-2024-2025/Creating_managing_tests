namespace SmartTestExam.DAL.Repositories
{
    public interface IQuestionsRepository
    {
        IEnumerable<Question> GetAll();
        Question GetById(int id);
        void Add(Question question);
        void Update(Question question);
        void Delete(int id);
        IEnumerable<Question> GetByTestId(int testId);
    }

    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly SmartTestExamDbContext _context;

        public QuestionsRepository(SmartTestExamDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Question> GetAll()
        {
            return _context.Questions.ToList();
        }

        public Question GetById(int id)
        {
            return _context.Questions.Find(id);
        }

        public void Add(Question question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();
        }

        public void Update(Question question)
        {
            var existingQuestion = GetById(question.Id);
            if (existingQuestion != null)
            {
                existingQuestion.TestId = question.TestId;
                existingQuestion.QuestionText = question.QuestionText;
                existingQuestion.QuestionType = question.QuestionType;

                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"Question with ID {question.Id} not found.");
            }
        }

        public void Delete(int id)
        {
            var question = GetById(id);
            if (question != null)
            {
                _context.Questions.Remove(question);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"Question with ID {id} not found.");
            }
        }

        public IEnumerable<Question> GetByTestId(int testId)
        {
            return _context.Questions.Where(q => q.TestId == testId).ToList();
        }
    }
}
