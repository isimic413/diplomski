using System;
using System.Collections.Generic;

namespace ExamPreparation.Model.Common
{
    public interface IRole
    {
        System.Guid Id { get; set; }
        string Title { get; set; }
        string Abrv { get; set; }
        ICollection<IUserRole> UserRoles { get; set; }
    }
}
