using System;

namespace ExamPreparation.DAL.Common
{
    interface IAnswerChoicePicture
    {
        System.Guid Id { get; set; }
        System.Guid AnswerChoiceId { get; set; }
        byte[] Picture { get; set; }
    }
}
