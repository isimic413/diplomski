using System;

namespace ExamPreparation.Model.Common
{
    public interface IExamProblem
    {
        System.Guid Id { get; set; }
        System.Guid ProblemId { get; set; }
        System.Guid ExamId { get; set; }
        short ProblemNumber { get; set; }
    }
}
