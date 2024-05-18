using System.ComponentModel.DataAnnotations.Schema;

namespace SiliconAPI.Models
{
  // Specify the table name for the Course entity
    public class Teacher
    {
        public int Id { get; set; }

        public string TeacherName { get; set; } = null!;

        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public ICollection<TeacherCourseDto>? TeacherCourses { get; set; }
    }

    public class Teacher2
    {
        public int Id { get; set; }

        public string TeacherName { get; set; } = null!;

        public string? ImagePath { get; set; }

 
    }
}
