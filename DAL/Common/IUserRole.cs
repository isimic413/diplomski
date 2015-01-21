using System;

namespace ExamPreparation.DAL.Common
{
    interface IUserRole
    {
        System.Guid Id { get; set; }
        System.Guid UserId { get; set; }
        System.Guid RoleId { get; set; }
    }
}
