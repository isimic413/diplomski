using System;
using System.Collections.Generic;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class QuestionType : IQuestionType
    {
        public QuestionType()
        {
            this.Questions = new List<IQuestion>();
        }

        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string Abrv { get; set; }
        public virtual ICollection<IQuestion> Questions { get; set; }
    }
}