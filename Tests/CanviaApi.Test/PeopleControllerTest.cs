using Canvia.Core.Interfaces;
using CanviaApi.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanviaApi.Test
{
    [TestClass]
    public class PeopleControllerTest
    {
        private Mock<IPeopleService> _mockPeopleService;

        [TestInitialize]
        public void InicializarTest()
        {
            //Mocking
            _mockPeopleService = new Mock<IPeopleService>();
            //Arrange
            _mockPeopleService.Setup(x => x.GetAll()).Returns(DataMock.GetAll());
        }

        [TestMethod]
        public void GetAll()
        {
            //Act
            var result = this._mockPeopleService.Object.GetAll();

            //Assert
            Assert.IsTrue(result.Count() == 1);
        }
    }
}
