using Microsoft.AspNetCore.Mvc;
using MK_s_University.Entities;
using MK_s_University.Models;

namespace MK_s_University
{
    public class StudentController : Controller
    {
        public IActionResult Slist()
        {
            var dbContext = new Mk_UniversityContext();
            var students = dbContext.StudentsTables.ToList();
            return View(students);
        }

        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveStudent(AddStudentModel model)
        {

            var student = new StudentsTable();

            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.Email = model.Email;
            student.MobileNumber = model.MobileNumber;

            var dbContext = new Mk_UniversityContext();

            dbContext.StudentsTables.Add(student);

            dbContext.SaveChanges();


            return RedirectToAction("Slist");
        }


        public IActionResult EditStudent(int studentId)
        {
            var dbContext = new Mk_UniversityContext();
            var studentObj = dbContext.StudentsTables.FirstOrDefault(p => p.StudentId == studentId);

            var model = new AddStudentModel();

            model.StudentId = studentObj.StudentId;
            model.FirstName = studentObj.FirstName;
            model.LastName = studentObj.LastName;
            model.Email = studentObj.Email;
            model.MobileNumber = studentObj.MobileNumber;

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateStudent(AddStudentModel model)
        {
            var dbContext = new Mk_UniversityContext();

            var studentObj = dbContext.StudentsTables.Where(p => p.StudentId == model.StudentId).FirstOrDefault();

            studentObj.FirstName = model.FirstName;
            studentObj.LastName = model.LastName;
            studentObj.Email = model.Email;
            studentObj.MobileNumber = model.MobileNumber;

            dbContext.StudentsTables.Update(studentObj);
            dbContext.SaveChanges();

            return RedirectToAction("Slist");
        }

        [HttpPost]
        public IActionResult DeleteStudent(int studentId)
        {
            var dbContext = new Mk_UniversityContext();

            var studentObj = dbContext.StudentsTables.Where(p => p.StudentId == studentId).FirstOrDefault();


            dbContext.StudentsTables.Remove(studentObj);
            dbContext.SaveChanges();

            return Json(true);
        }
    }
}
