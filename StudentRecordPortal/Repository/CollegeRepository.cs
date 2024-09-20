using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentRecordPortal.Data;
using StudentRecordPortal.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentRecordPortal.Repository
{
    public class CollegeRepository : ICollegeRepository
    {
        private readonly CollegeStudentDBContext _collegeStudentDBContext;

        public CollegeRepository(CollegeStudentDBContext collegeStudentDBContext)
        {
            _collegeStudentDBContext = collegeStudentDBContext;
        }

        public College Login(Login login)
        {
            if (login.CollegeEmail != null && login.Password != null)
            {
                var college = _collegeStudentDBContext.Colleges
                    .SingleOrDefault(u => u.CollegeEmail == login.CollegeEmail);

                return college;
            }

            return null;
        }

        public async Task AddAsync(College college)
        {
            await _collegeStudentDBContext.Colleges.AddAsync(college);
            await _collegeStudentDBContext.SaveChangesAsync();
        }
    }
}
