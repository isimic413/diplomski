using System;

namespace ExamPreparation.Model.Common
{
    public interface IAnswerChoice
    {
        System.Guid Id { get; set; }
        System.Guid ProblemId { get; set; }
        bool IsCorrect { get; set; }
        string Text { get; set; }
        bool HasPicture { get; set; }
    }
}
