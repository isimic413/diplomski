using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ExamPreparation.DAL.Models.Mapping
{
    public class ProblemMap : EntityTypeConfiguration<Problem>
    {
        public ProblemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Problem");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProblemTypeId).HasColumnName("ProblemTypeId");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.Points).HasColumnName("Points");
            this.Property(t => t.HasPicture).HasColumnName("HasPicture");
            this.Property(t => t.HasSteps).HasColumnName("HasSteps");

            // Relationships
            this.HasRequired(t => t.ProblemType)
                .WithMany(t => t.Problems)
                .HasForeignKey(d => d.ProblemTypeId);

        }
    }
}
