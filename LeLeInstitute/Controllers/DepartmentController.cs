using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeLeInstitute.Models;
using LeLeInstitute.Services.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace LeLeInstitute.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IInstructorRepository _instructorRepository;

        public DepartmentController( IDepartmentRepository departmentRepository, IInstructorRepository instructorRepository)
        {
            _departmentRepository = departmentRepository;
            _instructorRepository = instructorRepository;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepository.InStructorToDeparments();
            return View(departments);
        }

        public IActionResult Details(int detailId)
        {
            //var department =  _departmentRepository.GetById(detailId);
            var department = _departmentRepository.InStructorToDeparments().FirstOrDefault(x => x.DepartmentId == detailId);
            //var department = _departmentRepository.InstructorToDeparment(detailId);

            ;
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {

            InstructorList();
            return View();
        }

        public void InstructorList()
        {
            ViewBag.Instructors = _instructorRepository.GetAll();
        }


        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Department model)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.Add(model);
            }

            return RedirectToAction("Details", new { detailId = model.DepartmentId });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var department = _departmentRepository.GetById(id);
            if(department == null)
            {
                return NotFound();
            }
            InstructorList();
            return View(department);
        }


        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(Department model)
        {

            if (ModelState.IsValid)
            {
                _departmentRepository.Update(model);
                return RedirectToAction("Details", new { detailId = model.DepartmentId });
            }
          
            return View("Edit");
        }


        public IActionResult Delete (int id)
        {
            var department = _departmentRepository.InStructorToDeparments().FirstOrDefault(x=>x.DepartmentId == id);
            return View(department);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int departmentId)
        {
            var deparment = _departmentRepository.GetById(departmentId);
            if(deparment == null)
            {
                return NotFound();
            }
            _departmentRepository.Delete(deparment);
            return RedirectToAction("Index");
        }
    }
}
