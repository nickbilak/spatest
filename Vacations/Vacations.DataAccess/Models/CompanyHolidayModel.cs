using System;
using PetaPoco;

namespace Vacations.DataAccess.Models
{
    [TableName("company_holidays")]
    [PrimaryKey("Id")]
    public class CompanyHolidayModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Holiday Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Holiday Date
        /// </summary>
        public DateTime HolidayDate { get; set; }
    }
}
