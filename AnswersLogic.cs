using SmartTestExam.DAL.Interfaces;
using SmartTestExam.Models;
using System.Collections.Generic;

namespace SmartTestExam.BLL
{
    public class AnswersLogic
    {
        private readonly IAnswersRepository _answersRepository;

        public AnswersLogic(IAnswersRepository answersRepository)
        {
            _answersRepository = answersRepository;
        }

        public IEnumerable<Answer> GetAllAnswers()
        {
            return _answersRepository.GetAll();
        }

        public Answer GetAnswerById(int id)
        {
            return _answersRepository.GetById(id);
        }

        public void AddAnswer(Answer answer)
        {
            _answersRepository.Add(answer);
        }

        public void UpdateAnswer(Answer answer)
        {
            _answersRepository.Update(answer);
        }

        public void DeleteAnswer(int id)
        {
            _answersRepository.Delete(id);
        }

        public IEnumerable<Answer> GetAnswersByQuestionId(int questionId)
        {
            return _answersRepository.GetByQuestionId(questionId);
        }
    }
}
