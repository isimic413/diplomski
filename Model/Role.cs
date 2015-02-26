using System;
using System.Collections.Generic;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Model
{
    public partial class Role : IRole
    {
        public Role()
        {
            this.UserRoles = new List<UserRole>();
        }

        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string Abrv { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}