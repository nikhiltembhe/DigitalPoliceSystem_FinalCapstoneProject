using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace DigitalPoliceSystem.xUnitTestProject
{
    public partial class ComplaintCategoriesApiTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ComplaintCategoriesApiTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
    }
}
