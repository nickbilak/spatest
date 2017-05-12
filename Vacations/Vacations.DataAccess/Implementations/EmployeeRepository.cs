using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using PetaPoco;
using Vacations.DataAccess.Contracts;
using Vacations.DataAccess.Models;

namespace Vacations.DataAccess.Implementations
{
    /// <summary>
    /// EmployeeRepository implementation
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EmployeeRepository : IEmployeeRepository
    {

        private const string DbName = "erptest";


        /// <summary>
        /// Add an Employee.
        /// </summary>
        /// <param name="employee">Object EmployeeModel</param>
        /// <returns>Return the employee ID inserted, an error text if something wrong happens</returns>
        public string Add(EmployeeModel employee)
        {
            if (employee == null) throw new ArgumentNullException("employee");

            using (var dataContext = new Database(DbName))
            {
                var error = new SqlParameter("@error", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Output,
                    Size = 400
                };

                var id = new SqlParameter("@employee_id", SqlDbType.Int) { Direction = ParameterDirection.Output };

                var inserted = new SqlParameter("@inserted", SqlDbType.Bit) {Direction = ParameterDirection.Output};

                var sql = Sql.Builder.Append(@";EXEC uspAddEmployee @0, @1, @2, @3, @4, @5, @6, @7, @8, @9 OUTPUT, @10 OUTPUT, @11 OUTPUT",
                                            employee.Name,
                                            employee.Surname,
                                            employee.Email,
                                            employee.PasswordHash,
                                            (int)employee.Type,
                                            employee.StartDate,
                                            employee.EndDate,
                                            employee.Phone,
                                            employee.Position,
                                            error,
                                            id,
                                            inserted);

                dataContext.Execute(sql);

                if (error.Value != DBNull.Value) return error.Value.ToString();

                if (!(bool)inserted.Value) return "";

                return id.Value.ToString();
            }

        }

        /// <summary>
        /// Update an Employee
        /// </summary>
        /// <param name="employee">Employee Model</param>
        /// <returns>true on success</returns>
        public bool Update(EmployeeModel employee)
        {
            if (employee == null) throw new ArgumentNullException("employee");

            using (var dataContext = new Database(DbName))
            {
                dataContext.Update(employee);
                return true;
            }
        }

        /// <summary>
        /// Delete an Employee
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>true on success</returns>
        public bool Delete(int id)
        {
            if (id < 1) throw new ArgumentException("id");

            var employee = new EmployeeModel() { Id = id };

            using (var dataContext = new Database(DbName))
            {
                dataContext.Delete(employee);
                return true;
            }
        }

        /// <summary>
        /// Get an Employee
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Return a EmployeeModel object</returns>
        public EmployeeModel GetEmployee(int id)
        {
            if (id < 1) throw new ArgumentException("id");

            using (var dataContext = new Database(DbName))
            {
                return dataContext.Query<EmployeeModel>("SELECT * FROM employees WHERE id=@0", id).FirstOrDefault();
            }
        }

        /// <summary>
        /// Get an Employee
        /// </summary>
        /// <param name="login">login(email)</param>
        /// <param name="passwordHash">password hash</param>
        /// <returns>Return a EmployeeModel object</returns>
        public EmployeeModel GetEmployeeByLoginPassword(string login, byte[] passwordHash)
        {
            if (string.IsNullOrWhiteSpace(login)) throw new ArgumentNullException("login");
            if (passwordHash == null) throw new ArgumentNullException("passwordHash");

            using (var dataContext = new Database(DbName))
            {
                var model = dataContext.SingleOrDefault<EmployeeModel>("SELECT * FROM [erptest].[dbo].[employees] WHERE email=@0 AND password=@1", login, passwordHash);
                return model;
            }
        }

        /// <summary>
        /// Get Employee list
        /// </summary>
        /// <returns>Return a EmployeeModel collection</returns>
        public IEnumerable<EmployeeModel> GetEmployees()
        {
            using (var dataContext = new Database(DbName))
            {
                return dataContext.Query<EmployeeModel>("SELECT * FROM employees");
            }
        }

    }
}
