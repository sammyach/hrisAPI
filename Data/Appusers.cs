using System;
using System.Collections.Generic;

namespace WebApi.Data
{
    public partial class AppUsers
    {
        public AppUsers()
        {
            AppAppraisals = new HashSet<AppAppraisals>();
            AppUserRoles = new HashSet<AppUserRoles>();
            AppraisalsAppraisedByNavigation = new HashSet<Appraisals>();
            AppraisalsCreatedByNavigation = new HashSet<Appraisals>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? LastLoggedIn { get; set; }
        public string Host { get; set; }
        public byte? Enabled { get; set; }

        public virtual ICollection<AppAppraisals> AppAppraisals { get; set; }
        public virtual ICollection<AppUserRoles> AppUserRoles { get; set; }
        public virtual ICollection<Appraisals> AppraisalsAppraisedByNavigation { get; set; }
        public virtual ICollection<Appraisals> AppraisalsCreatedByNavigation { get; set; }
    }
}
