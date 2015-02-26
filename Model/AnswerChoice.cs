using System;
using System.Collections.Generic;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class AnswerChoice : IAnswerChoice
    {
        public AnswerChoice()
        {
            this.AnswerChoicePictures = new List<AnswerChoicePicture>();
        }

        public System.Guid Id { get; set; }
        public System.Guid ProblemId { get; set; }
        public bool IsCorrect { get; set; }
        public string Text { get; set; }
        public bool HasPicture { get; set; }
        public virtual Problem Problem { get; set; }
        public virtual ICollection<AnswerChoicePicture> AnswerChoicePictures { get; set; }
    }
}
