using System;

namespace ExamPreparation.DAL.Common
{
    public interface IExamProblem
    {
        System.Guid Id { get; set; }
        System.Guid ProblemId { get; set; }
        System.Guid ExamId { get; set; }
        System.SByte ProblemNumber { get; set; }
    }
}
