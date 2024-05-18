using System.ComponentModel.DataAnnotations.Schema;

namespace SiliconAPI.Models
{
    public class CourseLearnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
