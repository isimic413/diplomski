using System;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class ExamQuestion : IExamQuestion
    {
        public System.Guid Id { get; set; }
        public System.Guid QuestionId { get; set; }
        public System.Guid ExamId { get; set; }
        public short QuestionNumber { get; set; }
        public virtual IExam Exam { get; set; }
        public virtual IQuestion Question { get; set; }
    }
}