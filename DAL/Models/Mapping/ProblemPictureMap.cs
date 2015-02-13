using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ExamPreparation.DAL.Models.Mapping
{
    public class ProblemPictureMap : EntityTypeConfiguration<ProblemPicture>
    {
        public ProblemPictureMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Picture)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProblemPicture");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProblemId).HasColumnName("ProblemId");
            this.Property(t => t.Picture).HasColumnName("Picture");

            // Relationships
            this.HasRequired(t => t.Problem)
                .WithMany(t => t.ProblemPictures)
                .HasForeignKey(d => d.ProblemId);

        }
    }
}
