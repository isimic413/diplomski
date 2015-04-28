using System;

namespace ExamPreparation.Model.Common
{
    public interface IUserAnswer
    {
        System.Guid Id { get; set; }
        System.Guid UserId { get; set; }
        System.Guid QuestionId { get; set; }
        string AnswerText { get; set; }
        byte Points { get; set; }
        IQuestion Question { get; set; }
        IUser User { get; set; }
    }
}
