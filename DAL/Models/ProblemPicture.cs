using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamPreparation.DAL.Common;

namespace ExamPreparation.DAL.Models
{
    public partial class ProblemPicture : IProblemPicture
    {
        public ProblemPicture()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        [ForeignKey("Problem")]
        [Required]
        public System.Guid ProblemId { get; set; }
        public virtual Problem Problem { get; set; }

        [Required]
        [Display(Name = "Image")]
        public byte[] Picture { get; set; }
    }
}