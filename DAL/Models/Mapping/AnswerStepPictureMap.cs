using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ExamPreparation.DAL.Models.Mapping
{
    public class AnswerStepPictureMap : EntityTypeConfiguration<AnswerStepPicture>
    {
        public AnswerStepPictureMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Picture)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AnswerStepPicture");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AnswerStepId).HasColumnName("AnswerStepId");
            this.Property(t => t.Picture).HasColumnName("Picture");

            // Relationships
            this.HasRequired(t => t.AnswerStep)
                .WithMany(t => t.AnswerStepPictures)
                .HasForeignKey(d => d.AnswerStepId);

        }
    }
}
