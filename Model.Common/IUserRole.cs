using System;

namespace ExamPreparation.Model.Common
{
    public interface IUserRole
    {
        System.Guid Id { get; set; }
        System.Guid UserId { get; set; }
        System.Guid RoleId { get; set; }
        IRole Role { get; set; }
        IUser User { get; set; }
    }
}
