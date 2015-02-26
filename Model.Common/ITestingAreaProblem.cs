using System;

namespace ExamPreparation.Model.Common
{
    public interface ITestingAreaProblem
    {
        System.Guid Id { get; set; }
        System.Guid ProblemId { get; set; }
        System.Guid TestingAreaId { get; set; }
    }
}
