namespace SmartTestExam.DAL.Repositories
{
    public interface IAnswersRepository
    {
        IEnumerable<Answer> GetAll();
        Answer GetById(int id);
        void Add(Answer answer);
        void Update(Answer answer);
        void Delete(int id);
        IEnumerable<Answer> GetByQuestionId(int questionId);
    }

    public class AnswersRepository : IAnswersRepository
    {
        private readonly SmartTestExamDbContext _context;

        public AnswersRepository(SmartTestExamDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Answer> GetAll()
        {
            return _context.Answers.ToList();
        }

        public Answer GetById(int id)
        {
            return _context.Answers.Find(id);
        }

        public void Add(Answer answer)
        {
            _context.Answers.Add(answer);
            _context.SaveChanges();
        }

        public void Update(Answer answer)
        {
            var existingAnswer = GetById(answer.Id);
            if (existingAnswer != null)
            {
                existingAnswer.QuestionId = answer.QuestionId;
                existingAnswer.AnswerText = answer.AnswerText;
                existingAnswer.IsCorrect = answer.IsCorrect;

                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"Answer with ID {answer.Id} not found.");
            }
        }

        public void Delete(int id)
        {
            var answer = GetById(id);
            if (answer != null)
            {
                _context.Answers.Remove(answer);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"Answer with ID {id} not found.");
            }
        }

        public IEnumerable<Answer> GetByQuestionId(int questionId)
        {
            return _context.Answers.Where(a => a.QuestionId == questionId).ToList();
        }
    }
}
