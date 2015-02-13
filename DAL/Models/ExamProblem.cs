using System;
using System.Collections.Generic;

namespace ExamPreparation.DAL.Models
{
    public partial class ExamProblem
    {
        public System.Guid Id { get; set; }
        public System.Guid ProblemId { get; set; }
        public System.Guid ExamId { get; set; }
        public string ProblemNumber { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual Problem Problem { get; set; }
    }
}
