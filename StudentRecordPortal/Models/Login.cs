using System.ComponentModel.DataAnnotations;

namespace StudentRecordPortal.Models
{
    public class Login
    {
        [Required] public string CollegeEmail { get; set; }
        [Required] public string Password { get; set; }
    }
}
