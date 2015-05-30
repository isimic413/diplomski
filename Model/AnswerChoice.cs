using System;
using System.Collections.Generic;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class AnswerChoice : IAnswerChoice
    {
        public AnswerChoice()
        {
            this.AnswerChoicePictures = new List<IAnswerChoicePicture>();
        }

        public System.Guid Id { get; set; }
        public System.Guid QuestionId { get; set; }
        public bool IsCorrect { get; set; }
        public string Text { get; set; }
        public virtual IQuestion Question { get; set; }
        public virtual ICollection<IAnswerChoicePicture> AnswerChoicePictures { get; set; }
    }
}
