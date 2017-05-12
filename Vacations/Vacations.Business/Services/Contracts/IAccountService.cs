using Vacations.DataAccess.Models;

namespace Vacations.Business.Services.Contracts
{
    public interface IAccountService
    {

        /// <summary>
        /// Get EmployeeModel by login and password
        /// </summary>
        /// <param name="login">login(email)</param>
        /// <param name="password">password</param>
        /// <returns>Return an object EmployeeModel</returns>
        EmployeeModel GetEmployeeByLoginPassword(string login, string password);

    }
}
