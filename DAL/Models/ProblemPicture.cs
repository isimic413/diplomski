using System;
using System.Collections.Generic;

namespace ExamPreparation.DAL.Models
{
    public partial class ProblemPicture
    {
        public System.Guid Id { get; set; }
        public System.Guid ProblemId { get; set; }
        public byte[] Picture { get; set; }
        public virtual Problem Problem { get; set; }
    }
}
