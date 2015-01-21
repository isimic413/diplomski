using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamPreparation.DAL.Common;

namespace ExamPreparation.DAL.Models
{
    public partial class AnswerStep : IAnswerStep
    {
        public AnswerStep()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        [ForeignKey("Problem")]
        [Required]
        public System.Guid ProblemId { get; set; }
        public virtual Problem Problem { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Step number")]
        public System.Int16 StepNumber { get; set; } // Int16 = smallint

        [Required]
        [Display(Name = "Points")]
        public System.SByte Points { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Problem text")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Has picture")]
        public Boolean HasPicture { get; set; }
    }
}