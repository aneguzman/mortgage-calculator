using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MortgageCalculator.Controllers;
using MortgageCalculator.Models;
using MortgageCalculator.Models.Entities;
using MortgageCalculator.Models.ViewModels;
using MortgageCalculator.Services;

namespace MortgageCalculator.Tests.Controllers
{
    [TestClass]
    public class MortgageControllerTest
    {
        private MortgageEntryViewModel _mortgageEntry;
        private string _email;
        private Mock<IMortgageService> mortageServiceMock;
        private List<MortgageEntry> entryList;

        [TestInitialize()]
        public void Setup()
        {
            _mortgageEntry = new MortgageEntryViewModel()
            {
                InterestRate = 9,
                Amortization = 25,
                Amount = 250000,
                Created = DateTime.Now,
                Id = 1,
                MonthlyPayment = 2500,
                PaymentFrequency = "Monthly"
            };
            _email = "test@test.com";
            entryList = new List<MortgageEntry>();
            entryList.Add(_mortgageEntry);
            mortageServiceMock = new Mock<IMortgageService>();
            mortageServiceMock.Setup(service => service.SendEmail(_mortgageEntry, _email)).Returns(true);
            mortageServiceMock.Setup(service => service.SaveCalculationEntry(It.IsAny<MortgageEntry>())).Returns(true);
            mortageServiceMock.Setup(service => service.GetHistory()).Returns(entryList);
        }

        [TestMethod]
        public void It_Should_Get_Successfull_Response_And_Message_After_Successfull_Email_Sent()
        {
            //Arrange
            var expectedResult = new CustomJsonModel{
                Success = true,
                Message = "Email sent successfully"
            };
            MortgageController controller = new MortgageController(mortageServiceMock.Object);

            //Act
            var controllerResult = controller.SendMail(_mortgageEntry, _email);
            CustomJson jsonResult = controllerResult as CustomJson;

            //Assert
            Assert.AreEqual(expectedResult.Success, jsonResult.Data.Success);
            Assert.AreEqual(expectedResult.Message, jsonResult.Data.Message);
        }

        [TestMethod]
        public void It_Should_Get_Not_Successfull_Message_And_Response_After_Failed_Email_Sent()
        {
            //Arrange
            var expectedResult = new CustomJsonModel
            {
                Success = false,
                Message = "There was an error when processing your request. Please try again"
            };
            mortageServiceMock.Setup(service => service.SendEmail(_mortgageEntry, _email)).Returns(false);
            MortgageController controller = new MortgageController(mortageServiceMock.Object);

            //Act
            var controllerResult = controller.SendMail(_mortgageEntry, _email);
            CustomJson jsonResult = controllerResult as CustomJson;

            //Assert
            Assert.AreEqual(expectedResult.Message, jsonResult.Data.Message);
            Assert.AreEqual(expectedResult.Success, jsonResult.Data.Success);
        }

        [TestMethod]
        public void It_Should_Get_Not_Successfull_Message_And_Response_When_Sent_Email_After_Bad_ViewModel_Data_Passed()
        {
            //Arrange
            var expectedResult = new CustomJsonModel
            {
                Success = false,
                Message = "There was an error when processing your request. Please try again"
            };
            var antoherMortageEntry = new MortgageEntryViewModel()
            {
                InterestRate = 9,
            };
            MortgageController controller = new MortgageController(mortageServiceMock.Object);

            //Act
            var controllerResult = controller.SendMail(antoherMortageEntry, _email);
            CustomJson jsonResult = controllerResult as CustomJson;

            //Assert
            Assert.AreEqual(expectedResult.Message, jsonResult.Data.Message);
        }

        [TestMethod]
        public void It_Should_Get_Successfull_Response_After_Successfull_Add_Calculation_Entry()
        {
            //Arrange
            var expectedResult = new CustomJsonModel
            {
                Success = true,
            };
            MortgageController controller = new MortgageController(mortageServiceMock.Object);

            //Act
            var controllerResult = controller.AddCalculationEntry(_mortgageEntry);
            CustomJson jsonResult = controllerResult as CustomJson;

            //Assert
            Assert.AreEqual(expectedResult.Success, jsonResult.Data.Success);

        }

        [TestMethod]
        public void It_Should_Get_Not_Successfull_Message_And_Response_After_Failed_Entry_Added()
        {
            //Arrange
            var expectedResult = new CustomJsonModel
            {
                Success = false,
                Message = "There was an error when processing your request. Please try again"
            };
            mortageServiceMock.Setup(service => service.SaveCalculationEntry(It.IsAny<MortgageEntry>())).Returns(false);
            MortgageController controller = new MortgageController(mortageServiceMock.Object);

            //Act
            var controllerResult = controller.AddCalculationEntry(_mortgageEntry);
            CustomJson jsonResult = controllerResult as CustomJson;

            //Assert
            Assert.AreEqual(expectedResult.Message, jsonResult.Data.Message);
            Assert.AreEqual(expectedResult.Success, jsonResult.Data.Success);
        }

        [TestMethod]
        public void It_Should_Get_Successfull_Response_And_History_List_After_Successfull_Get_Entry_List()
        {
            //Arrange
            var expectedResult = new CustomJsonModel
            {
                Success = true,
                History = entryList
            };
            MortgageController controller = new MortgageController(mortageServiceMock.Object);

            //Act
            var controllerResult = controller.GetHistoryList();
            CustomJson jsonResult = controllerResult as CustomJson;

            //Assert
            Assert.AreEqual(expectedResult.Success, jsonResult.Data.Success);
            Assert.AreEqual(expectedResult.History.Count(), jsonResult.Data.History.Count());
        }

        [TestMethod]
        public void It_Should_Get_Successfull_Response_And_Clean_History_List_After_Get_Entry_List_With_No_Entries()
        {
            //Arrange
            var expectedResult = new CustomJsonModel
            {
                Success = true,
                History = new List<MortgageEntry>()
            };
            mortageServiceMock.Setup(service => service.GetHistory()).Returns(new List<MortgageEntry>());
            MortgageController controller = new MortgageController(mortageServiceMock.Object);

            //Act
            var controllerResult = controller.GetHistoryList();
            CustomJson jsonResult = controllerResult as CustomJson;

            //Assert
            Assert.AreEqual(expectedResult.Success, jsonResult.Data.Success);
            Assert.AreEqual(expectedResult.History.Count(), jsonResult.Data.History.Count());
        }

    }
}
