using System;
using System.Collections.Generic;

namespace WebApi.Data
{
    public partial class AppUserRoles
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public int Roleid { get; set; }

        public virtual AppRoles Role { get; set; }
        public virtual AppUsers User { get; set; }
    }
}
