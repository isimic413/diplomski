using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service;
using ExamPreparation.Service.Common;
using System.Threading.Tasks;

namespace ExamPreparation.Service.Tests
{
    [TestClass]
    public class TestingAreaTests
    {
        private List<ITestingArea> Areas;


        [TestInitialize]
        public void Setup()
        {
            Areas = new List<ITestingArea>()
            {
                new TestingArea()
                {
                    Title = "Engleski jezik - visa razina",
                    Abrv = "Eng-1",
                    Id = new Guid("992a69de-f418-4f7d-a0d6-ee533736d6f8")
                },

                new TestingArea()
                {
                    Title = "Informatika",
                    Abrv = "Inf",
                    Id = Guid.NewGuid()
                },

                new TestingArea()
                {
                    Title = "Matematika - visa razina",
                    Abrv = "Mat-1",
                    Id = Guid.NewGuid()
                }
            };
        }

        [TestMethod]
        public void GetAllTest()
        {
            var filter = new TestingAreaFilter("Abrv", 1, 3);

            var mock = new Mock<ITestingAreaRepository>();
            mock.Setup(x => x.GetAsync(filter)).ReturnsAsync(Areas);

            var repository = mock.Object;
            var service = new TestingAreaService(repository);
            var result = service.GetAsync(filter);

            Assert.IsNotNull(result.Result);
            Assert.AreEqual((result.Result)[0].Abrv, "Eng-1");
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var mock = new Mock<ITestingAreaRepository>();
            mock.Setup(x => x.GetAsync(new Guid("992a69de-f418-4f7d-a0d6-ee533736d6f8"))).ReturnsAsync(Areas[0]);

            var repository = mock.Object;
            var service = new TestingAreaService(repository);
            var result = service.GetAsync(new Guid("992a69de-f418-4f7d-a0d6-ee533736d6f8"));

            Assert.IsNotNull(result.Result);
            Assert.AreEqual((result.Result).Abrv, "Eng-1");
        }
    }
}
