using Canvia.Core.Interfaces;
using Canvia.Test.Mocks;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace Canvia.Test
{
    public class PeopleControllerTest
    {
        private Mock<IPeopleService> _mockPeopleService;

        [SetUp]
        public void Setup()
        {
            //Mocking
            _mockPeopleService = new Mock<IPeopleService>();
            //Arrange
            _mockPeopleService.Setup(x => x.GetAll()).Returns(DataMock.GetAll());
        }

        [Test]
        public void GetAll()
        {
            //Act
            var result = this._mockPeopleService.Object.GetAll();

            //Assert
            Assert.IsTrue(result.Count() == 1);
        }
    }
}