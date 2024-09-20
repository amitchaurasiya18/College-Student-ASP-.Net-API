using StudentRecordPortal.Models;
using StudentRecordPortal.Repository;

namespace StudentRecordPortal.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Task AddAsync(Student student)
        {
            return _studentRepository.AddAsync(student);
        }

        public Task DeleteAsync(int id)
        {
            return _studentRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<Student>> GetAllAsync()
        {
            return _studentRepository.GetAllAsync();
        }

        public Task<Student> GetByRollNoAsync(string rollNo)
        {
            return _studentRepository.GetByRollNoAsync(rollNo);
        }
    }
}
