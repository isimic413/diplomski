using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamPreparation.DAL.Common;

namespace ExamPreparation.DAL.Models
{
    public partial class Exam : IExam
    {
        public Exam()
        {
        }

        [Key]
        public System.Guid Id { get; set; }


        [ForeignKey("TestingArea")]
        [Required]
        public System.Guid TestingAreaId { get; set; }
        public virtual TestingArea TestingArea { get; set; }

        [Required(ErrorMessage = "*")]
        [Range(minimum: 2009, maximum: 3000, ErrorMessage = "Year must be at least 2009.")]
        public System.Int16 Year { get; set; }

        [Required(ErrorMessage = "*")]
        [Range(minimum: 1, maximum: 12, ErrorMessage = "Month must be between 1 and 12.")]
        public System.SByte Month { get; set; }

        [Required(ErrorMessage = "*")]
        [Range(minimum: 0, maximum: 20000, ErrorMessage = "Duration must be between 0 and 20000 seconds.")]
        [Display(Name = "Exam duration (in seconds)")]
        public System.Int16 Duration { get; set; }

        public virtual ICollection<ExamProblem> Problems { get; set; }
    }
}