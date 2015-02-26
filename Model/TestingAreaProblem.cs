using System;
using System.Collections.Generic;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class TestingAreaProblem : ITestingAreaProblem
    {
        public System.Guid Id { get; set; }
        public System.Guid ProblemId { get; set; }
        public System.Guid TestingAreaId { get; set; }
        public virtual Problem Problem { get; set; }
        public virtual TestingArea TestingArea { get; set; }
    }
}