using System.ComponentModel.DataAnnotations.Schema;

namespace SiliconAPI.Models
{
    public class CourseDetailsDto
    {
        public int Id { get; set; }
          public string Name { get; set; } = null!;
          public string Description { get; set; } = null!;
         
    }
}
