using System;
using System.Collections.Generic;

namespace WebApi.Data
{
    public partial class Department
    {
        public Department()
        {
            AppAppraisals = new HashSet<AppAppraisals>();
            Staff = new HashSet<Staff>();
        }

        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<AppAppraisals> AppAppraisals { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
