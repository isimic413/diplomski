using System;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class ProblemPicture : IProblemPicture
    {
        public System.Guid Id { get; set; }
        public System.Guid ProblemId { get; set; }
        public byte[] Picture { get; set; }
        public virtual Problem Problem { get; set; }
    }
}