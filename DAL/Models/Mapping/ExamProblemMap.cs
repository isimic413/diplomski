using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ExamPreparation.DAL.Models.Mapping
{
    public class ExamProblemMap : EntityTypeConfiguration<ExamProblem>
    {
        public ExamProblemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ProblemNumber)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ExamProblem");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProblemId).HasColumnName("ProblemId");
            this.Property(t => t.ExamId).HasColumnName("ExamId");
            this.Property(t => t.ProblemNumber).HasColumnName("ProblemNumber");

            // Relationships
            this.HasRequired(t => t.Exam)
                .WithMany(t => t.ExamProblems)
                .HasForeignKey(d => d.ExamId);
            this.HasRequired(t => t.Problem)
                .WithMany(t => t.ExamProblems)
                .HasForeignKey(d => d.ProblemId);

        }
    }
}
