using System;
using System.Collections.Generic;

namespace ExamPreparation.DAL.Models
{
    public partial class Role
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
