using System;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class AnswerStepPicture : IAnswerStepPicture
    {
        public System.Guid Id { get; set; }
        public System.Guid AnswerStepId { get; set; }
        public byte[] Picture { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public virtual IAnswerStep AnswerStep { get; set; }
    }
}