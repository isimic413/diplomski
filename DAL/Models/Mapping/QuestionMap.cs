using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ExamPreparation.DAL.Models.Mapping
{
    public class QuestionMap : EntityTypeConfiguration<Question>
    {
        public QuestionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Question");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TestingAreaId).HasColumnName("TestingAreaId");
            this.Property(t => t.QuestionTypeId).HasColumnName("QuestionTypeId");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.Points).HasColumnName("Points");
            this.Property(t => t.HasPicture).HasColumnName("HasPicture");
            this.Property(t => t.HasSteps).HasColumnName("HasSteps");

            // Relationships
            this.HasRequired(t => t.QuestionType)
                .WithMany(t => t.Questions)
                .HasForeignKey(d => d.QuestionTypeId);
            this.HasRequired(t => t.TestingArea)
                .WithMany(t => t.Questions)
                .HasForeignKey(d => d.TestingAreaId);

        }
    }
}
