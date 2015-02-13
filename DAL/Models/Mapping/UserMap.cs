using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ExamPreparation.DAL.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SaltKey)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PasswordRecoveryQuestion)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.PasswordRecoveryAnswer)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.SaltKey).HasColumnName("SaltKey");
            this.Property(t => t.PasswordRecoveryQuestion).HasColumnName("PasswordRecoveryQuestion");
            this.Property(t => t.PasswordRecoveryAnswer).HasColumnName("PasswordRecoveryAnswer");
        }
    }
}
