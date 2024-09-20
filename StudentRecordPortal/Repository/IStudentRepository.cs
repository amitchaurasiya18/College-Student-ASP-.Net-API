using StudentRecordPortal.Models;

namespace StudentRecordPortal.Repository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByRollNoAsync(string rollNo);
        Task AddAsync(Student student);
        Task DeleteAsync(int id);
    }
}
