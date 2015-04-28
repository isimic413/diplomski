using System;
using System.Collections.Generic;

namespace ExamPreparation.Model.Common
{
    public interface IUser
    {
        System.Guid Id { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string SaltKey { get; set; }
        string PasswordRecoveryQuestion { get; set; }
        string PasswordRecoveryAnswer { get; set; }
        ICollection<IUserAnswer> UserAnswers { get; set; }
        ICollection<IUserRole> UserRoles { get; set; }
    }
}
