using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ExamPreparation.DAL.Models.Mapping
{
    public class TestingAreaProblemMap : EntityTypeConfiguration<TestingAreaProblem>
    {
        public TestingAreaProblemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("TestingAreaProblem");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProblemId).HasColumnName("ProblemId");
            this.Property(t => t.TestingAreaId).HasColumnName("TestingAreaId");

            // Relationships
            this.HasRequired(t => t.Problem)
                .WithMany(t => t.TestingAreaProblems)
                .HasForeignKey(d => d.ProblemId);
            this.HasRequired(t => t.TestingArea)
                .WithMany(t => t.TestingAreaProblems)
                .HasForeignKey(d => d.TestingAreaId);

        }
    }
}
