using System;
using System.Collections.Generic;

namespace WebApi.Data
{
    public partial class AppGoals
    {
        public int GoalId { get; set; }
        public string Type { get; set; }
        public string Goal { get; set; }
        public string Activity { get; set; }
        public string Measures { get; set; }
        public string Comments { get; set; }
        public int? Score { get; set; }
        public int? Rating { get; set; }
        public int AppraisalId { get; set; }

        public virtual AppAppraisals Appraisal { get; set; }
    }
}
