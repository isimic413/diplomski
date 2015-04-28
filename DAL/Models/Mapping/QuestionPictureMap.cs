using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ExamPreparation.DAL.Models.Mapping
{
    public class QuestionPictureMap : EntityTypeConfiguration<QuestionPicture>
    {
        public QuestionPictureMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Picture)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("QuestionPicture");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.QuestionId).HasColumnName("QuestionId");
            this.Property(t => t.Picture).HasColumnName("Picture");

            // Relationships
            this.HasRequired(t => t.Question)
                .WithMany(t => t.QuestionPictures)
                .HasForeignKey(d => d.QuestionId);

        }
    }
}
