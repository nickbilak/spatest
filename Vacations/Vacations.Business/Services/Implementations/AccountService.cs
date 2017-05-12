using System;
using Vacations.Business.Services.Contracts;
using Vacations.DataAccess.Contracts;
using Vacations.DataAccess.Models;

namespace Vacations.Business.Services.Implementations
{
    public class AccountService : IAccountService
    {
        #region Fields

        /// <summary>
        /// EncryptService field
        /// </summary>
        private readonly IEncryptService _encryptService;

        /// <summary>
        /// IMsPartnerRepository interface
        /// </summary>
        private readonly IEmployeeRepository _employeeRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// AccountService constructor
        /// </summary>
        public AccountService()
        {
        }

        /// <summary>
        /// AccountService constructor
        /// </summary>
        /// <param name="encryptService">EncryptService instance</param>
        /// <param name="employeeRepository">EmployeeRepository instance</param>
        public AccountService(IEncryptService encryptService, IEmployeeRepository employeeRepository)
        {
            _encryptService = encryptService;
            _employeeRepository = employeeRepository;
        }

        #endregion

        /// <summary>
        /// Get EmployeeModel by login and password
        /// </summary>
        /// <param name="login">login(email)</param>
        /// <param name="password">password</param>
        /// <returns>Return an object EmployeeModel</returns>
        public EmployeeModel GetEmployeeByLoginPassword(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login)) throw new ArgumentNullException("login");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("password");

            var passwordHash = _encryptService.Sha1Hash(password);

            return _employeeRepository.GetEmployeeByLoginPassword(login, passwordHash);
        }

    }
}
