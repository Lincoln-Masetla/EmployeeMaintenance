using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace DataAccessLayer.Models
{
    public class Person 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }
        [Required, MaxLength(128)]
        public string LastName { get; set; }
        [Required, MaxLength(128)]
        public string FirstName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; } 
        public bool Active { get; set; }

        public DateTime CreateDate { get; set; }

    }
}