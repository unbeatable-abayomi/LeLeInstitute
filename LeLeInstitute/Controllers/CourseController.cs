using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeLeInstitute.DAL;
using Microsoft.AspNetCore.Mvc;

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
            var allCourse = _context.Courses.ToList();
            return View(allCourse);
        }
    }
}
