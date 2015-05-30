using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ExamPreparation.DAL.Models.Mapping
{
    public class AnswerChoicePictureMap : EntityTypeConfiguration<AnswerChoicePicture>
    {
        public AnswerChoicePictureMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Picture)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AnswerChoicePicture");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AnswerChoiceId).HasColumnName("AnswerChoiceId");
            this.Property(t => t.Picture).HasColumnName("Picture");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
            this.HasRequired(t => t.AnswerChoice)
                .WithMany(t => t.AnswerChoicePictures)
                .HasForeignKey(d => d.AnswerChoiceId);

        }
    }
}
