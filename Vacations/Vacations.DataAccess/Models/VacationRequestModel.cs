using System;
using PetaPoco;

namespace Vacations.DataAccess.Models
{
    [TableName("vacation_requests")]
    [PrimaryKey("Id")]
    public class VacationRequestModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// Approved by manager
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// Start date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}