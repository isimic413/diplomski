using System;
using System.Collections.Generic;

namespace ExamPreparation.DAL.Models
{
    public partial class Question
    {
        public Question()
        {
            this.AnswerChoices = new List<AnswerChoice>();
            this.AnswerSteps = new List<AnswerStep>();
            this.ExamQuestions = new List<ExamQuestion>();
            this.QuestionPictures = new List<QuestionPicture>();
            this.UserAnswers = new List<UserAnswer>();
        }

        public System.Guid Id { get; set; }
        public System.Guid TestingAreaId { get; set; }
        public System.Guid QuestionTypeId { get; set; }
        public string Text { get; set; }
        public byte Points { get; set; }
        public bool HasPicture { get; set; }
        public bool HasSteps { get; set; }
        public virtual ICollection<AnswerChoice> AnswerChoices { get; set; }
        public virtual ICollection<AnswerStep> AnswerSteps { get; set; }
        public virtual ICollection<ExamQuestion> ExamQuestions { get; set; }
        public virtual QuestionType QuestionType { get; set; }
        public virtual TestingArea TestingArea { get; set; }
        public virtual ICollection<QuestionPicture> QuestionPictures { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
