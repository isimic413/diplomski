using System;
using System.Collections.Generic;

namespace ExamPreparation.DAL.Models
{
    public partial class ProblemType
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
