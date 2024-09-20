using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentRecordPortal.Models;
using StudentRecordPortal.Repository;
using StudentRecordPortal.Services;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentRecordPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegeController : ControllerBase
    {
        private readonly ICollegeService _collegeService;

        public CollegeController(ICollegeService collegeService)
        {
            _collegeService = collegeService;
        }

        [HttpPost("Login")]
        public string Login([FromBody] Login login)
        {
            return _collegeService.Login(login);
        }


        [HttpPost("College")]
        public Task<College> AddCollege(College college) 
        {
            return _collegeService.Add(college);
        }

        [HttpGet("Details")]
        [Authorize]
        public IActionResult GetCollegeDetails()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var collegeName = claimsIdentity.FindFirst("CollegeName")?.Value;
                var collegeId = int.Parse(claimsIdentity.FindFirst("Id")?.Value);
                var collegePhone = claimsIdentity.FindFirst("CollegePhone")?.Value;

                return Ok(new { CollegeName = collegeName, CollegeId = collegeId, CollegePhone = collegePhone });
            }
            return Unauthorized();
        }
    }
}
