using System;
using System.Collections.Generic;

namespace ExamPreparation.DAL.Models
{
    public partial class TestingAreaProblem
    {
        public System.Guid Id { get; set; }
        public System.Guid ProblemId { get; set; }
        public System.Guid TestingAreaId { get; set; }
        public virtual Problem Problem { get; set; }
        public virtual TestingArea TestingArea { get; set; }
    }
}
