using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IStaffService _staffService;

        public UsersController(IUserService userService, IStaffService staffService)
        {
            _userService = userService;
            _staffService = staffService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _userService.AuthenticateAsync(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(new { token = response });
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("personalData")]
        public async Task<IActionResult> GetPersonalData()
        {
            var user = (AppUsers)HttpContext.Items["User"];
            var staff = await _staffService.FindStaffByUsernameAsync(user.Username);
            var data = new PersonalDataModel
            {
                DeptId = staff?.DeptId ?? 0,
                EmployedDate = staff?.DateJoined,
                Fullname = $"{staff?.FirstName} {staff?.LastName} {staff?.OtherNames}",
                Position = staff?.Position,
                Supervisor = staff?.Supervisor,
                AssessmentDate = DateTime.Now,
                ReviewYear = "2020"
            };

            return Ok(data);
        }
    }
}
