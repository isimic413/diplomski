using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ExamPreparation.DAL.Common;

namespace ExamPreparation.DAL.Models
{
    public partial class ProblemType : IProblemType
    {
        public ProblemType()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Abbreviation must be 5 characters long.")]
        [Display(Name = "Problem type abbreviation")]
        public string Abrv { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Type name must be between 5 and 20 characters long.")]
        [Display(Name = "Problem type title")]
        public string Title { get; set; }

        public virtual ICollection<Problem> Problems { get; set; }
    }
}