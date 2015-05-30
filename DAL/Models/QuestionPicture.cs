using System;
using System.Collections.Generic;

namespace ExamPreparation.DAL.Models
{
    public partial class QuestionPicture
    {
        public System.Guid Id { get; set; }
        public System.Guid QuestionId { get; set; }
        public byte[] Picture { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public virtual Question Question { get; set; }
    }
}
