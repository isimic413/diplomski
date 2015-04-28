using System;
using System.Collections.Generic;

namespace ExamPreparation.DAL.Models
{
    public partial class UserAnswer
    {
        public System.Guid Id { get; set; }
        public System.Guid UserId { get; set; }
        public System.Guid QuestionId { get; set; }
        public string AnswerText { get; set; }
        public byte Points { get; set; }
        public virtual Question Question { get; set; }
        public virtual User User { get; set; }
    }
}
