using System.Collections.Generic;
using Vacations.DataAccess.Models;

namespace Vacations.Business.Services.Contracts
{
    /// <summary>
    /// IEmployee interface
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Get the list of employee
        /// </summary>
        /// <returns>Ienumarable of employee</returns>
        IEnumerable<EmployeeModel> GetEmployees();

        /// <summary>
        /// Get Employee
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>Return a EmployeeModel object</returns>
        EmployeeModel GetEmployee(int employeeId);

        /// <summary>
        /// Add Employee
        /// </summary>
        /// <param name="employee">Employee Model</param>
        /// <returns>Return the Employee ID inserted, an error if something wrong happens</returns>
        string Add(EmployeeModel employee);

        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="employee">employee Model</param>
        /// <returns>Return an error.</returns>
        string Update(EmployeeModel employee);

        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="employeeId">employee ID</param>
        /// <returns>Return an error.</returns>
        string Delete(int employeeId);

        /// <summary>
        /// random random password
        /// </summary>
        /// <param name="passwordLength">password length</param>
        /// <returns>random password string</returns>
        string GeneratePassword(int passwordLength);


    }
}
