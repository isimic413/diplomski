using System;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class UserAnswer : IUserAnswer
    {
        public System.Guid Id { get; set; }
        public System.Guid UserId { get; set; }
        public System.Guid ProblemId { get; set; }
        public string AnswerText { get; set; }
        public byte Points { get; set; }
        public virtual Problem Problem { get; set; }
        public virtual User User { get; set; }
    }
}