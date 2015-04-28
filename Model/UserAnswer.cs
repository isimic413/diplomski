using System;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class UserAnswer : IUserAnswer
    {
        public System.Guid Id { get; set; }
        public System.Guid UserId { get; set; }
        public System.Guid QuestionId { get; set; }
        public string AnswerText { get; set; }
        public byte Points { get; set; }
        public virtual IQuestion Question { get; set; }
        public virtual IUser User { get; set; }
    }
}