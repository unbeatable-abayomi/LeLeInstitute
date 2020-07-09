using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeLeInstitute.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeLeInstitute.Controllers
{
    public class CourseController : Controller
    {
        private readonly LeLeContext _context;
        public CourseController(LeLeContext context)
        {
            _context = context;
        }  
        
        
        
        
        
        
        
        public IActionResult Index()
        {//method syntax
            var allCourse = _context.Courses.Include(d => d.Department).ToList();

            //query syntax

            var querySyntax = from dept in _context.Departments join course in _context.Courses on dept.DepartmentId equals course.DepartmentId select course;
            return View(querySyntax);
        }
    }
}
