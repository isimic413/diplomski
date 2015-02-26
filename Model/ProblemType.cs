using System;
using System.Collections.Generic;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class ProblemType : IProblemType
    {
        public ProblemType()
        {
            this.Problems = new List<Problem>();
        }

        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string Abrv { get; set; }
        public virtual ICollection<Problem> Problems { get; set; }
    }
}