using System;
using System.Collections.Generic;

namespace ExamPreparation.DAL.Models
{
    public partial class User
    {
        public User()
        {
            this.UserAnswers = new List<UserAnswer>();
            this.UserRoles = new List<UserRole>();
        }

        public System.Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SaltKey { get; set; }
        public string PasswordRecoveryQuestion { get; set; }
        public string PasswordRecoveryAnswer { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
