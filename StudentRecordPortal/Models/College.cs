using System.ComponentModel.DataAnnotations;

namespace StudentRecordPortal.Models
{
    public class College
    {
        public int Id { get; set; }
        [Required] public string CollegeName { get; set; }
        [Required] [EmailAddress] public string CollegeEmail { get; set; }
        [Required] public string CollegePassword { get; set; }
        [Required] public string CollegePhone { get; set; }
        public string? CollegeAddress { get; set; }
        public List<Student>? Students { get; set; }
    }
}
