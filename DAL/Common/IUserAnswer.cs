using System;

namespace ExamPreparation.DAL.Common
{
    public interface IUserAnswer
    {
        System.Guid Id { get; set; }
        System.Guid UserId { get; set; }
        System.Guid ProblemId { get; set; }
        string AnswerText { get; set; }
        System.SByte Points { get; set; }
    }
}
