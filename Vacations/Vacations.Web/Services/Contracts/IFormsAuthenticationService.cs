namespace Vacations.Web.Services.Contracts
{
    /// <summary>
    /// Wrapper for static FormsAuthentication helper
    /// </summary>
    public interface IFormsAuthenticationService
    {
        /// <summary>
        /// Login method wrapper
        /// </summary>
        void Login(string username);

        /// <summary>
        /// SignOut method wrapper
        /// </summary>
        void Logout();
    }
}