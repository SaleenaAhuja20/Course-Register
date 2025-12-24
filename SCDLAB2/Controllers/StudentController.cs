using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SCDLAB.Models;
using SCDLAB.Models.Entities;

namespace SCDLAB.Controllers
{
    public class StudentController : Controller
    {
        private readonly DatabaseContext _context;
        public StudentController(DatabaseContext context) {
            _context = context; 
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Student List";
            var students = _context.Students.ToList();
            return View(students);
        }

        public  IActionResult Create()
        {
            ViewBag.PageTitle = "Create Student";
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student s)
        {
            _context.Students.Add(s);
            _context.SaveChanges();

            TempData["Success"] = "Student Created Successfully";
            return RedirectToAction("Index");

        }

        public IActionResult Details(int id)
        {
            var student = _context.Students
                .Include(e => e.Enrollments)
                .ThenInclude(c => c.Course)
                .FirstOrDefault(x => x.Id == id);

            return View(student);
        }


    }
}
