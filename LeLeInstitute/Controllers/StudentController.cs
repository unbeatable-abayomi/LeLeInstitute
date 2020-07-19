using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeLeInstitute.Models;
using LeLeInstitute.Services;
using LeLeInstitute.Services.IRepository;
using LeLeInstitute.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;

namespace LeLeInstitute.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICourseRepository _courseRepository;
        public StudentController(IStudentRepository studentRepository, IEnrollmentRepository enrollmentRepository, ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
           
        }



        public IActionResult Index(string sortOrder, string searchString, int pageindex = 1)
        {
            //if (string.IsNullOrEmpty(sortOrder))
            //{
            //    ViewData["sortName"] = "name_desc";
            //}else
            //{
            //    ViewData["sortName"] = "";
            //}
            ViewData["sortName"] = string.IsNullOrEmpty(sortOrder)? "name_desc":" ";
            ViewData["sortByDate"] = sortOrder == "Date" ? "date_desc":"Date";
            ViewData["currentFilter"] = searchString;
            //if(sortOrder == "Date")
            //{
            //    ViewData["sortByDate"] = "date_desc";
            //}
            //else
            //{
            //    ViewData["sortByDate"] = "Date";
            //}
            var students = _studentRepository.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.FirstName.ToLower().Contains(searchString.ToLower())||s.LastName.ToLower().Contains(searchString.ToLower()) );
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.FirstName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.FirstName);
                    break;
            }
            var model = PagingList.Create(students, pageSize:2 ,pageIndex: pageindex);
            return View(model);
        }
        public IActionResult AddCourseToStudent(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Enrollment.StudentId == 0 || model.Enrollment.CourseId == 0)
                {

                    return RedirectToAction("Index");
                }
                _enrollmentRepository.Add(model.Enrollment);
            };
            return RedirectToAction("Details", routeValues: new { id = model.Enrollment.StudentId });
        }

        public IActionResult Details(int id = 0)
        {
           if(id == 0)
            {
                return NotFound();
            }
            //new SelectList(_courseRepository.GetAll(), "DepartmentId", "DepartmentName");
            ViewBag.Courses =  _courseRepository.GetAll();
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
