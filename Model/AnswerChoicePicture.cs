using System;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class AnswerChoicePicture : IAnswerChoicePicture
    {
        public System.Guid Id { get; set; }
        public System.Guid AnswerChoiceId { get; set; }
        public byte[] Picture { get; set; }
        public virtual IAnswerChoice AnswerChoice { get; set; }
    }
}