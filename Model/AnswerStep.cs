using System;
using System.Collections.Generic;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class AnswerStep : IAnswerStep
    {
        public AnswerStep()
        {
            this.AnswerStepPictures = new List<IAnswerStepPicture>();
        }

        public System.Guid Id { get; set; }
        public System.Guid QuestionId { get; set; }
        public short StepNumber { get; set; }
        public byte Points { get; set; }
        public string Text { get; set; }
        public bool HasPicture { get; set; }
        public virtual IQuestion Question { get; set; }
        public virtual ICollection<IAnswerStepPicture> AnswerStepPictures { get; set; }
    }
}