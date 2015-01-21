using System;

namespace ExamPreparation.DAL.Common
{
    interface ITestingAreaProblem
    {
        System.Guid Id { get; set; }
        System.Guid ProblemId { get; set; }
        System.Guid TestingAreaId { get; set; }
    }
}
