using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamPreparation.DAL.Common;

namespace ExamPreparation.DAL.Models
{
    public partial class Role : IRole
    {
        public Role()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Abbreviation must be 5 characters long.")]
        [Display(Name = "Role abbreviation")]
        public string Abrv { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Role title must be between 5 and 20 characters long.")]
        [Display(Name = "Role title")]
        public string Title { get; set; }

        public virtual ICollection<UserRole> Users { get; set; }
    }
}