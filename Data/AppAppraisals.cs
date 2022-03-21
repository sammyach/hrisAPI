using System;
using System.Collections.Generic;

namespace WebApi.Data
{
    public partial class AppAppraisals
    {
        public AppAppraisals()
        {
            AppGoals = new HashSet<AppGoals>();
        }

        public int AppraisalId { get; set; }
        public string ReviewYear { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string CreatorJobTitle { get; set; }
        public int? CreatorDeptId { get; set; }
        public DateTime? DateAppraised { get; set; }
        public string AppraisedBy { get; set; }
        public string AppraisalRemarks { get; set; }
        public int? AppraisalRating { get; set; }
        public string Status { get; set; }

        public virtual AppUsers CreatedByNavigation { get; set; }
        public virtual Department CreatorDept { get; set; }
        public virtual ICollection<AppGoals> AppGoals { get; set; }
    }
}
