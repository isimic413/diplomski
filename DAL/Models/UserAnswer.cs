using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamPreparation.DAL.Common;

namespace ExamPreparation.DAL.Models
{
    public partial class UserAnswer : IUserAnswer
    {
        public UserAnswer()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        [ForeignKey("User")]
        [Required]
        public System.Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Problem")]
        [Required]
        public System.Guid ProblemId { get; set; }
        public virtual Problem Problem { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Answer text")]
        public string AnswerText { get; set; }

        [Required]
        [Display(Name = "Points")]
        public System.SByte Points { get; set; }
    }
}