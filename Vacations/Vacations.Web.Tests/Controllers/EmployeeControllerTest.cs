using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Vacations.Business.Services.Contracts;
using Vacations.Business.Services.Implementations;
using Vacations.DataAccess.Enums;
using Vacations.DataAccess.Models;
using Vacations.Web.Controllers;

namespace Vacations.Web.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class EmployeeControllerTest
    {

        private IEncryptService _encryptService;

        [SetUp]
        public void SetUp()
        {
            _encryptService = new EncryptService();
        }


        [Test]
        public void List_test()
        {
            // Arrange
            HttpContextBase context = FakeHttpContext();

            List<EmployeeModel> employeeModels = new List<EmployeeModel>();
            var fakeEmployee1 = new EmployeeModel()
            {
                Id = 1,
                Name = "Utfake",
                Surname = "Fake1",
                PasswordClear = "fTest",
                Email = "fktest@fake.com",
                Type = EmployeeType.Ordinal,
                StartDate = new DateTime(2014, 02, 25),
                EndDate = new DateTime(2018, 02, 25)
            };

            var fakeEmployee2 = new EmployeeModel()
            {
                Id = 1,
                Name = "Utfake",
                Surname = "Fake1",
                PasswordClear = "fTest",
                Email = "fktest@fake.com",
                Type = EmployeeType.Ordinal,
                StartDate = new DateTime(2014, 02, 25),
                EndDate = new DateTime(2018, 02, 25)
            };

            employeeModels.Add(fakeEmployee1);
            employeeModels.Add(fakeEmployee2);

            Mock<IEmployeeService> mock = new Mock<IEmployeeService>();
            mock.Setup(c => c.GetEmployees()).Returns(employeeModels);
            mock.Setup(c => c.GetEmployee(It.IsAny<int>())).Returns(fakeEmployee1);

            EmployeeController controller = new EmployeeController(mock.Object, _encryptService);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.List(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.AreEqual(((EmployeeModel)result.Model).EmployeeList.Count, 2);
        }

        [Test]
        public void List_NullId_Test()
        {
            // Arrange
            HttpContextBase context = FakeHttpContext();

            Mock<IEmployeeService> mock = new Mock<IEmployeeService>();

            EmployeeController controller = new EmployeeController(mock.Object, _encryptService);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            RedirectToRouteResult result = controller.List(0) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.RouteValues.Values.ElementAt(0));
            Assert.AreEqual("Account", result.RouteValues.Values.ElementAt(1));
        }


        [TestCase(typeof(ArgumentException), "id")]
        public void DeleteEmployee_ArgumentException_Test(Type expectedException, string expectedVar)
        {
            //Arrange
            HttpContextBase context = FakeHttpContext();

            Mock<IEmployeeService> mock = new Mock<IEmployeeService>();

            EmployeeController controller = new EmployeeController(mock.Object, _encryptService);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);


            //Assert
            var ex = Assert.Throws(expectedException, () => controller.DeleteEmployee(null));
            Assert.That(ex.Message.Contains(expectedVar));
        }

        /// <summary>
        /// Fakes the HTTP context.
        /// </summary>
        /// <returns>A HTTP context.</returns>
        private static HttpContextBase FakeHttpContext(string query = "")
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();

            request.Setup(x => x.ServerVariables).Returns(new NameValueCollection
                {
                    {"HTTP_X_FORWARDED_FOR","127.0.0.1"},
                    {"REMOTE_ADDR","127.0.0.1"}
                });

            request.SetupGet(r => r.QueryString).Returns(HttpUtility.ParseQueryString(query));
            request.Setup(r => r.Cookies).Returns(new HttpCookieCollection());
            request.Setup(x => x.Url).Returns(new Uri("http://localhost:123"));

            response.Setup(x => x.Cookies).Returns(new HttpCookieCollection());

            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);

            return context.Object;
        }

    }
}
