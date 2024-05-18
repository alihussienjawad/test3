using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiliconAPI.Models.Dtos
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public int CategoryId { get; set; }
        public string CourseName { get; set; } = null!;

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal PriceAfterDiscount { get; set; }
        public int NumberOfHours { get; set; }
        public int Rate { get; set; }
        public int NumberOfLikes { get; set; }

        [Display(Name = "downloadable resources")]
        public int DowinloadResource { get; set; }



        public string? ImagePath { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public string CoursDec { get; set; } = null!;

        public int Aritc { get; set; }
        public int iDowinload { get; set; }

        [Display(Name = "Full Time")]
        public bool Time { get; set; } = true;
        public bool Certifacate { get; set; }

        public IEnumerable<CourseDetailsDto> CourseDetails { get; set; }  
        public IEnumerable<CourseLearnDto> CourseLearns { get; set; }  
        public IEnumerable<TeacherCourseDto> TeacherCourses { get; set; }  
    }
}
