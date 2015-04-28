using System;
using System.Collections.Generic;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class User : IUser
    {
        public User()
        {
            this.UserAnswers = new List<IUserAnswer>();
            this.UserRoles = new List<IUserRole>();
        }

        public System.Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SaltKey { get; set; }
        public string PasswordRecoveryQuestion { get; set; }
        public string PasswordRecoveryAnswer { get; set; }
        public virtual ICollection<IUserAnswer> UserAnswers { get; set; }
        public virtual ICollection<IUserRole> UserRoles { get; set; }
    }
}