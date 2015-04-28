using System;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class UserRole : IUserRole
    {
        public System.Guid Id { get; set; }
        public System.Guid UserId { get; set; }
        public System.Guid RoleId { get; set; }
        public virtual IRole Role { get; set; }
        public virtual IUser User { get; set; }
    }
}