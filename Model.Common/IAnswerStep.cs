using System;
using System.Collections.Generic;

namespace ExamPreparation.Model.Common
{
    public interface IAnswerStep
    {
        System.Guid Id { get; set; }
        System.Guid QuestionId { get; set; }
        short StepNumber { get; set; }
        byte Points { get; set; }
        string Text { get; set; }
        IQuestion Question { get; set; }
        ICollection<IAnswerStepPicture> AnswerStepPictures { get; set; }
    }
}
