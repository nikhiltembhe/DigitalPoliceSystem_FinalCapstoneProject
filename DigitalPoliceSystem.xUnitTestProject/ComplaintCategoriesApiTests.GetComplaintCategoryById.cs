using DigitalPoliceSystem.Controllers;
using DigitalPoliceSystem.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DigitalPoliceSystem.xUnitTestProject
{
    public partial class ComplaintCategoriesApiTests
    {
        [Fact]
        public void GetComplaintCategoryById_NotFoundResult()
        {
            // ARRANGE
            var dbName = nameof(ComplaintCategoriesApiTests.GetComplaintCategoryById_NotFoundResult);
            var logger = Mock.Of<ILogger<ComplaintCategoriesController>>();

            // using (var db = DbContextMocker.GetApplicationDbContext(dbName))
            // {
            // }           // db.Dispose(); implicitly called when you exit the USING Block

            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);    // Disposable!
            var apiController = new ComplaintCategoriesController(dbContext, logger);
            int findCategoryID = 900;

            // ACT
            IActionResult actionResultGet = apiController.GetComplaintCategory(findCategoryID).Result;

            // ASSERT - check if the IActionResult is NotFound 
            Assert.IsType<NotFoundResult>(actionResultGet);

            // ASSERT - check if the Status Code is (HTTP 404) "NotFound"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound;
            var actualStatusCode = (actionResultGet as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetComplaintCategoryById_BadRequestResult()
        {
            // ARRANGE
            var dbName = nameof(ComplaintCategoriesApiTests.GetComplaintCategoryById_BadRequestResult);
            var logger = Mock.Of<ILogger<ComplaintCategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);    // Disposable!
            var controller = new ComplaintCategoriesController(dbContext, logger);
            int? findCategoryID = null;

            // ACT
            IActionResult actionResultGet = controller.GetComplaintCategory(findCategoryID).Result;

            // ASSERT - check if the IActionResult is BadRequest
            Assert.IsType<BadRequestResult>(actionResultGet);

            // ASSERT - check if the Status Code is (HTTP 400) "BadRequest"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            var actualStatusCode = (actionResultGet as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetComplaintCategoryById_OkResult()
        {
            // ARRANGE
            var dbName = nameof(ComplaintCategoriesApiTests.GetComplaintCategoryById_OkResult);
            var logger = Mock.Of<ILogger<ComplaintCategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);    // Disposable!
            var controller = new ComplaintCategoriesController(dbContext, logger);
            int findCategoryID = 3;

            // ACT
            IActionResult actionResultGet = controller.GetComplaintCategory(findCategoryID).Result;

            // ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultGet);

            // ASSERT - if Status Code is HTTP 200 (Ok)
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultGet as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetComplaintCategoryById_CorrectResult()
        {
            // ARRANGE
            var dbName = nameof(ComplaintCategoriesApiTests.GetComplaintCategoryById_CorrectResult);
            var logger = Mock.Of<ILogger<ComplaintCategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var controller = new ComplaintCategoriesController(dbContext, logger);
            int findCategoryID = 2;
            ComplaintCategory expectedCategory = DbContextMocker.TestData_Categories
                                        .SingleOrDefault(c => c.ComplaintCategoryId == findCategoryID);

            // ACT
            IActionResult actionResultGet = controller.GetComplaintCategory(findCategoryID).Result;

            // ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultGet);

            // ASSERT - if IActionResult (i.e., OkObjectResult) contains an object of the type Category
            OkObjectResult okResult = actionResultGet.Should().BeOfType<OkObjectResult>().Subject;
            Assert.IsType<ComplaintCategory>(okResult.Value);

            // Extract the category object from the result.
            ComplaintCategory actualCategory = okResult.Value.Should().BeAssignableTo<ComplaintCategory>().Subject;
            _testOutputHelper.WriteLine($"Found: CategoryID == {actualCategory.ComplaintCategoryId}");

            // ASSERT - if category is NOT NULL
            Assert.NotNull(actualCategory);

            // ASSERT - if the CategoryId is containing the expected data.
            Assert.Equal<int>(expected: expectedCategory.ComplaintCategoryId,
                              actual: actualCategory.ComplaintCategoryId);

            // ASSERT - if the CateogoryName is correct 
            Assert.Equal(expected: expectedCategory.CompliantCategoryName,
                         actual: actualCategory.CompliantCategoryName);
        }
    }
}
