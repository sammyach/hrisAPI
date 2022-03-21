using System;
using System.Collections.Generic;

namespace WebApi.Data
{
    public partial class AppraisalGoalsObjectives
    {
        public int Id { get; set; }
        public int AppraisalId { get; set; }
        public int GoalId { get; set; }
        public int ObjectiveId { get; set; }

        public virtual Appraisals Appraisal { get; set; }
        public virtual AppraisalGoals Goal { get; set; }
        public virtual AppraisalObjectives Objective { get; set; }
    }
}
