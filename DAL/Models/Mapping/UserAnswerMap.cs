using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ExamPreparation.DAL.Models.Mapping
{
    public class UserAnswerMap : EntityTypeConfiguration<UserAnswer>
    {
        public UserAnswerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.AnswerText)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UserAnswer");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.QuestionId).HasColumnName("QuestionId");
            this.Property(t => t.AnswerText).HasColumnName("AnswerText");
            this.Property(t => t.Points).HasColumnName("Points");

            // Relationships
            this.HasRequired(t => t.Question)
                .WithMany(t => t.UserAnswers)
                .HasForeignKey(d => d.QuestionId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserAnswers)
                .HasForeignKey(d => d.UserId);

        }
    }
}
