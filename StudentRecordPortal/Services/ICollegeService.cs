using StudentRecordPortal.Models;

namespace StudentRecordPortal.Services
{
    public interface ICollegeService
    {
        string Login(Login login);
        Task<College> Add(College college);
    }
}
