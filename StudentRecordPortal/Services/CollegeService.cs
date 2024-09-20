using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using StudentRecordPortal.Models;
using StudentRecordPortal.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentRecordPortal.Services
{
    public class CollegeService : ICollegeService
    {
        private readonly ICollegeRepository _collegeRepository;
        private readonly IConfiguration _configuration;

        public CollegeService(ICollegeRepository collegeRepository, IConfiguration configuration)
        {
            _collegeRepository = collegeRepository;
            _configuration = configuration;
        }

        public string Login(Login login)
        {
            var college = _collegeRepository.Login(login);

            if (login.CollegeEmail != null && login.Password != null) 
            {
                if (college != null && BCrypt.Net.BCrypt.EnhancedVerify(login.Password, college.CollegePassword))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, college.CollegeName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("Id", college.Id.ToString()),
                        new Claim("CollegeName", college.CollegeName),
                        new Claim("CollegePhone", college.CollegePhone),
                        new Claim("CollegeEmail", college.CollegeEmail)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var generateToken = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddHours(1),
                        signingCredentials: signIn
                    );
                    var Token = new JwtSecurityTokenHandler().WriteToken(generateToken);
                    return Token;
                }
            }

            return "Not a Valid User";
        }


        public async Task<College> Add(College college)
        {
            college.CollegePassword = BCrypt.Net.BCrypt.EnhancedHashPassword(college.CollegePassword);
            await _collegeRepository.AddAsync(college);
            return college;
        }
    }


}
