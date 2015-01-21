using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamPreparation.DAL.Common;

namespace ExamPreparation.DAL.Models
{
    public partial class User : IUser
    {
        public User()
        {
        }

        [Key]
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "*")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        public string SaltKey { get; set; }

        [Required(ErrorMessage = "*")]
        public string PasswordRecoveryQuestion { get; set; }

        [Required(ErrorMessage = "*")]
        public string PasswordRecoveryAnswer { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual ICollection<UserAnswer> Answers { get; set; }
    }
}