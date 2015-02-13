using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ExamPreparation.DAL.Models.Mapping
{
    public class ExamMap : EntityTypeConfiguration<Exam>
    {
        public ExamMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Exam");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TestingAreaId).HasColumnName("TestingAreaId");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.duration).HasColumnName("duration");

            // Relationships
            this.HasRequired(t => t.TestingArea)
                .WithMany(t => t.Exams)
                .HasForeignKey(d => d.TestingAreaId);

        }
    }
}
