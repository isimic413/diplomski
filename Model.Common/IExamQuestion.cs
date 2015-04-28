using System;

namespace ExamPreparation.Model.Common
{
    public interface IExamQuestion
    {
        System.Guid Id { get; set; }
        System.Guid QuestionId { get; set; }
        System.Guid ExamId { get; set; }
        short QuestionNumber { get; set; }
        IExam Exam { get; set; }
        IQuestion Question { get; set; }
    }
}
