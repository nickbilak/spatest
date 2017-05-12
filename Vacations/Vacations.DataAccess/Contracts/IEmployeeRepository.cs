using System.Collections.Generic;
using Vacations.DataAccess.Models;

namespace Vacations.DataAccess.Contracts
{
    public interface IEmployeeRepository
    {

        /// <summary>
        /// Add an Employee.
        /// </summary>
        /// <param name="employee">Object EmployeeModel</param>
        /// <returns>Return the employee ID inserted, an error text if something wrong happens</returns>
        string Add(EmployeeModel employee);

        /// <summary>
        /// Update an Employee
        /// </summary>
        /// <param name="employee">Employee Model</param>
        /// <returns>true on success</returns>
        bool Update(EmployeeModel employee);

        /// <summary>
        /// Delete an Employee
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>true on success</returns>
        bool Delete(int id);

        /// <summary>
        /// Get an Employee
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Return a EmployeeModel object</returns>
        EmployeeModel GetEmployee(int id);

        /// <summary>
        /// Get an Employee
        /// </summary>
        /// <param name="login">login(email)</param>
        /// <param name="passwordHash">password hash</param>
        /// <returns>Return a EmployeeModel object</returns>
        EmployeeModel GetEmployeeByLoginPassword(string login, byte[] passwordHash);

        /// <summary>
        /// Get Employee list
        /// </summary>
        /// <returns>Return a EmployeeModel collection</returns>
        IEnumerable<EmployeeModel> GetEmployees();

    }
}
