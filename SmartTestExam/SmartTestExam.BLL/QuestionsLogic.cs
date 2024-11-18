using SmartTestExam.DAL.Repositories;
using SmartTestExam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartTestExam.BLL
{
    public class QuestionsLogic
    {
        private readonly IQuestionsRepository _questionsRepository;

        public QuestionsLogic(IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }

        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            return await _questionsRepository.GetAll();
        }

        public async Task<Question> GetQuestionById(int id)
        {
            var question = await _questionsRepository.GetById(id);
            if (question == null)
                throw new KeyNotFoundException($"Question with ID {id} not found.");

            return question;
        }

        public async Task AddQuestion(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question), "Question cannot be null.");

            await _questionsRepository.Add(question);
        }

        public async Task UpdateQuestion(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question), "Question cannot be null.");

            await _questionsRepository.Update(question);
        }

        public async Task DeleteQuestion(int id)
        {
            var question = await _questionsRepository.GetById(id);
            if (question == null)
                throw new KeyNotFoundException($"Question with ID {id} not found.");

            await _questionsRepository.Delete(id);
        }

        public async Task<IEnumerable<Question>> GetQuestionsByTestId(int testId)
        {
            return await _questionsRepository.GetByTestId(testId);
        }
    }
}