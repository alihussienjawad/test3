using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiliconAPI.Data;
using SiliconAPI.Models;
using SiliconAPI.Models.Dtos;
using SiliconAPI.Models.VM;

namespace SiliconAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private List<int> bestSellers { get; set; }
        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
            bestSellers = new();
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<List<Course>>> GetCourses()
        {
            int CountBest = 0;
            int UsersCount = _context.ApplicationUsers.Count();

            List<Course> Courses = await _context.Courses//.ToListAsync();
            .Include(c => c.Category)
            .Include(c => c.CourseLearns)
            .Include(c => c.CourseDetails).ToListAsync();

            bestSellers = [];

            bestSellers.Add(Courses.Max(i => i.iDowinload));

            if (_context.Courses.Where(i => i.iDowinload > 0).Count() > 6)
                CountBest =6;
            else
                CountBest = _context.Courses.Where(i => i.iDowinload > 0).Count();

            while (bestSellers.Count < CountBest)
            {
                bestSellers.Add((from c in Courses
                                 where !bestSellers.Any(i => i == c.iDowinload)
                                 select c).Max(i => i.iDowinload));
            }
            foreach (Course item in Courses)
            {
              
                item.NumberOfLikesResult = (item.NumberOfLikes * 100 / UsersCount);
                item.IsBestSeller = bestSellers.Any(i => i == item.iDowinload);
                item.Teachers = GetTeachers(item.CourseId);
               
             }
            return Courses;
      }
         private List<Teacher2>? GetTeachers(int id)
            {
                var x= _context.TeacherCourses.Where(i => i.CourseId == id).ToList();
                List<Teacher2>? result = [];
                List<Teacher2> teacher2 = new();
                foreach (var course in x)
                {
                    teacher2=(from t in _context.Teachers
                         where t.Id== course.TeacherId
                         select new Teacher2
                         {
                             Id=t.Id,
                             TeacherName=t.TeacherName,
                             ImagePath=t.ImagePath
                         }).ToList();
                 
                    result.AddRange(teacher2);
                }
                return result;
            }
            // GET: api/Courses/5
            [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            List<Course> Courses = await _context.Courses.ToListAsync();
            if (course == null)
            {
                return NotFound();
            }

            int CountBest = 0;
            int UsersCount = _context.ApplicationUsers.Count();

            Course? Course = await _context.Courses.Where(i => i.CourseId == id)
                .Include(c => c.Category)
                .Include(c => c.CourseLearns)
                .Include(c => c.CourseDetails)
                .FirstOrDefaultAsync();
         

            bestSellers = [];

            bestSellers.Add(Courses.Max(i => i.iDowinload));

            if (_context.Courses.Where(i => i.iDowinload > 0).Count() > 6)
                CountBest = 6;
            else
                CountBest = _context.Courses.Where(i => i.iDowinload > 0).Count();

            while (bestSellers.Count < CountBest)
            {
                bestSellers.Add((from c in Courses
                                 where !bestSellers.Any(i => i == c.iDowinload)
                                 select c).Max(i => i.iDowinload));
            }
            if (Course != null)
            {
              
                Course.NumberOfLikesResult = (Course.NumberOfLikes * 100 / UsersCount);
                Course.IsBestSeller = bestSellers.Any(i => i == Course.iDowinload);
                Course.Teachers = GetTeachers(Course.CourseId);
                
            }

             
            return Courses.Where(i=>i.CourseId==id).First();
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> Post([FromForm]CourseDto course)
        {
           //  _context.Courses.Add(course);
          if(  await _context.SaveChangesAsync() > 0)
            {
                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
