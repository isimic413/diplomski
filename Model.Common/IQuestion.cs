using System;
using System.Collections.Generic;

namespace ExamPreparation.Model.Common
{
    public interface IQuestion
    {
        System.Guid Id { get; set; }
        System.Guid TestingAreaId { get; set; }
        System.Guid QuestionTypeId { get; set; }
        string Text { get; set; }
        byte Points { get; set; }
        ICollection<IAnswerChoice> AnswerChoices { get; set; }
        ICollection<IAnswerStep> AnswerSteps { get; set; }
        ICollection<IExamQuestion> ExamQuestions { get; set; }
        IQuestionType QuestionType { get; set; }
        ITestingArea TestingArea { get; set; }
        ICollection<IQuestionPicture> QuestionPictures { get; set; }
        ICollection<IUserAnswer> UserAnswers { get; set; }
    }
}
