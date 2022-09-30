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
        public void InsertComplaintCategory_OkResult()
        {
            // ARRANGE
            var dbName = nameof(ComplaintCategoriesApiTests.InsertComplaintCategory_OkResult);
            var logger = Mock.Of<ILogger<ComplaintCategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new ComplaintCategoriesController(dbContext, logger);
            ComplaintCategory categoryToAdd = new ComplaintCategory
            {
                ComplaintCategoryId = 5,
                CompliantCategoryName = "New Category"             // IF = null, then: INVALID!  CategoryName is REQUIRED
            };

            // ACT
            IActionResult actionResultPost = apiController.PostComplaintCategory(categoryToAdd).Result;

            // ASSERT - check if the IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultPost);

            // ASSERT - check if the Status Code is (HTTP 200) "Ok", (HTTP 201 "Created")
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultPost as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);

            // Extract the result from the IActionResult object.
            var postResult = actionResultPost.Should().BeOfType<OkObjectResult>().Subject;

            // ASSERT - if the result is a CreatedAtActionResult
            Assert.IsType<CreatedAtActionResult>(postResult.Value);

            // Extract the inserted Category object
            ComplaintCategory actualCategory = (postResult.Value as CreatedAtActionResult).Value
                                      .Should().BeAssignableTo<ComplaintCategory>().Subject;

            // ASSERT - if the inserted Category object is NOT NULL
            Assert.NotNull(actualCategory);

            Assert.Equal(categoryToAdd.ComplaintCategoryId, actualCategory.ComplaintCategoryId);
            Assert.Equal(categoryToAdd.CompliantCategoryName, actualCategory.CompliantCategoryName);
        }
    }
}
