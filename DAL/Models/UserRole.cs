using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamPreparation.DAL.Common;

namespace ExamPreparation.DAL.Models
{
    public partial class UserRole : IUserRole
    {
        public UserRole()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        [ForeignKey("User")]
        [Required]
        public System.Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Role")]
        [Required]
        public System.Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}