using System;
using System.Collections.Generic;

namespace ExamPreparation.DAL.Models
{
    public partial class Problem
    {
        public Problem()
        {
            this.AnswerChoices = new List<AnswerChoice>();
            this.AnswerSteps = new List<AnswerStep>();
            this.ExamProblems = new List<ExamProblem>();
            this.ProblemPictures = new List<ProblemPicture>();
            this.TestingAreaProblems = new List<TestingAreaProblem>();
            this.UserAnswers = new List<UserAnswer>();
        }

        public System.Guid Id { get; set; }
        public System.Guid ProblemTypeId { get; set; }
        public string Text { get; set; }
        public byte Points { get; set; }
        public bool HasPicture { get; set; }
        public bool HasSteps { get; set; }
        public virtual ICollection<AnswerChoice> AnswerChoices { get; set; }
        public virtual ICollection<AnswerStep> AnswerSteps { get; set; }
        public virtual ICollection<ExamProblem> ExamProblems { get; set; }
        public virtual ProblemType ProblemType { get; set; }
        public virtual ICollection<ProblemPicture> ProblemPictures { get; set; }
        public virtual ICollection<TestingAreaProblem> TestingAreaProblems { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
