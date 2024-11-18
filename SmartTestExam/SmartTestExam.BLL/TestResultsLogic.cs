using SmartTestExam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using SmartTestExam.DAL.Repositories;

namespace SmartTestExam.BLL
{
    public class TestResultsLogic
    {
        private readonly ITestResultsRepository _testResultsRepository;

        public TestResultsLogic(ITestResultsRepository testResultsRepository)
        {
            _testResultsRepository = testResultsRepository ?? throw new ArgumentNullException(nameof(testResultsRepository), "TestResultsRepository cannot be null.");
        }

        public async Task<IEnumerable<TestResult>> GetAllResults()
        {
            try
            {
                var results = await _testResultsRepository.GetAll();
                if (results == null)
                    throw new InvalidOperationException("No test results found.");

                return results;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving all test results.", ex);
            }
        }

        public async Task<TestResult> GetResultById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Test result ID must be greater than 0.", nameof(id));

            try
            {
                var result = await _testResultsRepository.GetById(id);
                if (result == null)
                    throw new KeyNotFoundException($"Test result with ID {id} not found.");

                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while retrieving the test result with ID {id}.", ex);
            }
        }

        public async Task AddResult(TestResult testResult)
        {
            if (testResult == null)
                throw new ArgumentNullException(nameof(testResult), "TestResult cannot be null.");

            if (testResult.Score < 0 || testResult.Score > 100)
                throw new ArgumentOutOfRangeException(nameof(testResult.Score), "Score must be between 0 and 100.");

            try
            {
                await _testResultsRepository.Add(testResult);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the test result.", ex);
            }
        }

        public async Task UpdateResult(TestResult testResult)
        {
            if (testResult == null)
                throw new ArgumentNullException(nameof(testResult), "TestResult cannot be null.");

            if (testResult.Score < 0 || testResult.Score > 100)
                throw new ArgumentOutOfRangeException(nameof(testResult.Score), "Score must be between 0 and 100.");

            try
            {
                var existingResult = await _testResultsRepository.GetById(testResult.Id);
                if (existingResult == null)
                    throw new KeyNotFoundException($"TestResult with ID {testResult.Id} not found.");

                await _testResultsRepository.Update(testResult);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the test result.", ex);
            }
        }

        public async Task DeleteResult(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Test result ID must be greater than 0.", nameof(id));

            try
            {
                var existingResult = await _testResultsRepository.GetById(id);
                if (existingResult == null)
                    throw new KeyNotFoundException($"TestResult with ID {id} not found.");

                await _testResultsRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while deleting the test result with ID {id}.", ex);
            }
        }

        public async Task<IEnumerable<TestResult>> GetResultsByUserId(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than 0.", nameof(userId));

            try
            {
                var results = await _testResultsRepository.GetByUserId(userId);
                if (results == null)
                    throw new InvalidOperationException($"No test results found for user with ID {userId}.");

                return results;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while retrieving test results for user with ID {userId}.", ex);
            }
        }
    }
}
