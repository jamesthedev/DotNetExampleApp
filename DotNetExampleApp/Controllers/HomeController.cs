using DotNetExampleApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetExampleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DbContext dbContext = HttpContext.RequestServices.GetService(typeof(DotNetExampleApp.Models.DbContext)) as DbContext;
            List<Student> students = dbContext.GetStudents();
            ViewBag.studentList = students;

            return View();
        }

        [HttpPost("AddEditStudent")]
        //I tried so hard to just pass Student object from the front-end. It kept coming back as null and I ran out of time...😢
        public ActionResult AddEditStudent(int Id, string FName, string LName, double CurrGrade, int Age)
        {
            DbContext dbContext = HttpContext.RequestServices.GetService(typeof(DotNetExampleApp.Models.DbContext)) as DbContext;

            Student student = new Student()
            {
                Id = Id,
                FName = FName,
                LName = LName,
                CurrGrade = CurrGrade,
                Age = Age
            };

            if (Id == -1)
            {
                dbContext.AddStudent(student);
            }

            else
            {
                dbContext.EditStudent(student);
            }

            return new OkObjectResult(200);
        }

        [HttpGet("DeleteStudent")]
        public ActionResult DeleteStudent(int studentId)
        {
            DbContext dbContext = HttpContext.RequestServices.GetService(typeof(DotNetExampleApp.Models.DbContext)) as DbContext;
            dbContext.DeleteStudent(studentId);

            return new OkObjectResult(200);
        }
    }
} 
