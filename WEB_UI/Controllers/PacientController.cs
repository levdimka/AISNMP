using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using WEB_UI.DAL;
using System.Net;
using System.Data;
using System.Data.Entity;
using WEB_UI.Models;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace WEB_UI.Controllers
{
    public class PacientController : Controller
    {
        private MedicalContext db = new MedicalContext();
        // GET: Pacient
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var pacients = from p in db.Pacient select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                pacients = pacients.Where(p => p.Name.Contains(searchString) || p.Sourname.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    pacients = pacients.OrderByDescending(p => p.Name);
                    ViewBag.NameSortParm = "name";
                    break;
                case "name":
                    pacients = pacients.OrderBy(p => p.Name);
                    ViewBag.NameSortParm = "name_desc";
                    break;
                case "sourname_desc":
                    pacients = pacients.OrderByDescending(p => p.Sourname);
                    ViewBag.SournameSortParm = "sourname";
                    break;
                case "sourname":
                    pacients = pacients.OrderBy(p => p.Sourname);
                    ViewBag.SournameSortParm = "sourname_desc";
                    break;
                case "patronymic_desc":
                    pacients = pacients.OrderByDescending(p => p.Patronymic);
                    ViewBag.PatronymicSortParm = "patronymic";
                    break;
                case "patronymic":
                    pacients = pacients.OrderBy(p => p.Patronymic);
                    ViewBag.SournameSortParm = "patronymic_desc";
                    break;
                case "email_desc":
                    pacients = pacients.OrderByDescending(p => p.Email);
                    ViewBag.EmailSortParm = "email";
                    break;
                case "email":
                    pacients = pacients.OrderBy(p => p.Email);
                    ViewBag.EmailSortParm = "email_desc";
                    break;
                case "date_of_birth_desc":
                    pacients = pacients.OrderByDescending(p => p.Date_of_birth);
                    ViewBag.Date_of_birthSortParm = "date_of_birth";
                    break;
                case "date_of_birth":
                    pacients = pacients.OrderBy(p => p.Date_of_birth);
                    ViewBag.Date_of_birthSortParm = "date_of_birth_desc";
                    break;
                case "adress_desc":
                    pacients = pacients.OrderByDescending(p => p.Adress);
                    ViewBag.AdressSortParm = "adress";
                    break;
                case "adress":
                    pacients = pacients.OrderBy(p => p.Adress);
                    ViewBag.AdressSortParm = "adress_desc";
                    break;
                case "telephone_desc":
                    pacients = pacients.OrderByDescending(p => p.Number_of_telephone);
                    ViewBag.TelephoneSortParm = "telephone";
                    break;
                case "telephone":
                    pacients = pacients.OrderBy(p => p.Number_of_telephone);
                    ViewBag.TelephoneSortParm = "telephone_desc";
                    break;
                case "card_number_desc":
                    pacients = pacients.OrderByDescending(p => p.Card_number);
                    ViewBag.Card_numberSortParm = "card_number";
                    break;
                case "card_number":
                    pacients = pacients.OrderBy(p => p.Card_number);
                    ViewBag.Card_numberSortParm = "card_number_desc";
                    break;
                default:  // Name ascending 
                    pacients = pacients.OrderBy(p => p.id);
                    ViewBag.NameSortParm = "name";
                    ViewBag.SournameSortParm = "sourname";
                    ViewBag.PatronymicSortParm = "patronymic";
                    ViewBag.EmailSortParm = "email";
                    ViewBag.Date_of_birthSortParm = "date_of_birth";
                    ViewBag.AdressSortParm = "adress";
                    ViewBag.TelephoneSortParm = "telephone";
                    ViewBag.Card_numberSortParm = "card_number";
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(pacients.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacient pacient = db.Pacient.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }
        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Sourname, Patronymic, Email, Date_of_birth, Adress, Number_of_telephone, Card_number, Password")] Pacient pacient)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Card new_card = new Card()
                    {
                        Card_number = pacient.Card_number,
                        Start_data = DateTime.Now,
                        Stop_data = null
                    };
                    db.Card.Add(new_card);
                    db.Pacient.Add(pacient);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e   /*RetryLimitExceededException  dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(pacient);
        }
        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacient pacient = db.Pacient.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pacientToUpdate = db.Pacient.Find(id);
            if (TryUpdateModel(pacientToUpdate, "", new string[] { "Name", "Sourname", "Patronymic", "Email", "Date_of_birth", "Adress", "Number_of_telephone"}))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(pacientToUpdate);
        }
        // GET: Student/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Pacient pacient = db.Pacient.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }
        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Pacient pacient = db.Pacient.Find(id);
                db.Pacient.Remove(pacient);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}