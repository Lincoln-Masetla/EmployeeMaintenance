using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        public int PersonId { get; set; }
        [Required, MaxLength(16)]
        public string EmployeeNum { get; set; }
        [Required]
        public DateTime EmployedDate { get; set; }
        public DateTime? TerminatedDate { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }

        [ForeignKey("PersonId")]
        public Person Persons { get; set; }
    }
}