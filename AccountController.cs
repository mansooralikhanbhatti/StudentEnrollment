using Microsoft.AspNetCore.Mvc;
using MK_s_University.Entities;
using MK_s_University.Models;

namespace MK_s_University
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public IActionResult SubmitLogin(LoginModel loginModel)
        {
            var dbContext = new Mk_UniversityContext();

            User userObj = dbContext.Users.FirstOrDefault(p =>
                                         p.UserName == loginModel.Username &&
                                         p.Password == loginModel.Password);

            if (userObj == null)
            {
                ModelState.AddModelError("", "Entered User & Password was incorrect..");

                return View("Login", loginModel);
            }
            else
            {
                return RedirectToAction("Dashboard", "Home");
            }

        }

        public IActionResult RegisterForm()
        {
            var model = new RegisterModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveUser(RegisterModel registerModel)
        {
            // Logic
            var dbContext = new Mk_UniversityContext();

            User newUser = new User();
            newUser.UserName = registerModel.Username;
            newUser.Password = registerModel.Password;
            newUser.Email = registerModel.Email;

            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();

            return RedirectToAction("Login");
        }


    }
}
