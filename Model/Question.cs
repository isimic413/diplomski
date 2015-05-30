using System;
using System.Collections.Generic;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class Question : IQuestion
    {
        public Question()
        {
            this.AnswerChoices = new List<IAnswerChoice>();
            this.AnswerSteps = new List<IAnswerStep>();
            this.ExamQuestions = new List<IExamQuestion>();
            this.QuestionPictures = new List<IQuestionPicture>();
            this.UserAnswers = new List<IUserAnswer>();
        }

        public System.Guid Id { get; set; }
        public System.Guid TestingAreaId { get; set; }
        public System.Guid QuestionTypeId { get; set; }
        public string Text { get; set; }
        public byte Points { get; set; }
        public virtual ICollection<IAnswerChoice> AnswerChoices { get; set; }
        public virtual ICollection<IAnswerStep> AnswerSteps { get; set; }
        public virtual ICollection<IExamQuestion> ExamQuestions { get; set; }
        public virtual IQuestionType QuestionType { get; set; }
        public virtual ITestingArea TestingArea { get; set; }
        public virtual ICollection<IQuestionPicture> QuestionPictures { get; set; }
        public virtual ICollection<IUserAnswer> UserAnswers { get; set; }
    }
}