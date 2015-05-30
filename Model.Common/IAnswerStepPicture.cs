using System;

namespace ExamPreparation.Model.Common
{
    public interface IAnswerStepPicture
    {
        System.Guid Id { get; set; }
        System.Guid AnswerStepId { get; set; }
        byte[] Picture { get; set; }
        System.DateTime DateCreated { get; set; }
        System.DateTime DateUpdated { get; set; }
        IAnswerStep AnswerStep { get; set; }
    }
}
