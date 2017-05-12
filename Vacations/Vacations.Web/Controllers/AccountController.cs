using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using log4net;
using Vacations.Business.Services.Contracts;
using Vacations.DataAccess.Models;
using Vacations.Web.Helpers;
using Vacations.Web.Services.Contracts;

namespace Vacations.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Fields

        /// <summary>
        /// Get the instance of the ConfigHelper class
        /// </summary>
        private readonly ConfigHelper _configHelper = ConfigHelper.GetInstance;

        /// <summary>
        /// Get the instance of the ErrorHelper
        /// </summary>
        private readonly ErrorHelper _error = ErrorHelper.GetInstance;

        /// <summary>
        /// Get the instance of the IAccountService
        /// </summary>
        private readonly IAccountService _accountService;

        /// <summary>
        /// Get the instance of the IEncryptService
        /// </summary>
        private readonly IEncryptService _encryptService;

        /// <summary>
        /// Get the instance of the IFormsAuthenticationService
        /// </summary>
        private readonly IFormsAuthenticationService _formsAuthenticationService;

        /// <summary>
        /// Logger field
        /// </summary>
        private static ILog _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// AccountController constructor
        /// </summary>
        /// <param name="accountService">AccountService instance</param>
        /// <param name="encryptService">EncryptService instance</param>
        /// <param name="formsAuthenticationService">FormsAuthenticationService instance</param>
        public AccountController(IAccountService accountService, IEncryptService encryptService, IFormsAuthenticationService formsAuthenticationService)
        {
            _accountService = accountService;
            _encryptService = encryptService;
            _formsAuthenticationService = formsAuthenticationService;
        }

        #endregion

        /// <summary>
        /// handle login process with token or cookies
        /// </summary>
        /// <returns>Login view or redirect to /employee/list</returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            EmployeeModel user = null;

            //todo
            /*
            if (Request.Cookies["erpvac"] != null
                && !string.IsNullOrEmpty(Request.Cookies["erpvac"]["login"])
                && !string.IsNullOrEmpty(Request.Cookies["erpvac"]["password"]))
            {
                var login = _encryptService.Decrypt(Request.Cookies["erpvac"]["login"], _configHelper.EncryptionKey);
                var password = _encryptService.Decrypt(Request.Cookies["erpvac"]["password"], _configHelper.EncryptionKey);
                user = _accountService.GetEmployeeByLoginPassword(login, password);
                _logger.Info(string.Format("user: {0}", user.Email));
            }
            if (user != null)
            {
                _logger.Info("proceed to /");
                _formsAuthenticationService.Login(user.Email);
                return RedirectOnAuth("/", user.Id);
            }
            */
            user = new EmployeeModel();
            ClearLoginCookies();
            return View(user);
        }


        /// <summary>
        /// handle login process with login and password
        /// </summary>
        /// <param name="model">UserViewModel</param>
        /// <param name="returnUrl">returnUrl</param>
        /// <returns>Login view or redirect to returnUrl</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(EmployeeModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                EmployeeModel user = _accountService.GetEmployeeByLoginPassword(model.Email, model.PasswordClear);

                if (user != null)
                {
                    if (model.AutoLogon)
                    {
                        SaveLoginCookies(model.Email, model.PasswordClear);
                    }
                    _formsAuthenticationService.Login(user.Email);
                    return RedirectOnAuth(returnUrl, user.Id);
                }

                model.Error = _error.GetError("loginorpasswordincorrect");
                return View(model);
            }
            ClearLoginCookies(); 
            return View(model);
        }

        /// <summary>
        /// handle logout process 
        /// </summary>
        /// <returns>Login view</returns>
        [Authorize]
        public ActionResult Logout()
        {
            _formsAuthenticationService.Logout();
            ClearLoginCookies();
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// redirect to returnUrl or /employee/list/{id}
        /// </summary>
        /// <param name="returnUrl">returnUrl</param>
        /// <param name="employeeId">employee id</param>
        /// <returns>redirect</returns>
        private ActionResult RedirectOnAuth(string returnUrl, int employeeId)
        {
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
          
            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
            {
                _logger.Info("Redirect to " + returnUrl);
                return Redirect(returnUrl);
            }

            //todo - redirect to proper page by employee type
            _logger.Info("Redirect to /employee/list/" + employeeId);
            return RedirectToAction("List", "Employee", new { employeeId = employeeId });
        }

        /// <summary>
        /// Clear Login Cookies
        /// </summary>
        private void ClearLoginCookies()
        {
            if (Request.Cookies["erpvac"] == null) return;
            HttpCookie cookies = new HttpCookie("erpvac") { Expires = DateTime.Now.AddDays(-1d) };
            Response.Cookies.Set(cookies);
        }

        /// <summary>
        /// Save Login Cookies
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        private void SaveLoginCookies(string login, string password)
        {
            var responseCookie = Response.Cookies["erpvac"] ?? new HttpCookie("erpvac");

            responseCookie["login"] = _encryptService.Encrypt(login, _configHelper.EncryptionKey);
            responseCookie["password"] = _encryptService.Encrypt(password, _configHelper.EncryptionKey);
            responseCookie.Expires = DateTime.Now.AddDays(180);                      
        }

    }
}