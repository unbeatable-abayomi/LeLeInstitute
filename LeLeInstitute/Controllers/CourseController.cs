using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeLeInstitute.DAL;
using LeLeInstitute.Models;
using LeLeInstitute.Services;
using LeLeInstitute.Services.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LeLeInstitute.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IDepartmentRepository _departmentRepository;
        public CourseController(LeLeContext context, ICourseRepository courseRepository, IDepartmentRepository departmentRepository)
        {
           

            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
        }  
        
        
        
        public IActionResult Lists()
        {
            return View(_courseRepository.GetAll());
        }
        
        
        
        public IActionResult Index()
        {
            ////method syntax
            //var allCourse = _context.Courses.Include(d => d.Department).ToList();

            ////query syntax

            //var querySyntax = from dept in _context.Departments join course in _context.Courses on dept.DepartmentId equals course.DepartmentId select course;
           
            var allCourses = _courseRepository.CoursesToDeparment();
            return View(allCourses);
        }

        [HttpGet]

        public IActionResult Create()
        {
            ViewBag.Departments = _departmentRepository.GetAll();
            return View();
        }

        [HttpPost,ActionName("Create")]

        public IActionResult CreatePost(Course model)
        {
            if(model == null)
            {
                return NotFound();
            }

            _courseRepository.Add(model);
            return View("Index");
        }
    }
}
