using SmartTestExam.DAL.Interfaces;
using SmartTestExam.Models;
using System.Collections.Generic;

namespace SmartTestExam.BLL
{
    public class TestResultsLogic
    {
        private readonly ITestResultsRepository _testResultsRepository;

        public TestResultsLogic(ITestResultsRepository testResultsRepository)
        {
            _testResultsRepository = testResultsRepository;
        }

        public IEnumerable<TestResult> GetAllResults()
        {
            return _testResultsRepository.GetAll();
        }

        public TestResult GetResultById(int id)
        {
            return _testResultsRepository.GetById(id);
        }

        public void AddResult(TestResult testResult)
        {
            _testResultsRepository.Add(testResult);
        }

        public void UpdateResult(TestResult testResult)
        {
            _testResultsRepository.Update(testResult);
        }

        public void DeleteResult(int id)
        {
            _testResultsRepository.Delete(id);
        }

        public IEnumerable<TestResult> GetResultsByUserId(int userId)
        {
            return _testResultsRepository.GetByUserId(userId);
        }
    }
}
