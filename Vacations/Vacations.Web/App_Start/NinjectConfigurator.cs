using System.Diagnostics.CodeAnalysis;
using Ninject;
using Vacations.Business.Services.Contracts;
using Vacations.Business.Services.Implementations;
using Vacations.DataAccess.Contracts;
using Vacations.DataAccess.Implementations;
using Vacations.Web.Services.Contracts;
using Vacations.Web.Services.Implementations;

namespace Vacations.Web
{
    /// <summary>
    /// NinjectConfigurator class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class NinjectConfigurator
    {
        /// <summary>
        /// Configure the kernel binding
        /// </summary>
        /// <param name="container"></param>
        public void Configure(IKernel container)
        {
            AddBindings(container);
        }

        /// <summary>
        /// Adds injection bindings
        /// </summary>
        /// <param name="container">The binding container</param>
        private void AddBindings(IKernel container)
        {
            container.Bind<IEmployeeRepository>().To<EmployeeRepository>();
            container.Bind<IEmployeeService>().To<EmployeeService>();
            container.Bind<IAccountService>().To<AccountService>();
            container.Bind<IEncryptService>().To<EncryptService>();
            container.Bind<IFormsAuthenticationService>().To<FormsAuthenticationService>();
            container.Bind<ICompanyHolidayRepository>().To<CompanyHolidayRepository>();
            container.Bind<IVacationPolicyRepository>().To<VacationPolicyRepository>();
            container.Bind<IVacationRequestRepository>().To<VacationRequestRepository>();
        }
    }
}