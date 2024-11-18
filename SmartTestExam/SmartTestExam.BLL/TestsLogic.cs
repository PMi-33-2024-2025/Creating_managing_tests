using SmartTestExam.Models;
using SmartTestExam.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace SmartTestExam.BLL
{
    public class TestsLogic
    {
        private readonly ITestsRepository _testsRepository;

        public TestsLogic(ITestsRepository testsRepository)
        {
            _testsRepository = testsRepository;
        }

        public async Task<IEnumerable<Test>> GetAllTests()
        {
            return await _testsRepository.GetAll();
        }

        public async Task<Test> GetTestById(int id)
        {
            var test = await _testsRepository.GetById(id);
            if (test == null)
                throw new KeyNotFoundException($"Test with ID {id} not found.");

            return test;
        }

        public async Task AddTest(Test test)
        {
            if (test == null)
                throw new ArgumentNullException(nameof(test), "Test cannot be null.");

            var existingTest = await _testsRepository.GetById(test.Id);
            if (existingTest != null)
                throw new InvalidOperationException($"Test with ID {test.Id} already exists.");

            await _testsRepository.Add(test);
        }

        public async Task UpdateTest(Test test)
        {
            if (test == null)
                throw new ArgumentNullException(nameof(test), "Test cannot be null.");

            var existingTest = await _testsRepository.GetById(test.Id);
            if (existingTest == null)
                throw new KeyNotFoundException($"Test with ID {test.Id} not found.");

            await _testsRepository.Update(test);
        }

        public async Task DeleteTest(int id)
        {
            var test = await _testsRepository.GetById(id);
            if (test == null)
                throw new KeyNotFoundException($"Test with ID {id} not found.");

            await _testsRepository.Delete(id);
        }
    }
}
