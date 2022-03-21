using System;
using System.Collections.Generic;

namespace WebApi.Data
{
    public partial class Staff
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherNames { get; set; }
        public string Gender { get; set; }
        public DateTime? DateJoined { get; set; }
        public string Position { get; set; }
        public string PositionLevel { get; set; }
        public int DeptId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string Supervisor { get; set; }
        public byte? Enabled { get; set; }

        public virtual Department Dept { get; set; }
    }
}
