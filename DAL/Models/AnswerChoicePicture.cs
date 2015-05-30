using System;
using System.Collections.Generic;

namespace ExamPreparation.DAL.Models
{
    public partial class AnswerChoicePicture
    {
        public System.Guid Id { get; set; }
        public System.Guid AnswerChoiceId { get; set; }
        public byte[] Picture { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public virtual AnswerChoice AnswerChoice { get; set; }
    }
}
