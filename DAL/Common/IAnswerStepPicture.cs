using System;

namespace ExamPreparation.DAL.Common
{
    interface IAnswerStepPicture
    {
        System.Guid Id { get; set; }
        System.Guid AnswerStepId { get; set; }
        byte[] Picture { get; set; }
    }
}
