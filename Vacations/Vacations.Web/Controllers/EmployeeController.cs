using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Vacations.Business.Services.Contracts;
using Vacations.DataAccess.Enums;
using Vacations.DataAccess.Models;
using Vacations.Web.Filters;
using Vacations.Web.Helpers;

namespace Vacations.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        #region Fields

        /// <summary>
        /// Get the instance of IEmployeeService
        /// </summary>
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Get the instance of IEncryptService
        /// </summary>
        private readonly IEncryptService _encryptService;

        /// <summary>
        /// Get the instance of the ConfigHelper class
        /// </summary>
        private readonly ConfigHelper _configHelper = ConfigHelper.GetInstance;

        /// <summary>
        /// Logger field
        /// </summary>
        private static ILog _logger;

        /// <summary>
        /// Get the instance of the ErrorHelper
        /// </summary>
        private readonly ErrorHelper _error = ErrorHelper.GetInstance;


        #endregion


        #region Constructors

        /// <summary>
        /// EmployeeController constructor
        /// </summary>
        /// <param name="employeeService">EmployeeService instance</param>
        /// <param name="encryptService">EncryptService instance</param>
        public EmployeeController(IEmployeeService employeeService, IEncryptService encryptService)
        {
            _employeeService = employeeService;
            _encryptService = encryptService;
        }

        #endregion


        /// <summary>
        /// Display the main page with grid
        /// </summary>
        /// <returns> Main view</returns>
        [Authorize]
        public ActionResult List(int? employeeId)
        {
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            if (employeeId == null || employeeId.Value < 1)
            {
                return RedirectToAction("Login", "Account");
            }

            var emp = _employeeService.GetEmployee(employeeId.Value);

            var employeeList = _employeeService.GetEmployees().ToList();

            emp.EmployeeList = employeeList;

            return View("Main", emp);
        }


        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="id">employee ID</param>
        [HttpPost]
        [Authorize]
        [ValidateJsonAntiForgeryToken]
        public ActionResult DeleteEmployee(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("id");

            var employeeModel = new EmployeeModel();

            var result = _employeeService.Delete(Convert.ToInt32(_encryptService.Decrypt(HttpUtility.HtmlDecode(id), _configHelper.EncryptionKey)));

            if (!string.IsNullOrEmpty(result))
            {
                employeeModel.Error = _error.GetError(result);
                return Content(JsonConvert.SerializeObject(employeeModel, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" }).Replace(@"\r\n", @"\\r\\n").Replace(@"\t", @"\\t"));
            }

            return Content("");
        }

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="employee">EmployeeFormViewModel object</param>
        /// <returns>Return the JSon object of the updated employee</returns>
        [HttpPut]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEmployee(EmployeeModel employee)
        {
            if (employee == null) throw new ArgumentNullException("employee");

            var result = _employeeService.Update(employee);

            var employeeFormViewModel = new EmployeeModel();
            if (!string.IsNullOrEmpty(result))
            {
                employeeFormViewModel.Error = _error.GetError(result);
                return Content(JsonConvert.SerializeObject(employeeFormViewModel, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" }).Replace(@"\r\n", @"\\r\\n").Replace(@"\t", @"\\t"));
            }

            var row = _employeeService.GetEmployee(Convert.ToInt32(employee.Id));

            return Content(JsonConvert.SerializeObject(employeeFormViewModel, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" }).Replace(@"\r\n", @"\\r\\n").Replace(@"\t", @"\\t"));

        }

        /// <summary>
        /// Add employee
        /// </summary>
        /// <param name="employee">EmployeeFormViewModel object</param>
        /// <returns>Return the JSon Object of the newly added employee</returns>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(EmployeeModel employee)
        {
            if (employee == null) throw new ArgumentNullException("employee");

            employee.EndDate = DateTime.MaxValue;
            //employee.PasswordClear = _employeeService.GeneratePassword(8);
            employee.PasswordHash = _encryptService.Sha1Hash(employee.PasswordClear);
            employee.Type = EmployeeType.Ordinal;
            var result = _employeeService.Add(employee);

            int newId;

            var employeeFormViewModel = new EmployeeModel();
            if (!int.TryParse(result, out newId))
            {
                employeeFormViewModel.Error = _error.GetError(result);
                return Content(JsonConvert.SerializeObject(employeeFormViewModel, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" }).Replace(@"\r\n", @"\\r\\n").Replace(@"\t", @"\\t"));
            }

            var newEmployee = _employeeService.GetEmployee(Convert.ToInt32(newId));

            return Content(JsonConvert.SerializeObject(newEmployee, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" }).Replace(@"\r\n", @"\\r\\n").Replace(@"\t", @"\\t"));

        }


    }
}
