using Microsoft.AspNetCore.Mvc;
using MK_s_University.Entities;
using MK_s_University.Models;

namespace MK_s_University
{
    public class ClassroomController : Controller
    {
        public IActionResult Clist()
        {
            var dbContext = new Mk_UniversityContext();
            var classroom = dbContext.ClassroomTables.ToList();
            return View(classroom);
        }
      
        public IActionResult AddClassroom()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveClassroom(AddClassroomModel model)
        {

            var classroom = new ClassroomTable();

            classroom.ClassName = model.ClassName;
            classroom.ClassLocation = model.ClassLocation;
            classroom.CapacityOfStudents = model.CapacityOfStudents;
            classroom.ClassId = model.ClassId;

            var dbContext = new Mk_UniversityContext();

            dbContext.ClassroomTables.Add(classroom);

            dbContext.SaveChanges();


            return RedirectToAction("Clist");
        }


        public IActionResult EditClassroom(int ClassId)
        {
            var dbContext = new Mk_UniversityContext();
            var classroomObj = dbContext.ClassroomTables.FirstOrDefault(p => p.ClassId == ClassId);

            var model = new AddClassroomModel();

            model.ClassId = classroomObj.ClassId;
            model.ClassName = classroomObj.ClassName;
            model.ClassLocation = classroomObj.ClassLocation;
            model.CapacityOfStudents = classroomObj.CapacityOfStudents;

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateClassroom(AddClassroomModel model)
        {
            var dbContext = new Mk_UniversityContext();

            var classroomObj = dbContext.ClassroomTables.Where(p => p.ClassId == model.ClassId).FirstOrDefault();

            classroomObj.ClassName = model.ClassName;
            classroomObj.ClassLocation = model.ClassLocation;
            classroomObj.CapacityOfStudents = model.CapacityOfStudents;

            dbContext.ClassroomTables.Update(classroomObj);
            dbContext.SaveChanges();

            return RedirectToAction("Clist");
        }

        [HttpPost]
        public IActionResult DeleteClassroom(int classId)
        {
            var dbContext = new Mk_UniversityContext();

            var classroomObj = dbContext.ClassroomTables.Where(p => p.ClassId == classId).FirstOrDefault();


            dbContext.ClassroomTables.Remove(classroomObj);
            dbContext.SaveChanges();

            return Json(true);
        }
    }
}
