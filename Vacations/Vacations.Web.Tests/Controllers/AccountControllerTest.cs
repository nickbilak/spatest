using System;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Vacations.Business.Services.Contracts;
using Vacations.Business.Services.Implementations;
using Vacations.DataAccess.Contracts;
using Vacations.DataAccess.Enums;
using Vacations.DataAccess.Models;
using Vacations.Web.Controllers;
using Vacations.Web.Services.Contracts;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Vacations.Web.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class AccountControllerTest
    {
        private IFormsAuthenticationService _formsAuthenticationService; 
        private IEmployeeRepository _employeeRepository;
        private IEncryptService _encryptService;
        private IAccountService _accountService;

        [SetUp]
        public void SetUp()
        {
            EmployeeModel fakeModel = new EmployeeModel
            {
                Id = 2,
                Email = "fake@ema.io",
                Type = EmployeeType.HR,
                PasswordHash = new byte[] { 0, 1, 2 }
            };
            Mock<IEmployeeRepository> employeeRepository = new Mock<IEmployeeRepository>();
            employeeRepository.Setup(c => c.GetEmployeeByLoginPassword(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(fakeModel);
            employeeRepository.Setup(c => c.GetEmployee(It.IsAny<int>())).Returns(fakeModel);

            _employeeRepository = employeeRepository.Object;

            Mock<IFormsAuthenticationService> formsAuthenticationService = new Mock<IFormsAuthenticationService>();
            formsAuthenticationService.Setup(c => c.Login(It.IsAny<string>()));
            formsAuthenticationService.Setup(c => c.Logout());

            _formsAuthenticationService = formsAuthenticationService.Object;
            _encryptService = new EncryptService();
            _accountService = new AccountService(_encryptService, _employeeRepository);
        }

        /// <summary>
        /// test Login action
        /// </summary>
        [Test]
        public void Login_Test()
        {
            // Arrange
            HttpContextBase context = FakeHttpContext();
            AccountController controller = new AccountController(_accountService, _encryptService, _formsAuthenticationService);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsNull(((EmployeeModel)result.Model).Error);
        }

        /// <summary>
        /// test Login with post data action
        /// </summary>
        [Test]
        public void LoginPost_Test()
        {
            // Arrange
            HttpContextBase context = FakeHttpContext();
            AccountController controller = new AccountController(_accountService, _encryptService, _formsAuthenticationService);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);
            var requestContext = new RequestContext(context, new RouteData());
            controller.Url = new UrlHelper(requestContext);

            var model = new EmployeeModel
            {
                Email = "fake login",
                PasswordClear = "fake password",
                AutoLogon = true
            };
            var returnUrl = "/employee/List/2";

            // Act
            var result = controller.Login(model, returnUrl) as RedirectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(returnUrl, result.Url);
        }


        /// <summary>
        /// test Logout action
        /// </summary>
        [Test]
        public void Logout_Test()
        {
            // Arrange
            HttpContextBase context = FakeHttpContext();
            AccountController controller = new AccountController(_accountService, _encryptService, _formsAuthenticationService);
            controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);

            // Act
            var result = controller.Logout() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.RouteValues["Action"]);
            Assert.AreEqual("Account", result.RouteValues["controller"]);
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
