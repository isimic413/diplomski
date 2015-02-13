using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ExamPreparation.DAL.Models.Mapping
{
    public class AnswerStepMap : EntityTypeConfiguration<AnswerStep>
    {
        public AnswerStepMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("AnswerStep");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProblemId).HasColumnName("ProblemId");
            this.Property(t => t.StepNumber).HasColumnName("StepNumber");
            this.Property(t => t.Points).HasColumnName("Points");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.HasPicture).HasColumnName("HasPicture");

            // Relationships
            this.HasRequired(t => t.Problem)
                .WithMany(t => t.AnswerSteps)
                .HasForeignKey(d => d.ProblemId);

        }
    }
}
