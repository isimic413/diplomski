using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamPreparation.DAL.Common;

namespace ExamPreparation.DAL.Models
{
    public partial class AnswerChoicePicture : IAnswerChoicePicture
    {
        public AnswerChoicePicture()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        [ForeignKey("AnswerChoice")]
        [Required]
        public System.Guid AnswerChoiceId { get; set; }
        public virtual AnswerChoice AnswerChoice { get; set; }

        [Required]
        [Display(Name = "Image")]
        public byte[] Picture { get; set; }
    }
}