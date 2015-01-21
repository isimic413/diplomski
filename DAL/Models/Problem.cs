using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamPreparation.DAL.Common;

namespace ExamPreparation.DAL.Models
{
    public partial class Problem : IProblem
    {
        public Problem()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        [ForeignKey("ProblemType")]
        [Required]
        public System.Guid ProblemTypeId { get; set; }
        public virtual ProblemType ProblemType { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Problem text")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Points")]
        public System.SByte Points { get; set; }

        [Required]
        [Display(Name = "Has picture")]
        public Boolean HasPicture { get; set; }

        [Required]
        [Display(Name = "Has steps")]
        public Boolean HasSteps { get; set; }

        public virtual ICollection<TestingAreaProblem> TestingAreas { get; set; }
        public virtual ICollection<ExamProblem> Exams { get; set; }
        public virtual ICollection<AnswerStep> Steps { get; set; }
        public virtual ICollection<AnswerChoice> Choices { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}