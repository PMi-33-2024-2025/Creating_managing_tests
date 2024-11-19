using SmartTestExam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using SmartTestExam.DAL.Repositories;

namespace SmartTestExam.BLL
{
    public class AnswersLogic
    {
        private readonly IAnswersRepository _answersRepository;

        public AnswersLogic(IAnswersRepository answersRepository)
        {
            _answersRepository = answersRepository;
        }

        public async Task<IEnumerable<Answer>> GetAllAnswers()
        {
            return await _answersRepository.GetAll();
        }

        public async Task<Answer> GetAnswerById(int id)
        {
            var answer = await _answersRepository.GetById(id);
            if (answer == null)
                throw new KeyNotFoundException($"Answer with ID {id} not found.");

            return answer;
        }

        public async Task AddAnswer(Answer answer)
        {
            if (answer == null)
                throw new ArgumentNullException(nameof(answer), "Answer cannot be null.");

            await _answersRepository.Add(answer);
        }

        public async Task UpdateAnswer(Answer answer)
        {
            if (answer == null)
                throw new ArgumentNullException(nameof(answer), "Answer cannot be null.");

            var existingAnswer = await _answersRepository.GetById(answer.Id);
            if (existingAnswer == null)
                throw new KeyNotFoundException($"Answer with ID {answer.Id} not found.");

            await _answersRepository.Update(answer);
        }

        public async Task DeleteAnswer(int id)
        {
            var answer = await _answersRepository.GetById(id);
            if (answer == null)
                throw new KeyNotFoundException($"Answer with ID {id} not found.");

            await _answersRepository.Delete(id);
        }

        public async Task<IEnumerable<Answer>> GetAnswersByQuestionId(int questionId)
        {
            return await _answersRepository.GetByQuestionId(questionId);
        }
    }
}
