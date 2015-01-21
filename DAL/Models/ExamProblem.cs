using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamPreparation.DAL.Common;

namespace ExamPreparation.DAL.Models
{
    public partial class ExamProblem : IExamProblem
    {
        public ExamProblem()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        [ForeignKey("Problem")]
        [Required]
        public System.Guid ProblemId { get; set; }
        public virtual Problem Problem { get; set; }

        [ForeignKey("Exam")]
        [Required]
        public System.Guid ExamId { get; set; }
        public virtual Exam Exam { get; set; }

        [Required(ErrorMessage = "*")]
        [Range(minimum: 1, maximum: 100, ErrorMessage = "Problem number must be between 1 and 100.")]
        public System.SByte ProblemNumber { get; set; }
    }
}