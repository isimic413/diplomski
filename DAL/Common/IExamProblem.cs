using System;

namespace ExamPreparation.DAL.Common
{
    interface IExamProblem
    {
        System.Guid Id { get; set; }
        System.Guid ProblemId { get; set; }
        System.Guid ExamId { get; set; }
        System.SByte ProblemNumber { get; set; }
    }
}
