using System;
using System.Collections.Generic;

namespace ExamPreparation.Model.Common
{
    public interface IAnswerChoice
    {
        System.Guid Id { get; set; }
        System.Guid QuestionId { get; set; }
        bool IsCorrect { get; set; }
        string Text { get; set; }
        IQuestion Question { get; set; }
        ICollection<IAnswerChoicePicture> AnswerChoicePictures { get; set; }
    }
}
