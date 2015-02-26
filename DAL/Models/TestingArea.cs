using System;
using System.Collections.Generic;

namespace ExamPreparation.DAL.Models
{
    public partial class TestingArea
    {
        public TestingArea()
        {
            this.Exams = new List<Exam>();
            this.TestingAreaProblems = new List<TestingAreaProblem>();
        }

        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string Abrv { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<TestingAreaProblem> TestingAreaProblems { get; set; }
    }
}
