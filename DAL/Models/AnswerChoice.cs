using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamPreparation.DAL.Common;

namespace ExamPreparation.DAL.Models
{
    public partial class AnswerChoice : IAnswerChoice
    {
        public AnswerChoice()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        [ForeignKey("Problem")]
        [Required]
        [Index("CorrectAnswer", 1)]
        public System.Guid ProblemId { get; set; }
        public virtual Problem Problem { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Choice text")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Has picture")]
        public Boolean HasPicture { get; set; }

        [Required]
        [Display(Name = "Is correct")]
        [Index("CorrectAnswer", 2)]
        public Boolean IsCorrect { get; set; }
    }
}