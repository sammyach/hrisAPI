using System;
using System.Collections.Generic;

namespace WebApi.Data
{
    public partial class AppRoles
    {
        public AppRoles()
        {
            AppUserRoles = new HashSet<AppUserRoles>();
        }

        public int RoleId { get; set; }
        public string Rolename { get; set; }
        public string Description { get; set; }

        public virtual ICollection<AppUserRoles> AppUserRoles { get; set; }
    }
}
