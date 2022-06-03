using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WEB_UI.DAL;
using WEB_UI.Models;

namespace WEB_UI.Controllers
{
    public class HomeController : Controller
    {
        private MedicalContext db = new MedicalContext();

        public ActionResult Index()
        {
            bool r = User.Identity.IsAuthenticated;


            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin model)
        {
            // Уже выбрана дата и врач
            //string sql = "SELECT [Num_document] = CAST([Card_number] AS INT) ,[Password] ,[doctor] = CAST(0 AS BIT) FROM [Medical].[dbo].[Pacient]";
            //List<UserLogin> users = db.Database.SqlQuery<UserLogin>(sql).ToList();
            List<UserLogin> users_doctor = db.Doctor.Select(d => new UserLogin() { NumDocument = d.Num_document, Password = d.Password }).ToList();
            List<UserLogin> users_pacient = db.Pacient.Select(d => new UserLogin() { NumDocument = d.Card_number, Password = d.Password }).ToList();

            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                UserLogin user_doctor = null;
                UserLogin user_pacient = null;

                user_doctor = users_doctor.Where(u => u.NumDocument == model.NumDocument && u.Password == model.Password).FirstOrDefault();
                user_pacient = users_pacient.Where(u => u.NumDocument == model.NumDocument && u.Password == model.Password).FirstOrDefault();

                if (user_doctor != null || user_pacient != null)
                {
                    FormsAuthentication.SetAuthCookie(model.NumDocument.ToString(), true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким документом и паролем нет");
                }
            }

            return View(model);
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegister model)
        {
            //if (ModelState.IsValid)
            //{
            //    User user = null;
            //    using (UserContext db = new UserContext())
            //    {
            //        user = db.Users.FirstOrDefault(u => u.Email == model.Name);
            //    }
            //    if (user == null)
            //    {
            //        // создаем нового пользователя
            //        using (UserContext db = new UserContext())
            //        {
            //            db.Users.Add(new User { Email = model.Name, Password = model.Password, Age = model.Age });
            //            db.SaveChanges();

            //            user = db.Users.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
            //        }
            //        // если пользователь удачно добавлен в бд
            //        if (user != null)
            //        {
            //            FormsAuthentication.SetAuthCookie(model.Name, true);
            //            return RedirectToAction("Index", "Home");
            //        }
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "Пользователь с таким логином уже существует");
            //    }
            //}

            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}