using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ExamPreparation.DAL.Models.Mapping
{
    public class AnswerChoiceMap : EntityTypeConfiguration<AnswerChoice>
    {
        public AnswerChoiceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("AnswerChoice");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.QuestionId).HasColumnName("QuestionId");
            this.Property(t => t.IsCorrect).HasColumnName("IsCorrect");
            this.Property(t => t.Text).HasColumnName("Text");

            // Relationships
            this.HasRequired(t => t.Question)
                .WithMany(t => t.AnswerChoices)
                .HasForeignKey(d => d.QuestionId);

        }
    }
}
