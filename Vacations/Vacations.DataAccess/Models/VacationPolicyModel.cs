using PetaPoco;

namespace Vacations.DataAccess.Models
{
    [TableName("vacation_policies")]
    [PrimaryKey("Id")]
    public class VacationPolicyModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Years of Service
        /// </summary>
        public int ServiceYears { get; set; }

        /// <summary>
        /// Number of vacation days
        /// </summary>
        public int Days { get; set; }
    }
}