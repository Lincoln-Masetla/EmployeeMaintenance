using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeMaintenanceApi.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public int PersonId { get; set; }
        [Required, MaxLength(128)]
        public string LastName { get; set; }
        [Required, MaxLength(128)]
        public string FirstName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
        [Required, MaxLength(16)]
        public string EmployeeNum { get; set; }
        [Required]
        public DateTime EmployedDate { get; set; }
        public DateTime? TerminatedDate { get; set; }
    }
}