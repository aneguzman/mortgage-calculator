using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MortgageCalculator.Controllers;
using MortgageCalculator.Models.Entities;
using MortgageCalculator.Models.Repositories;
using MortgageCalculator.Models.ViewModels;
using MortgageCalculator.Services;

namespace MortgageCalculator.Tests.Services
{
    [TestClass]
    public class MortgageServiceTests
    {
        private Mock<IRepository> repositoryServiceMock;
        private List<MortgageEntry> entryList;
        private MortgageEntry _mortgageEntry;
        private Dictionary<string, string> filters;
        [TestInitialize()]
        public void Setup()
        {
            _mortgageEntry = new MortgageEntry()
            {
                InterestRate = 9,
                Amortization = 25,
                Amount = 250000,
                Created = DateTime.Now,
                Id = 1,
                MonthlyPayment = 2500,
                PaymentFrequency = "Monthly"
            };
            entryList = new List<MortgageEntry>();
            entryList.Add(_mortgageEntry);
            repositoryServiceMock = new Mock<IRepository>();
            filters = new Dictionary<string, string>();
            repositoryServiceMock.Setup(service => service.WhereAllEq<MortgageEntry>(filters, null)).Returns(entryList);
        }


        [TestMethod]
        public void It_Should_Get_The_History_List()
        {
            //Arrange
            var expectedList = entryList;
            MortgageService service = new MortgageService(repositoryServiceMock.Object);

            //Act
            var historyList = service.GetHistory();

            //Assert
            Assert.AreEqual(expectedList.Count, historyList.Count());
        }


        [TestMethod]
        public void It_Should_Get_The_History_List_With_The_Same_Items()
        {
            //Arrange
            var expectedList = entryList;
            MortgageService service = new MortgageService(repositoryServiceMock.Object);

            //Act
            var historyList = service.GetHistory();

            //Assert
            Assert.AreEqual(expectedList.FirstOrDefault().Id, historyList.FirstOrDefault().Id);
        }

        [TestMethod]
        public void It_Should_Get_An_Empty_List()
        {
            //Arrange
            var expectedList = entryList;
            repositoryServiceMock.Setup(s => s.WhereAllEq<MortgageEntry>(filters, null)).Returns(new List<MortgageEntry>());
            MortgageService service = new MortgageService(repositoryServiceMock.Object);

            //Act
            var historyList = service.GetHistory();

            //Assert
            Assert.AreNotEqual(expectedList.Count, historyList.Count());
            Assert.AreEqual(historyList.Count(), 0);
        }

        [TestMethod]
        public void It_Should_Call_Save_When_Saving_An_Entry()
        {
            //Arrange
            repositoryServiceMock.Setup(s => s.Save(It.IsAny<MortgageEntry>())).Verifiable();

            MortgageService service = new MortgageService(repositoryServiceMock.Object);
            
            //Act
            var result = service.SaveCalculationEntry(_mortgageEntry);

            //Assert
            repositoryServiceMock.Verify(r => r.Save(It.IsAny<MortgageEntry>()), Times.Once());
        }

        [TestMethod]
        public void It_Should_Return_False_When_Saving_An_Entry_And_Exception_Is_Thrown()
        {
            //Arrange
            repositoryServiceMock.Setup(s => s.Save(It.IsAny<MortgageEntry>())).Throws(new NotImplementedException());

            MortgageService service = new MortgageService(repositoryServiceMock.Object);

            //Act
            var result = service.SaveCalculationEntry(_mortgageEntry);

            //Assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void It_Should_Return_True_When_Saved_An_Entry()
        {
            //Arrange
            repositoryServiceMock.Setup(s => s.Save(It.IsAny<MortgageEntry>())).Verifiable();

            MortgageService service = new MortgageService(repositoryServiceMock.Object);

            //Act
            var result = service.SaveCalculationEntry(_mortgageEntry);

            //Assert
            Assert.AreEqual(result, true);
        }
    }
}
