using System;

namespace ExamPreparation.DAL.Common
{
    public interface IAnswerChoice
    {
        System.Guid Id { get; set; }
        System.Guid ProblemId { get; set; }
        string Text { get; set; }
        Boolean HasPicture { get; set; }
        Boolean IsCorrect { get; set; }
    }
}
