using Microsoft.AspNetCore.Mvc;
using SCDLAB.Models;
using SCDLAB.Models.Entities;

namespace SCDLAB.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly DatabaseContext _context;
        public EnrollmentController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Enroll(int studentId)        
        {
            ViewBag.StudentId = studentId;
            ViewBag.Courses = _context.Courses.ToList();
            ViewData["Title"] = "Enroll Student";
            return View();
        }

        [HttpPost]
        public IActionResult Enroll(int studentId,int course)
        {
            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = course,
                EnrolledOn = DateTime.Now
            };
            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();
            TempData["Message"] = "Enrollment successful!";
            return RedirectToAction("Details", "Student", new
            {
                id = studentId,
            });
        }
    }
}
