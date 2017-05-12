using System.Collections.Generic;
using Vacations.DataAccess.Models;

namespace Vacations.DataAccess.Contracts
{
    public interface IVacationPolicyRepository
    {

        /// <summary>
        /// Add VacationPolicy
        /// </summary>
        /// <param name="vacationPolicy">Object VacationPolicyMdel</param>
        /// <returns>Return the employee ID inserted, an error text if something wrong happens</returns>
        string Add(VacationPolicyModel vacationPolicy);

        /// <summary>
        /// Update VacationPolicy
        /// </summary>
        /// <param name="vacationPolicy">VacationPolicyModel</param>
        /// <returns>Return an error text if something wrong happens</returns>
        bool Update(VacationPolicyModel vacationPolicy);

        /// <summary>
        /// Delete VacationPolicy
        /// </summary>
        /// <param name="id">VacationPolicyModel Id</param>
        /// <returns>Return an error text if something wrong happens</returns>
        bool Delete(int id);

        /// <summary>
        /// Get VacationPolicy
        /// </summary>
        /// <param name="id">VacationPolicyModel Id</param>
        /// <returns>Return a EmployeeModel object</returns>
        VacationPolicyModel GetVacationPolicyModel(int id);

        /// <summary>
        /// Get VacationPolicy list
        /// </summary>
        /// <returns>Return a EmployeeModel collection</returns>
        IEnumerable<VacationPolicyModel> GetVacationPolicies();
    }
}
