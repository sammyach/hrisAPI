using System;
using System.Collections.Generic;

namespace WebApi.Data
{
    public partial class Appraisals
    {
        public Appraisals()
        {
            AppraisalGoalsObjectives = new HashSet<AppraisalGoalsObjectives>();
        }

        public int AppraisalId { get; set; }
        public string ReviewYear { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateAppraised { get; set; }
        public string AppraisedBy { get; set; }
        public string AppraisalRemarks { get; set; }
        public int? AppraisalRating { get; set; }
        public string Status { get; set; }

        public virtual AppUsers AppraisedByNavigation { get; set; }
        public virtual AppUsers CreatedByNavigation { get; set; }
        public virtual ICollection<AppraisalGoalsObjectives> AppraisalGoalsObjectives { get; set; }
    }
}
