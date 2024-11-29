using SmartTestExam.DAL.Interfaces;
using SmartTestExam.Models;
using System.Collections.Generic;

namespace SmartTestExam.BLL
{
    public class TestsLogic
    {
        private readonly ITestsRepository _testsRepository;

        public TestsLogic(ITestsRepository testsRepository)
        {
            _testsRepository = testsRepository;
        }

        public IEnumerable<Test> GetAllTests()
        {
            return _testsRepository.GetAll();
        }

        public Test GetTestById(int id)
        {
            return _testsRepository.GetById(id);
        }

        public void AddTest(Test test)
        {
            _testsRepository.Add(test);
        }

        public void UpdateTest(Test test)
        {
            _testsRepository.Update(test);
        }

        public void DeleteTest(int id)
        {
            _testsRepository.Delete(id);
        }
    }
}
