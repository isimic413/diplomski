using System;

namespace ExamPreparation.Model.Common
{
    public interface IAnswerStep
    {
        System.Guid Id { get; set; }
        System.Guid ProblemId { get; set; }
        short StepNumber { get; set; }
        byte Points { get; set; }
        string Text { get; set; }
        bool HasPicture { get; set; }
    }
}
