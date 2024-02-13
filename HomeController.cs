using Microsoft.AspNetCore.Mvc;
using MK_s_University.Entities;
using MK_s_University.Models;
using System.Runtime.InteropServices;

namespace MK_s_University
{
    public class HomeController : Controller
    {
        public IActionResult Dashboard()
        {
            var dashboardModel = new DashboardModel();

            var dbContext = new Mk_UniversityContext();

            var students = dbContext.StudentsTables.ToList();
            dashboardModel.Students = students;
            dashboardModel.TotalStudents = students.Count;

            var courses = dbContext.CoursesTables.ToList();
            dashboardModel.Courses = courses;
            dashboardModel.TotalCourses = courses.Count;


            var classrooms = dbContext.ClassroomTables.ToList();
            dashboardModel.Classrooms = classrooms;
            dashboardModel.TotalClassroom = classrooms.Count;


            return View(dashboardModel);

        }
    }
}
