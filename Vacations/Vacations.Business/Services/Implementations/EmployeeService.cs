using System;
using System.Collections.Generic;
using Vacations.Business.Services.Contracts;
using Vacations.DataAccess.Contracts;
using Vacations.DataAccess.Models;

namespace Vacations.Business.Services.Implementations
{
    /// <summary>
    /// EmployeeService class
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        #region Fields

        /// <summary>
        /// IEmployeeRepository interface 
        /// </summary>
        private readonly IEmployeeRepository _employeeRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// EmployeeService constructor
        /// </summary>
        public EmployeeService()
        {
        }

        /// <summary>
        /// EmployeeService constructor
        /// </summary>
        /// <param name="employeeRepository">Employee Repository instance</param>
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Get the list of employees
        /// </summary>
        /// <returns>Ienumarable of Employee model</returns>
        public IEnumerable<EmployeeModel> GetEmployees()
        {
            return _employeeRepository.GetEmployees();
        }

        /// <summary>
        /// Get Employee
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>Return a EmployeeModel object</returns>
        public EmployeeModel GetEmployee(int employeeId)
        {
            if (employeeId < 1) throw new ArgumentException("employeeId");

            return _employeeRepository.GetEmployee(employeeId);
        }

        /// <summary>
        /// Add employee
        /// </summary>
        /// <param name="employee">employee Model</param>
        /// <returns>Return the employee ID inserted, an error if something wrong happens</returns>
        public string Add(EmployeeModel employee)
        {
            if (employee == null) throw new ArgumentNullException("employee");

            return _employeeRepository.Add(employee);
        }

        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="employee">employee Model</param>
        /// <returns>Return an error.</returns>
        public string Update(EmployeeModel employee)
        {
            if (employee == null) throw new ArgumentNullException("employee");
            var res = _employeeRepository.Update(employee) ? "" : "error update";
            return res;
        }

        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="employeeId">employee ID</param>
        /// <returns>Return an error.</returns>
        public string Delete(int employeeId)
        {
            if (employeeId < 1) throw new ArgumentException("employeeId");
            var res = _employeeRepository.Delete(employeeId) ? "" : "error delete";
            return res;
        }

        /// <summary>
        /// get random password
        /// </summary>
        /// <param name="passwordLength">password length</param>
        /// <returns>random password string</returns>
        public string GeneratePassword(int passwordLength)
        {
            string alphaCaps = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string alphaLow = "qwertyuiopasdfghjklzxcvbnm";
            string nonliteral = "1234567890!@$^&*)_-+[{]}:?";
            string allChars = alphaCaps + alphaLow + nonliteral;
            Random r = new Random();
            String generatedPassword = "";
            string posArray = "0123456789";
            if (passwordLength < posArray.Length)
                posArray = posArray.Substring(0, passwordLength);
            var pLower = GetRandomPosition(r, ref posArray);
            var pUpper = GetRandomPosition(r, ref posArray);
            var pNonliteral = GetRandomPosition(r, ref posArray);
            int i = 0;
            while (generatedPassword.Length < passwordLength)
            {
                if (i == pLower)
                    generatedPassword += GetRandomChar(r, alphaCaps);
                else if (i == pUpper)
                    generatedPassword += GetRandomChar(r, alphaLow);
                else if (i == pNonliteral)
                    generatedPassword += GetRandomChar(r, nonliteral);
                else
                    generatedPassword += GetRandomChar(r, allChars);
                i++;
            }
            return generatedPassword;
        }

        /// <summary>
        /// gets random char
        /// </summary>
        /// <param name="r">Random instance</param>
        /// <param name="fullString">string to get the random char from</param>
        /// <returns>random char from fullString</returns>
        private string GetRandomChar(Random r, string fullString)
        {
            if (r == null) throw new ArgumentNullException("r");
            if (fullString == null) throw new ArgumentNullException("fullString");

            return fullString.ToCharArray()[(int)Math.Floor(r.NextDouble() * fullString.Length)].ToString();
        }

        /// <summary>
        /// gets random position
        /// </summary>
        /// <param name="r">Random instance</param>
        /// <param name="posArray">string to get the random position number from</param>
        /// <returns>random position number</returns>
        private int GetRandomPosition(Random r, ref string posArray)
        {
            if (r == null) throw new ArgumentNullException("r");
            if (posArray == null) throw new ArgumentNullException("posArray");

            var randomChar = posArray.ToCharArray()[(int)Math.Floor(r.NextDouble()
                                                                       * posArray.Length)].ToString();
            var pos = int.Parse(randomChar);
            posArray = posArray.Replace(randomChar, "");
            return pos;
        }

        #endregion
    }
}
