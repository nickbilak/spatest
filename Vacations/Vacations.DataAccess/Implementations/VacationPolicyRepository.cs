using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using PetaPoco;
using Vacations.DataAccess.Contracts;
using Vacations.DataAccess.Models;

namespace Vacations.DataAccess.Implementations
{
    /// <summary>
    /// VacationPolicyRepository implementation
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class VacationPolicyRepository : IVacationPolicyRepository
    {

        private const string DbName = "erptest";


        /// <summary>
        /// Add VacationPolicy
        /// </summary>
        /// <param name="vacationPolicy">Object VacationPolicyModel</param>
        /// <returns>Return the employee ID inserted, an error text if something wrong happens</returns>
        public string Add(VacationPolicyModel vacationPolicy)
        {
            if (vacationPolicy == null) throw new ArgumentNullException("vacationPolicy");

            using (var dataContext = new Database(DbName))
            {
                dataContext.Insert(vacationPolicy);
                return vacationPolicy.Id.ToString();
            }

        }

        /// <summary>
        /// Update VacationPolicy
        /// </summary>
        /// <param name="vacationPolicy">Object VacationPolicyModel</param>
        /// <returns></returns>
        public bool Update(VacationPolicyModel vacationPolicy)
        {
            if (vacationPolicy == null) throw new ArgumentNullException("vacationPolicy");

            using (var dataContext = new Database(DbName))
            {
                dataContext.Update(vacationPolicy);
                return true;
            }
        }

        /// <summary>
        /// Delete an Employee
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Return an error text if something wrong happens</returns>
        public bool Delete(int id)
        {
            if (id < 1) throw new ArgumentException("id");

            var model = new VacationPolicyModel() { Id = id };

            using (var dataContext = new Database(DbName))
            {
                dataContext.Delete(model);
                return true;
            }
        }

        /// <summary>
        /// Get VacationPolicy
        /// </summary>
        /// <param name="id">VacationPolicyModel Id</param>
        /// <returns>Return a VacationPolicyModel object</returns>
        public VacationPolicyModel GetVacationPolicyModel(int id)
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
        /// <returns>Return a EmployeeModel collection</returns>
        public IEnumerable<VacationPolicyModel> GetVacationPolicies()
        {
            using (var dataContext = new Database(DbName))
            {
                return dataContext.Query<VacationPolicyModel>("SELECT * FROM vacation_policies");
            }
        }


    }
}
