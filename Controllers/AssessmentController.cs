using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService _assessmentService;
        private readonly IStaffService _staff;
        public AssessmentController(IAssessmentService service, IStaffService staff)
        {
            _assessmentService = service;
            _staff = staff;
        }

        [Authorize]
        [HttpGet("user/allappraisals")]
        public async Task<IEnumerable<AppAppraisals>> GetAssessmentsByUser()
        {
            var user = (AppUsers)HttpContext.Items["User"];
            return await _assessmentService.GetAppraisalsByUsername(user.Username);
        }

        [Authorize]
        [HttpPost("new")]
        public async Task<IActionResult> CreateAssessment([FromBody] AppAppraisals model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var pathBase = HttpContext.Request.PathBase;
            var path = HttpContext.Request.Path;

            var user = (AppUsers) HttpContext.Items["User"];
            var host = HttpContext.Request.Host;

            model.CreatedBy = user.Username;
            model.DateCreated = DateTime.Now;

            var res = await _assessmentService.CreateNewAssessmentAsync(model);


            return Ok(res);

        }

        [Authorize]
        [HttpPost("new/goals")]
        public async Task<IActionResult> CreateGoals([FromBody] CreateNewGoalsModel model)
        {
            var appraisalDocID = model.AppraisalId;
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var pathBase = HttpContext.Request.PathBase;
            var path = HttpContext.Request.Path;

            var user =(AppUsers) HttpContext.Items["User"];
            var host = HttpContext.Request.Host;

            if (model.IsForNewAppraisal)
            {
                var personalData = await _staff.FindStaffByUsernameAsync(user.Username);
                // we first create appraisal n use id to create goals
                var appraisal = new AppAppraisals
                {
                    CreatedBy = user.Username,
                    CreatorDeptId = personalData.DeptId,
                    DateCreated = DateTime.Now,
                    ReviewYear = "2020",
                    AppraisedBy = personalData.Supervisor,
                    CreatorJobTitle = personalData.Position,
                    Status = "NEW"
                };
                //if (model.IsForNewAppraisal)
                //    appraisal.Status = "NEW";                

                var resAppr = await _assessmentService.CreateNewAssessmentAsync(appraisal);
                appraisalDocID = resAppr.AppraisalId;
            }

            var goals = model.Goals;
            foreach (var goal in goals)
            {
                goal.AppraisalId = appraisalDocID ?? 0;
            }
            var res = _assessmentService.CreateNewGoalsAsync(goals);

            if ((bool)model.IsFinalStage)
            {
                // update appraisal status to OPEN
                await _assessmentService.UpdateAppraisalStatusAsync(appraisalDocID ?? 0, "OPEN");
            }

            return Ok(appraisalDocID);
        }

        [Authorize]
        [HttpGet("details/{id}")]        
        public async Task<IActionResult> GetAppraisalDetails(int id)
        {
            if (id <= 0) return BadRequest("Appraisal ID is required");

            var res = await _assessmentService.GetAppraisalByIdAysnc(id);
            List<GoalDetailsViewModel> goals = new List<GoalDetailsViewModel>();
            foreach(var goal in res.AppGoals)
            {
                goals.Add(new GoalDetailsViewModel(goal));
            }
            var apps = new AppraisalDetailsViewModel(res, goals);
            return Ok(apps);
        }
    }
}