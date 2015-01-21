using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ExamPreparation.DAL.Common;


namespace ExamPreparation.DAL.Models
{
    public partial class TestingArea : ITestingArea
    {
        public TestingArea()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Abbreviation must be 5 characters long.")]
        [Display(Name = "Exam type abbreviation")]
        public string Abrv { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Type name must be between 5 and 20 characters long.")]
        [Display(Name = "Exam type title")]
        public string Title { get; set; }

        public virtual ICollection<TestingAreaProblem> Problems { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
    }
}