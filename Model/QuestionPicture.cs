using System;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class QuestionPicture : IQuestionPicture
    {
        public System.Guid Id { get; set; }
        public System.Guid QuestionId { get; set; }
        public byte[] Picture { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public virtual IQuestion Question { get; set; }
    }
}