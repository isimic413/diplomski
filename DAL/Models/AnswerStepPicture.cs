using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamPreparation.DAL.Common;

namespace ExamPreparation.DAL.Models
{
    public partial class AnswerStepPicture : IAnswerStepPicture
    {
        public AnswerStepPicture()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        [ForeignKey("AnswerStep")]
        [Required]
        public System.Guid AnswerStepId { get; set; }
        public virtual AnswerStep AnswerStep { get; set; }

        [Required]
        [Display(Name = "Image")]
        public byte[] Picture { get; set; }
    }
}