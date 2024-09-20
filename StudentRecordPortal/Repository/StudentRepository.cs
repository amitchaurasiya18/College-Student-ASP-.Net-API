using Microsoft.EntityFrameworkCore;
using StudentRecordPortal.Data;
using StudentRecordPortal.Models;
using StudentRecordPortal.Services;

namespace StudentRecordPortal.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CollegeStudentDBContext _context;
        private readonly IUserService _userService;

        public StudentRepository(CollegeStudentDBContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var collegeId = _userService.GetCurrentCollegeId();
            return await _context.Students
                .Where(s => s.CollegeId == collegeId)
                .ToListAsync();
        }

        public async Task<Student> GetByRollNoAsync(string rollNo)
        {
            var collegeId = _userService.GetCurrentCollegeId();
            return await _context.Students.SingleOrDefaultAsync(s => s.StudentRollNo == rollNo && s.CollegeId == collegeId);
        }

        public async Task AddAsync(Student student)
        {
            //student.DateOfAdmission
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
    }
}
