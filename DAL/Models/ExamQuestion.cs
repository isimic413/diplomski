using System;
using System.Collections.Generic;

namespace ExamPreparation.DAL.Models
{
    public partial class ExamQuestion
    {
        public System.Guid Id { get; set; }
        public System.Guid QuestionId { get; set; }
        public System.Guid ExamId { get; set; }
        public short QuestionNumber { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual Question Question { get; set; }
    }
}
