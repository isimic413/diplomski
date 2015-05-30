using System;
using System.Collections.Generic;

namespace ExamPreparation.DAL.Models
{
    public partial class AnswerStepPicture
    {
        public System.Guid Id { get; set; }
        public System.Guid AnswerStepId { get; set; }
        public byte[] Picture { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public virtual AnswerStep AnswerStep { get; set; }
    }
}
