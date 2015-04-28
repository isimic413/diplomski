using System;

namespace ExamPreparation.Model.Common
{
    public interface IAnswerChoicePicture
    {
        System.Guid Id { get; set; }
        System.Guid AnswerChoiceId { get; set; }
        byte[] Picture { get; set; }
        IAnswerChoice AnswerChoice { get; set; }
    }
}
