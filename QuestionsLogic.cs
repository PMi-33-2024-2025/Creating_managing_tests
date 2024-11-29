using SmartTestExam.DAL.Interfaces;
using SmartTestExam.Models;
using System.Collections.Generic;

namespace SmartTestExam.BLL
{
    public class QuestionsLogic
    {
        private readonly IQuestionsRepository _questionsRepository;

        public QuestionsLogic(IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return _questionsRepository.GetAll();
        }

        public Question GetQuestionById(int id)
        {
            return _questionsRepository.GetById(id);
        }

        public void AddQuestion(Question question)
        {
            _questionsRepository.Add(question);
        }

        public void UpdateQuestion(Question question)
        {
            _questionsRepository.Update(question);
        }

        public void DeleteQuestion(int id)
        {
            _questionsRepository.Delete(id);
        }

        public IEnumerable<Question> GetQuestionsByTestId(int testId)
        {
            return _questionsRepository.GetByTestId(testId);
        }
    }
}
