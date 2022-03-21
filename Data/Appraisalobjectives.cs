using System;
using System.Collections.Generic;

namespace WebApi.Data
{
    public partial class AppraisalObjectives
    {
        public AppraisalObjectives()
        {
            AppraisalGoalsObjectives = new HashSet<AppraisalGoalsObjectives>();
        }

        public int ObjectiveId { get; set; }
        public string Objective { get; set; }
        public string Comments { get; set; }
        public int? Score { get; set; }
        public int? Rating { get; set; }

        public virtual ICollection<AppraisalGoalsObjectives> AppraisalGoalsObjectives { get; set; }
    }
}
