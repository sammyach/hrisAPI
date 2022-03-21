using System;
using System.Collections.Generic;

namespace WebApi.Data
{
    public partial class AppraisalGoals
    {
        public AppraisalGoals()
        {
            AppraisalGoalsObjectives = new HashSet<AppraisalGoalsObjectives>();
        }

        public int GoalId { get; set; }
        public string Type { get; set; }
        public string Goal { get; set; }
        public string Comments { get; set; }
        public int? Score { get; set; }
        public int? Rating { get; set; }

        public virtual ICollection<AppraisalGoalsObjectives> AppraisalGoalsObjectives { get; set; }
    }
}
