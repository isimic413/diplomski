using System;
using System.Collections.Generic;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class Exam : IExam
    {
        public Exam()
        {
            this.ExamQuestions = new List<IExamQuestion>();
        }

        public System.Guid Id { get; set; }
        public System.Guid TestingAreaId { get; set; }
        public short Year { get; set; }
        public short Month { get; set; }
        public System.TimeSpan Duration { get; set; }
        public virtual ITestingArea TestingArea { get; set; }
        public virtual ICollection<IExamQuestion> ExamQuestions { get; set; }
    }
}