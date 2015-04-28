using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ExamPreparation.DAL.Models.Mapping
{
    public class ExamQuestionMap : EntityTypeConfiguration<ExamQuestion>
    {
        public ExamQuestionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ExamQuestion");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.QuestionId).HasColumnName("QuestionId");
            this.Property(t => t.ExamId).HasColumnName("ExamId");
            this.Property(t => t.QuestionNumber).HasColumnName("QuestionNumber");

            // Relationships
            this.HasRequired(t => t.Exam)
                .WithMany(t => t.ExamQuestions)
                .HasForeignKey(d => d.ExamId);
            this.HasRequired(t => t.Question)
                .WithMany(t => t.ExamQuestions)
                .HasForeignKey(d => d.QuestionId);

        }
    }
}
