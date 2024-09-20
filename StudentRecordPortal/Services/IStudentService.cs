using StudentRecordPortal.Models;

namespace StudentRecordPortal.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByRollNoAsync(string rollNo);
        Task AddAsync(Student student);
        Task DeleteAsync(int id);
    }
}
