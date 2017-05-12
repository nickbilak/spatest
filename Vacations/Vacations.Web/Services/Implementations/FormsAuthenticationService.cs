using System.Diagnostics.CodeAnalysis;
using System.Web.Security;
using Vacations.Web.Services.Contracts;

namespace Vacations.Web.Services.Implementations
{
    /// <summary>
    /// Wrapper for static FormsAuthentication helper
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        /// <summary>
        /// Login method wrapper
        /// </summary>
        public void Login(string username)
        {
            FormsAuthentication.SetAuthCookie(username, false);
        }

        /// <summary>
        /// SignOut method wrapper
        /// </summary>
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

    }
}