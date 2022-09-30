using DigitalPoliceSystem.Controllers;
using DigitalPoliceSystem.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DigitalPoliceSystem.xUnitTestProject
{
    public partial class ComplaintCategoriesApiTests
    {
        [Fact]

        public void GetComplaintCategories_OkResult()
        {
            // ARRANGE
            var dbName = nameof(ComplaintCategoriesApiTests.GetComplaintCategories_OkResult);
            var logger = Mock.Of<ILogger<ComplaintCategoriesController>>();
            var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var apiController = new ComplaintCategoriesController(dbContext, logger);

            // ACT
            IActionResult actionResult = apiController.GetComplaintCategories().Result;

            // ASSERT
            // --- Check if the ActionResult is of the type OkObjectResult
            Assert.IsType<OkObjectResult>(actionResult);

            // --- Check if the HTTP Response Code is 200 "Ok"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            int actualStatusCode = (actionResult as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetComplaintCategories_CheckCorrectResult()
        {
            // ARRANGE
            var dbName = nameof(ComplaintCategoriesApiTests.GetComplaintCategories_CheckCorrectResult);
            var logger = Mock.Of<ILogger<ComplaintCategoriesController>>();
            var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var apiController = new ComplaintCategoriesController(dbContext, logger);

            // ACT
            IActionResult actionResult = apiController.GetComplaintCategories().Result;

            // ASSERT
            // --- Check if the ActionResult is of the type OkObjectResult
            Assert.IsType<OkObjectResult>(actionResult);


            // ACT: Extract the OkResult result 
            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;

            // ASSERT: if the OkResult contains the object of the Correct Type
            Assert.IsAssignableFrom<List<ComplaintCategory>>(okResult.Value);








            // ACT: Extract the Categories from the result of the action
            var complaintCategoriesFromApi = okResult.Value.Should().BeAssignableTo<List<ComplaintCategory>>().Subject;

            // ASSERT: if the categories is NOT NULL
            Assert.NotNull(complaintCategoriesFromApi);

            // ASSERT: if the number of categories in the DbContext seed data
            //         is the same as the number of categories returned in the API Result
            Assert.Equal<int>(expected: DbContextMocker.TestData_Categories.Length,
                              actual: complaintCategoriesFromApi.Count);

            // ASSERT: Test the data received from the API against the Seed Data
            int ndx = 0;
            foreach (ComplaintCategory complaintCategory in DbContextMocker.TestData_Categories)
            {
                // ASSERT: check if the Category ID is correct
                Assert.Equal<int>(expected: complaintCategory.ComplaintCategoryId,
                                  actual: complaintCategoriesFromApi[ndx].ComplaintCategoryId);

                // ASSERT: check if the Category Name is correct
                Assert.Equal(expected: complaintCategory.CompliantCategoryName,
                             actual: complaintCategoriesFromApi[ndx].CompliantCategoryName);

                _testOutputHelper.WriteLine($"Compared Row # {ndx} successfully");

                ndx++;          // now compare against the next element in the array
            }

        }
    }
}
