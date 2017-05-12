using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Vacations.Web.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class RouteConfigTest
    {

        /// <summary>
        /// Checks that the values are being obtained from the segments or the URL that is hoped for
        /// </summary>
        /// <param name="url">The input URL</param>
        /// <param name="controller">The controller</param>
        /// <param name="action">The action</param>
        /// <param name="routeProperties">the route Properties</param>
        /// <param name="httpMethod">The http method</param>
        [TestCase("∼/Account/Login/test", "Account", "Login", null, "GET")] //check for the URL that is hoped for
        [TestCase("∼/One/Two", "One", "Two", null, "GET")] // check that the values are being obtained from the segments
        public void TestRouteMatch(string url, string controller, string action, object routeProperties = null, string httpMethod = "GET")
        {
            // Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act - process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            // Assert
            Assert.IsNotNull(result);
            bool isValidrouteResult = CheckIncomingRouteResult(result, controller, action, routeProperties);
            Assert.IsTrue(isValidrouteResult);
        }



        /// <summary>
        /// Ensures that too many or too few segments fails to match
        /// </summary>
        /// <param name="url"></param>
        [TestCase("∼/Employee/List/Segment1/Segment2")]
        [TestCase("∼/Employee/List/Segment1/Segment2/Segment3")]
        public void TestRouteFail(string url) 
        {
            // Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act - process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url));


            // Assert
            Assert.IsTrue(result == null || result.Route == null);
        }


        /// <summary>
        /// Checks the incoming route result
        /// </summary>
        /// <param name="routeResult">The route data</param>
        /// <param name="controller">The controller</param>
        /// <param name="action">The action</param>
        /// <param name="propertySet">the property set</param>
        /// <returns></returns>
        private static bool CheckIncomingRouteResult(RouteData routeResult, string controller, string action, object propertySet = null) 
        {
            Func<object, object, bool> valCompare = (v1, v2) => StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
            
            bool result = valCompare(routeResult.Values["controller"], controller) && valCompare(routeResult.Values["action"], action);

            if (propertySet == null) return result;
            PropertyInfo[] propInfo = propertySet.GetType().GetProperties();

            if (propInfo.Any(pi => !routeResult.Values.ContainsKey(pi.Name) || !valCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null))))
            {
                result = false;
            }
            return result;
        }



        /// <summary>
        /// Creates mock HttpContext
        /// </summary>
        /// <param name="targetUrl">The target URL</param>
        /// <param name="httpMethod">The HTTP method</param>
        /// <returns></returns>
        private HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET")
        {
            // create the mock request
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);
            
            // create the mock response
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            // create the mock context, using the request and response
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            // return the mocked context
            return mockContext.Object;

        }

    }
}
