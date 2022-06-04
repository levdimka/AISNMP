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
                    ModelState.AddModelError("", "Користувача с таким документом та паролем нема");
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
            if (ModelState.IsValid)
            {
                if (model.Card_number >= 1000)
                {
                    Pacient old_pacient = db.Pacient.Where(p => p.Card_number == model.Card_number).FirstOrDefault();
                    if (old_pacient == null)
                    {
                        Card new_card = new Card()
                        {
                            Card_number = model.Card_number,
                            Start_data = DateTime.Now,
                            Stop_data = null
                        };

                        Pacient pacient = new Pacient()
                        {
                            id = 0,
                            Card_number = model.Card_number,
                            Name = model.Name,
                            Sourname = model.Sourname,
                            Patronymic = model.Patronymic,
                            Adress = model.Adress,
                            Date_of_birth = model.Date_of_birth,
                            Password = model.Password,
                            Email = model.Email,
                            Number_of_telephone = model.Number_of_telephone
                        };
                        db.Card.Add(new_card);
                        db.Pacient.Add(pacient);
                        db.SaveChanges();

                        pacient = db.Pacient.Where(p => p.Card_number == model.Card_number && p.Password == model.Password).FirstOrDefault();
                        if (pacient != null)
                        {
                            FormsAuthentication.SetAuthCookie(model.Card_number.ToString(), true);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пацієнт з таким номером вже існує");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Номер картки повинен бути >999");

                }
            }
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