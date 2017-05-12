using System.Collections.Generic;
using Vacations.DataAccess.Models;

namespace Vacations.DataAccess.Contracts
{
    public interface IVacationRequestRepository
    {

        /// <summary>
        /// Add VacationRequest
        /// </summary>
        /// <param name="vacationRequest">Object VacationRequestModel</param>
        /// <returns>Return the employee ID inserted, an error text if something wrong happens</returns>
        string Add(VacationRequestModel vacationRequest);

        /// <summary>
        /// Update VacationRequest
        /// </summary>
        /// <param name="vacationRequest">VacationRequestModel</param>
        /// <returns>true on success</returns>
        bool Update(VacationRequestModel vacationRequest);

        /// <summary>
        /// Delete VacationRequest
        /// </summary>
        /// <param name="id">VacationRequestModel Id</param>
        /// <returns>true on success</returns>
        bool Delete(int id);

        /// <summary>
        /// Get VacationRequest
        /// </summary>
        /// <param name="id">VacationRequestModel Id</param>
        /// <returns>Return a VacationRequestModel object</returns>
        VacationRequestModel GetVacationRequest(int id);

        /// <summary>
        /// Get VacationRequest list
        /// </summary>
        /// <returns>Return a VacationRequestModel collection</returns>
        IEnumerable<VacationRequestModel> GetVacationRequests();

    }
}
