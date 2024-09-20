using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace StudentRecordPortal.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string StudentRollNo { get; set; }
        public int StudentAge { get; set; }
        public string StudentBranch { get; set; }

        [Required] [EmailAddress] public string StudentEmail { get; set; }
        [Required] public DateTime DateOfAdmission { get; set; }
        [Required] public int CollegeId { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public College College { get; set; }
    }
}
