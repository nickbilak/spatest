using System.Collections.Generic;
using Vacations.DataAccess.Models;

namespace Vacations.DataAccess.Contracts
{
    public interface ICompanyHolidayRepository
    {

        /// <summary>
        /// Add CompanyHoliday
        /// </summary>
        /// <param name="companyHoliday">Object VacationPolicyMdel</param>
        /// <returns>Return the employee ID inserted, an error text if something wrong happens</returns>
        string Add(CompanyHolidayModel companyHoliday);

        /// <summary>
        /// Update CompanyHoliday
        /// </summary>
        /// <param name="companyHoliday">VacationPolicyModel</param>
        /// <returns>true on success</returns>
        bool Update(CompanyHolidayModel companyHoliday);

        /// <summary>
        /// Delete CompanyHoliday
        /// </summary>
        /// <param name="id">VacationPolicyModel Id</param>
        /// <returns>true on success</returns>
        bool Delete(int id);

        /// <summary>
        /// Get CompanyHoliday
        /// </summary>
        /// <param name="id">CompanyHolidayModel Id</param>
        /// <returns>Return a CompanyHolidayModel object</returns>
        VacationPolicyModel GetCompanyHoliday(int id);

        /// <summary>
        /// Get VacationPolicy list
        /// </summary>
        /// <returns>Return a CompanyHolidayModel collection</returns>
        IEnumerable<VacationPolicyModel> GetCompanyHolidays();

    }
}
