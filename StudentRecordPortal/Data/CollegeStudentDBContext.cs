using Microsoft.EntityFrameworkCore;
using StudentRecordPortal.Models;

namespace StudentRecordPortal.Data
{
    public class CollegeStudentDBContext : DbContext
    {
        public CollegeStudentDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<College> Colleges { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
