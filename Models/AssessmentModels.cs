using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Models
{
    public class PersonalDataModel
    {
        public string Fullname { get; set; }
        public string ReviewYear { get; set; }
        public DateTime? EmployedDate { get; set; }
        public DateTime AssessmentDate { get; set; }
        public string Location { get; set; }
        public int DeptId { get; set; }
        public string Position { get; set; }
        public string Supervisor { get; set; }

    }

    public class CreateNewGoalsModel
    {
        public IEnumerable<AppGoals> Goals { get; set; }
        public Boolean IsForNewAppraisal { get; set; }
        public int? AppraisalId { get; set; }
        public bool? IsFinalStage { get; set; }
    }

    public class AppraisalDetailsViewModel
    {
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
        public IEnumerable<GoalDetailsViewModel> Goals { get; set; }

        public AppraisalDetailsViewModel(AppAppraisals model, IEnumerable<GoalDetailsViewModel> g)
        {
            AppraisalId = model.AppraisalId;
            ReviewYear = model.ReviewYear;
            DateCreated = model.DateCreated;
            CreatedBy = model.CreatedBy;
            CreatorJobTitle = model.CreatorJobTitle;
            CreatorDeptId = model.CreatorDeptId;
            DateAppraised = model.DateAppraised;
            AppraisedBy = model.AppraisedBy;
            AppraisalRemarks = model.AppraisalRemarks;
            AppraisalRating = model.AppraisalRating;
            Status = model.Status;
            Goals = g;

        }
    }

    public class GoalDetailsViewModel
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

        public GoalDetailsViewModel(AppGoals g)
        {
            GoalId = g.GoalId;
            Type = g.Type;
            Goal = g.Goal;
            Activity = g.Activity;
            Measures = g.Measures;
            Comments = g.Comments;
            Score = g.Score;
            Rating = g.Rating;
            AppraisalId = g.AppraisalId;

        }
    }
}
