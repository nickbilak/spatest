using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;
using Vacations.Business.Services.Contracts;
using Vacations.Business.Services.Implementations;
using Vacations.DataAccess.Contracts;
using Vacations.DataAccess.Enums;
using Vacations.DataAccess.Implementations;
using Vacations.DataAccess.Models;

namespace Vacations.Business.Tests.Services.Implementations
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class EmployeeServiceTest
    {
        private readonly IEmployeeService _employeeService = new EmployeeService();

        [Test]
        public void GetEmployeesTest()
        {
            //Arrange
            List<EmployeeModel> fakeListEmployeeModels = new List<EmployeeModel>();
            var fakeEmployee1 = new EmployeeModel()
            {
                Id = 1,
                Name = "Utfake",
                Surname = "Fake1",
                PasswordClear = "fTest",
                Type = EmployeeType.Ordinal,
                Email = "fktest1@fake.com",
                StartDate = new DateTime(2014, 02, 25),
                EndDate = new DateTime(2016, 02, 25),
            };

            var fakeEmployee2 = new EmployeeModel()
            {
                Id = 1,
                Name = "Utfake",
                Surname = "Fake2",
                PasswordClear = "fTest",
                Type = EmployeeType.Ordinal,
                Email = "fktest2@fake.com",
                StartDate = new DateTime(2014, 02, 25),
                EndDate = new DateTime(2016, 02, 25),
            };

            fakeListEmployeeModels.Add(fakeEmployee1);
            fakeListEmployeeModels.Add(fakeEmployee2);

            Mock<IEmployeeRepository> repository = new Mock<IEmployeeRepository>();
            repository.Setup(c => c.GetEmployees()).Returns(fakeListEmployeeModels);

            var employeeService = new EmployeeService(repository.Object);

            //Act
            var result = employeeService.GetEmployees();

            //Assert
            Assert.AreEqual(result.Count(), 2);
        }

        /// <summary>
        /// Test the add of employee
        /// </summary>
        /// <param name="error">Error return by the repository</param>
        /// <param name="expected">Result expected</param>
        [TestCase("", "")]
        [TestCase("EmailAlreadyExist", "EmailAlreadyExist")]
        public void Add_Employee_Test(string error, string expected)
        {
            //Arrange

            var fakeEmployee = new EmployeeModel()
            {
                Id = 1,
                Name = "Utfake",
                Surname = "Fake1",
                PasswordClear = "fTest",
                Type = EmployeeType.Ordinal,
                Email = "fktest1@fake.com",
                StartDate = new DateTime(2014, 02, 25),
                EndDate = new DateTime(2016, 02, 25),
            };

            Mock<IEmployeeRepository> repository = new Mock<IEmployeeRepository>();
            repository.Setup(c => c.Add(It.IsAny<EmployeeModel>())).Returns(error);


            var employeeService = new EmployeeService(repository.Object);

            //Act
            var result = employeeService.Add(fakeEmployee);

            //Assert
            Assert.AreEqual(result, expected);

        }

        /// <summary>
        /// Test the argument exception of add a employee
        /// </summary>
        /// <param name="expectedException">Type of exception</param>
        /// <param name="expectedVar">Expected result</param>
        [TestCase(typeof(ArgumentNullException), "employee")]
        public void Add_Employee_ArgumentNullException_Test(Type expectedException, string expectedVar)
        {
            //Arrange
            Mock<IEmployeeRepository> repository = new Mock<IEmployeeRepository>();


            var employeeService = new EmployeeService(repository.Object);

            //Assert
            var ex = Assert.Throws(expectedException, () => employeeService.Add(null));
            Assert.That(ex.Message.Contains(expectedVar));

        }

        /// <summary>
        /// get random password test
        /// </summary>
        [Test]
        public void GeneratePasswordTest()
        {
            // Arrange
            int passwordLength = 8;
            // Act
            var result = _employeeService.GeneratePassword(passwordLength);

            // Assert
            Assert.AreEqual(passwordLength, result.Length);

            //8 characters minimum with lowercase, uppercase, number and / or special characters
            string pattern = @"(?=^.{6,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?!.*/)(?=.*[A-Z])(?=.*[a-z]).*$";
            Regex r = new Regex(pattern);
            var m = r.Match(result);

            Assert.IsTrue(m.Success);

        }

    }
}
