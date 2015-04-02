using System;

namespace ExamPreparation.Model.Common
{
    public interface IProblem
    {
        System.Guid Id { get; set; }
        System.Guid TestingAreaId { get; set; }
        System.Guid ProblemTypeId { get; set; }
        string Text { get; set; }
        byte Points { get; set; }
        bool HasPicture { get; set; }
        bool HasSteps { get; set; }
    }
}
