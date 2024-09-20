using StudentRecordPortal.Models;

namespace StudentRecordPortal.Repository
{
    public interface ICollegeRepository
    {
        College Login(Login login);
        Task AddAsync(College college);
    }
}
