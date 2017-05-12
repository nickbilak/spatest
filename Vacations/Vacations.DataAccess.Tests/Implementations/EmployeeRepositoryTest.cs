using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vacations.DataAccess.Contracts;
using Vacations.DataAccess.Implementations;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Vacations.DataAccess.Tests.Implementations
{
    /// <summary>
    /// Summary description for EmployeeRepositoryTest
    /// </summary>
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class EmployeeRepositoryTest
    {

        private readonly IEmployeeRepository _employeeRepository = new EmployeeRepository();

        [TestCategory("skip")]
        [TestMethod]
        public void GetEmployeesTests()
        {
            //ACT
            var result = _employeeRepository.GetEmployees();

            //ASSERT
            Assert.IsNotNull(result);
        }

    }
}
