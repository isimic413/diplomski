using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamPreparation.DAL.Common;

namespace ExamPreparation.DAL.Models
{
    public partial class TestingAreaProblem : ITestingAreaProblem
    {
        public TestingAreaProblem()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        [ForeignKey("Problem")]
        [Required]
        public System.Guid ProblemId { get; set; }
        public virtual Problem Problem { get; set; }

        [ForeignKey("TestingArea")]
        [Required]
        public System.Guid TestingAreaId { get; set; }
        public virtual TestingArea TestingArea { get; set; }
    }
}