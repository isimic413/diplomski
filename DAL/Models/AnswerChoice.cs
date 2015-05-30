using System;
using System.Collections.Generic;

namespace ExamPreparation.DAL.Models
{
    public partial class AnswerChoice
    {
        public AnswerChoice()
        {
            this.AnswerChoicePictures = new List<AnswerChoicePicture>();
        }

        public System.Guid Id { get; set; }
        public System.Guid QuestionId { get; set; }
        public bool IsCorrect { get; set; }
        public string Text { get; set; }
        public virtual Question Question { get; set; }
        public virtual ICollection<AnswerChoicePicture> AnswerChoicePictures { get; set; }
    }
}
