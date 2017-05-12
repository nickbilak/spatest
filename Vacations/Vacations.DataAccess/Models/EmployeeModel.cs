using System;
using System.Collections.Generic;
using PetaPoco;
using Vacations.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace Vacations.DataAccess.Models
{
    [TableName("employees")]
    [PrimaryKey("Id")]
    public class EmployeeModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Employee Type
        /// </summary>
        [PetaPoco.Column("type_id")]
        public EmployeeType Type { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PasswordClear { get; set; }

        /// <summary>
        /// PasswordHash
        /// </summary>
        [PetaPoco.Column("password")]
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [Display(Name = "Login")]
        public string Email { get; set; }

        /// <summary>
        /// Start date
        /// </summary>
        [PetaPoco.Column("start_date")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// End date
        /// </summary>
        [PetaPoco.Column("end_date")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public string Position { get; set; }

        public bool AutoLogon { get; set;  }

        /// <summary>
        /// Employee List
        /// </summary>
        public List<EmployeeModel> EmployeeList { get; set; }

        /// <summary>
        /// Error Model
        /// </summary>
        public ErrorModel Error { get; set; }
    }
}