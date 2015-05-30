using System;

namespace ExamPreparation.Model.Common
{
    public interface IQuestionPicture
    {
        System.Guid Id { get; set; }
        System.Guid QuestionId { get; set; }
        byte[] Picture { get; set; }
        System.DateTime DateCreated { get; set; }
        System.DateTime DateUpdated { get; set; }
        IQuestion Question { get; set; }
    }
}
