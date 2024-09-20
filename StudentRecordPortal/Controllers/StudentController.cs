using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentRecordPortal.Models;
using StudentRecordPortal.Services;

namespace StudentRecordPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{rollNo}")]
        public async Task<IActionResult> GetByRollNo(string rollNo)
        {
            var student = await _studentService.GetByRollNoAsync(rollNo);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            await _studentService.AddAsync(student);
            return CreatedAtAction(nameof(GetByRollNo), new { rollNo = student.StudentRollNo }, student);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
