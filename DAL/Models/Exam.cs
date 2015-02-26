using System;
using System.Collections.Generic;

namespace ExamPreparation.DAL.Models
{
    public partial class Exam
    {
        public Exam()
        {
            this.ExamProblems = new List<ExamProblem>();
        }

        public System.Guid Id { get; set; }
        public System.Guid TestingAreaId { get; set; }
        public short Year { get; set; }
        public short Month { get; set; }
        public System.TimeSpan Duration { get; set; }
        public virtual TestingArea TestingArea { get; set; }
        public virtual ICollection<ExamProblem> ExamProblems { get; set; }
    }
}
