using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using PetaPoco;
using Vacations.DataAccess.Contracts;
using Vacations.DataAccess.Models;

namespace Vacations.DataAccess.Implementations
{
    /// <summary>
    /// VacationRequestRepository implementation
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class VacationRequestRepository : IVacationRequestRepository
    {

        private const string DbName = "erptest";

        /// <summary>
        /// Add VacationRequest
        /// </summary>
        /// <param name="vacationRequest">Object VacationRequestModel</param>
        /// <returns>Return the employee ID inserted, an error text if something wrong happens</returns>
        public string Add(VacationRequestModel vacationRequest)
        {
            if (vacationRequest == null) throw new ArgumentNullException("vacationRequest");

            using (var dataContext = new Database(DbName))
            {
                dataContext.Insert(vacationRequest);
                return vacationRequest.Id.ToString();
            }

        }

        /// <summary>
        /// Update VacationRequest
        /// </summary>
        /// <param name="vacationRequest">VacationRequestModel</param>
        /// <returns>true on success</returns>
        public bool Update(VacationRequestModel vacationRequest)
        {
            if (vacationRequest == null) throw new ArgumentNullException("vacationRequest");

            using (var dataContext = new Database(DbName))
            {
                dataContext.Update(vacationRequest);
                return true;
            }
        }

        /// <summary>
        /// Delete VacationRequest
        /// </summary>
        /// <param name="id">VacationRequestModel Id</param>
        /// <returns>true on success</returns>
        public bool Delete(int id)
        {
            if (id < 1) throw new ArgumentException("id");

            var model = new VacationRequestModel() { Id = id };

            using (var dataContext = new Database(DbName))
            {
                dataContext.Delete(model);
                return true;
            }
        }

        /// <summary>
        /// Get VacationRequest
        /// </summary>
        /// <param name="id">VacationRequestModel Id</param>
        /// <returns>Return a VacationRequestModel object</returns>
        public VacationRequestModel GetVacationRequest(int id)
        {
            if (id < 1) throw new ArgumentException("id");

            using (var dataContext = new Database(DbName))
            {
                return dataContext.SingleOrDefault<VacationRequestModel>("SELECT * FROM vacation_requests WHERE id=@0", id);
            }
        }

        /// <summary>
        /// Get VacationRequest list
        /// </summary>
        /// <returns>Return a VacationRequestModel collection</returns>
        public IEnumerable<VacationRequestModel> GetVacationRequests()
        {
            using (var dataContext = new Database(DbName))
            {
                return dataContext.Query<VacationRequestModel>("SELECT * FROM vacation_requests");
            }
        }

    }
}
