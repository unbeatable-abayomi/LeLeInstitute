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
            ViewBag.DepartmentId =new SelectList( _departmentRepository.GetAll(),"DepartmentId","DepartmentName");
            return View();
        }

        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Course model)
        {
            if (ModelState.IsValid)
            {
                _courseRepository.Add(model);
                RedirectToAction("Index");
            }
            //if(model == null)
            //{
            //    return NotFound();
            //}

           
            return View("Create");
        } 
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var course = _courseRepository.GetById(Id);
            ViewBag.DepartmentId = new SelectList(_departmentRepository.GetAll(), "DepartmentId", "DepartmentName");
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course model)
        {
            if (ModelState.IsValid)
            {
                _courseRepository.Update(model);
                return RedirectToAction("Index");
            }
            

            return View("Edit");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var course = _courseRepository.GetById(Id);
            if(course == null )
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost , ActionName("Delete")]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeletePost (int courseId)
        {
            var course = _courseRepository.GetById(courseId);
            if (course == null && courseId == 0)
            {
                return NotFound();
            }
         
                _courseRepository.Delete(course);
               return RedirectToAction("Index");
                       
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            var course = _courseRepository.CoursesToDeparment().FirstOrDefault(x => x.CourseId == Id);
                if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
    }
}
