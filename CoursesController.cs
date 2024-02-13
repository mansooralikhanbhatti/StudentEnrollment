using Microsoft.AspNetCore.Mvc;
using MK_s_University.Entities;
using MK_s_University.Models;

namespace MK_s_University
{
    public class CoursesController : Controller
    {
        public IActionResult CSlist()
        {
            var dbContext = new Mk_UniversityContext();
            var courses = dbContext.CoursesTables.ToList();
            return View(courses);
        }

        public IActionResult AddCourses()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveCourses(AddCoursesModel model)
        {

            var courses = new CoursesTable();

            courses.CourseId = model.CourseId;
            courses.CourseName = model.CourseName;
            courses.LecturerName = model.LecturerName;
            courses.Duration = model.Duration;

            var dbContext = new Mk_UniversityContext();

            dbContext.CoursesTables.Add(courses); 

            dbContext.SaveChanges();         

            return RedirectToAction("CSlist");
        }


        public IActionResult EditCourses(int courseId)
        {
            var dbContext = new Mk_UniversityContext();
            var coursesObj = dbContext.CoursesTables.FirstOrDefault(p => p.CourseId == courseId);

            var model = new AddCoursesModel();

            model.CourseId = coursesObj.CourseId;
            model.CourseName = coursesObj.CourseName;
            model.LecturerName = coursesObj.LecturerName;
            model.Duration = coursesObj.Duration;

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateCourses(AddCoursesModel model)
        {
            var dbContext = new Mk_UniversityContext();

            var coursesObj = dbContext.CoursesTables.Where(p => p.CourseId == model.CourseId).FirstOrDefault();

            coursesObj.CourseName = model.CourseName;
            coursesObj.LecturerName = model.LecturerName;
            coursesObj.Duration = model.Duration;

            dbContext.CoursesTables.Update(coursesObj);
            dbContext.SaveChanges();

            return RedirectToAction("CSlist");
        }

        [HttpPost]
        public IActionResult DeleteCourses(int courseId)
        {
            var dbContext = new Mk_UniversityContext();

            var coursesObj = dbContext.CoursesTables.Where(p => p.CourseId == courseId).FirstOrDefault();


            dbContext.CoursesTables.Remove(coursesObj);
            dbContext.SaveChanges();

            return Json(true);
        }
    }
}