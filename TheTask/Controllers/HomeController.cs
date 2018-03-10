using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TheTask.Models;

namespace TheTask.Controllers
{
    public class HomeController : Controller
    {
        private TaskDbContext _context;

        public HomeController()
        {
            _context = new TaskDbContext();
        }

        public ActionResult Index()
        {
            /*
             * Utilizado para criação de usuário para login 
             */
            //User u = new User() { ID = 1, Password = "abcdef", Email = "test@test.com" };
            //_context.Users.Add(u);
            //_context.SaveChanges();

            return View();
        }

        // método que verifica o login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(Models.User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userFound = _context.Users.Where( x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password)).FirstOrDefault();

            if(userFound != null)
            {
                return RedirectToAction("List", "Tasks");
            }
            else
            {
                return View(model);
            }
        }
    }
}