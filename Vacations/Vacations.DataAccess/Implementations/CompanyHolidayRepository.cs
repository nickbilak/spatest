using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using PetaPoco;
using Vacations.DataAccess.Contracts;
using Vacations.DataAccess.Models;

namespace Vacations.DataAccess.Implementations
{
    /// <summary>
    /// CompanyHolidayRepository implementation
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CompanyHolidayRepository : ICompanyHolidayRepository
    {

        private const string DbName = "erptest";


        /// <summary>
        /// Add CompanyHoliday
        /// </summary>
        /// <param name="companyHoliday">Object VacationPolicyMdel</param>
        /// <returns>Return the inserted Id, an error text if something wrong happens</returns>
        public string Add(CompanyHolidayModel companyHoliday)
        {
            if (companyHoliday == null) throw new ArgumentNullException("companyHoliday");

            using (var dataContext = new Database(DbName))
            {
                dataContext.Insert(companyHoliday);
                return companyHoliday.Id.ToString();
            }

        }

        /// <summary>
        /// Update CompanyHoliday
        /// </summary>
        /// <param name="companyHoliday">VacationPolicyModel</param>
        /// <returns>true on success</returns>
        public bool Update(CompanyHolidayModel companyHoliday)
        {
            if (companyHoliday == null) throw new ArgumentNullException("companyHoliday");

            using (var dataContext = new Database(DbName))
            {
                dataContext.Update(companyHoliday);
                return true;
            }
        }

        /// <summary>
        /// Delete CompanyHoliday
        /// </summary>
        /// <param name="id">VacationPolicyModel Id</param>
        /// <returns>true on success</returns>
        public bool Delete(int id)
        {
            if (id < 1) throw new ArgumentException("id");

            var model = new CompanyHolidayModel() { Id = id };

            using (var dataContext = new Database(DbName))
            {
                dataContext.Delete(model);
                return true;
            }
        }

        /// <summary>
        /// Get CompanyHoliday
        /// </summary>
        /// <param name="id">CompanyHolidayModel Id</param>
        /// <returns>Return a CompanyHolidayModel object</returns>
        public VacationPolicyModel GetCompanyHoliday(int id)
        {
            if (id < 1) throw new ArgumentException("id");

            using (var dataContext = new Database(DbName))
            {
                return dataContext.SingleOrDefault<VacationPolicyModel>("SELECT * FROM vacation_policies WHERE id=@0", id);
            }
        }

        /// <summary>
        /// Get VacationPolicy list
        /// </summary>
        /// <returns>Return a CompanyHolidayModel collection</returns>
        public IEnumerable<VacationPolicyModel> GetCompanyHolidays()
        {
            using (var dataContext = new Database(DbName))
            {
                return dataContext.Query<VacationPolicyModel>("SELECT * FROM vacation_policies");
            }
        }


    }
}
