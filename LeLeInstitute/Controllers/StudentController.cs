using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeLeInstitute.Models;
using LeLeInstitute.Services.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace LeLeInstitute.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {


            _studentRepository = studentRepository;
           
        }



        public IActionResult Lists()
        {
            return View(_studentRepository.GetAll());
        }


        public IActionResult Details(int Id)
        {
            return View(_studentRepository.GetAll());
        }

        public IActionResult Index()
        {
            ////method syntax
            //var allCourse = _context.Courses.Include(d => d.Department).ToList();

            ////query syntax

            //var querySyntax = from dept in _context.Departments join course in _context.Courses on dept.DepartmentId equals course.DepartmentId select course;

            //var allStudents = _studentRepository.CoursesToDeparment();
            return View();
        }

        [HttpGet]

        public IActionResult Create()
        {
            //ViewBag.DepartmentId = new SelectList(_departmentRepository.GetAll(), "DepartmentId", "DepartmentName");
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Student model)
        {
            if (ModelState.IsValid)
            {
                //_studentRepository.Add(model);
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
            var course = _studentRepository.GetById(Id);
            //ViewBag.DepartmentId = new SelectList(_departmentRepository.GetAll(), "DepartmentId", "DepartmentName");
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student model)
        {
            if (ModelState.IsValid)
            {
                //_studentRepository.Update();
                return RedirectToAction("Index");
            }


            return View("Edit");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var course = _studentRepository.GetById(Id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeletePost(int courseId)
        {
            var course = _studentRepository.GetById(courseId);
            if (course == null && courseId == 0)
            {
                return NotFound();
            }

            _studentRepository.Delete(course);
            return RedirectToAction("Index");

        }

        //[HttpGet]
        //public IActionResult Details(int Id)
        //{
        //    var course = _studentRepository.CoursesToDeparment().FirstOrDefault(x => x.CourseId == Id);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(course);
        //}
    }
}
