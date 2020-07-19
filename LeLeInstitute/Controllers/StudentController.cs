using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeLeInstitute.Models;
using LeLeInstitute.Services.IRepository;
using LeLeInstitute.ViewModels;
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



        public IActionResult Index(string sortOrder)
        {
            //if (string.IsNullOrEmpty(sortOrder))
            //{
            //    ViewData["sortName"] = "name_desc";
            //}else
            //{
            //    ViewData["sortName"] = "";
            //}
            ViewData["sortName"] = string.IsNullOrEmpty(sortOrder)? "name_desc":" ";
            ViewData["sortByDate"] = sortOrder == "Date" ? "date_desc":"Date ";
            if(sortOrder == "Date")
            {
                ViewData["sortByDate"] = "date_desc";
            }
            else
            {
                ViewData["sortByDate"] = "date_desc";
            }
            var students = _studentRepository.GetAll();
           

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.FirstName);
                    break;
                default:
                    students = students.OrderBy(s => s.FirstName);
                    break;
            }
            return View(students);
        }


        public IActionResult Details(int id = 0)
        {
           if(id == 0)
            {
                return NotFound();
            }

            var student = _studentRepository.GetById(id);

            var model = new StudentViewModel()
            {
                Student = student,
                Enrollments = _studentRepository.CoursesToStudent(student.StudentId)

            };
            return View(model);
          
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
                _studentRepository.Add(model);
                return RedirectToAction(nameof(Index));
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
