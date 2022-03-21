using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MockUpController : ControllerBase
    {
        private readonly IAppRepository _repo;
        public MockUpController(IAppRepository repo)
        {
            _repo = repo;
        }
        [Authorize]
        [HttpGet("department/LOV")]
        public async Task<IActionResult> GetAllDepts()
        {
            return Ok(await _repo.GetAllDepartmentsAsync());
        }

        [Authorize]
        [HttpPost("assessments/new")]        
        public async Task<IActionResult> CreateAssessment([FromBody] NewAssessment model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            List<Assessment> assessments = new List<Assessment>();
            foreach(var item in model.goals)
            {
                var ass = new Assessment
                {
                    Activity = item.activity,
                    Type = item.type,
                    Goal = item.goal,
                    Remarks = item.comment
                };
                assessments.Add(ass);
            }
            
            Console.WriteLine(model);
            return Ok();
        }
        
    }
}