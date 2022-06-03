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
    public class DoctorController : Controller
    {
        private MedicalContext db = new MedicalContext();
        //public class Specifical
        //{
        //    public int id { get; set; }
        //    public string Title { get; set; }
        //}
        
        private List<Specialization> GetSpecificals()
        {
            //var specificals = new List<Specifical>();
            //specificals.Add(new Specifical() { id = 1, Title = "Лор" });
            //specificals.Add(new Specifical() { id = 2, Title = "Окуліст" });
            //specificals.Add(new Specifical() { id = 3, Title = "Хірург" });
            //specificals.Add(new Specifical() { id = 4, Title = "Терапевт" });
            List<Specialization>  res = db.Specialization.ToList();
            return res;
        }
        // GET: Doctor
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
            List<Doctor> doctors = db.Doctor.ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(d => d.Name.Contains(searchString) || d.Sourname.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    doctors = doctors.OrderByDescending(d => d.Name).ToList();
                    ViewBag.NameSortParm = "name";
                    break;
                case "name":
                    doctors = doctors.OrderBy(d => d.Name).ToList();
                    ViewBag.NameSortParm = "name_desc";
                    break;
                case "sourname_desc":
                    doctors = doctors.OrderByDescending(d => d.Sourname).ToList();
                    ViewBag.SournameSortParm = "sourname";
                    break;
                case "sourname":
                    doctors = doctors.OrderBy(d => d.Sourname).ToList();
                    ViewBag.SournameSortParm = "sourname_desc";
                    break;
                case "patronymic_desc":
                    doctors = doctors.OrderByDescending(d => d.Patronymic).ToList();
                    ViewBag.PatronymicSortParm = "patronymic";
                    break;
                case "patronymic":
                    doctors = doctors.OrderBy(d => d.Patronymic).ToList();
                    ViewBag.PatronymicSortParm = "patronymic_desc";
                    break;
                case "email_desc":
                    doctors = doctors.OrderByDescending(d => d.Email).ToList();
                    ViewBag.EmailSortParm = "email";
                    break;
                case "email":
                    doctors = doctors.OrderBy(d => d.Email).ToList();
                    ViewBag.EmailSortParm = "email_desc";
                    break;
                case "date_of_birth_desc":
                    doctors = doctors.OrderByDescending(d => d.Date_of_birth).ToList();
                    ViewBag.Date_of_birthSortParm = "date_of_birth";
                    break;
                case "date_of_birth":
                    doctors = doctors.OrderBy(d => d.Date_of_birth).ToList();
                    ViewBag.Date_of_birthSortParm = "date_of_birth_desc";
                    break;
                case "telephone_desc":
                    doctors = doctors.OrderByDescending(d => d.Number_of_telephone).ToList();
                    ViewBag.TelephoneSortParm = "telephone";
                    break;
                case "telephone":
                    doctors = doctors.OrderBy(d => d.Number_of_telephone).ToList();
                    ViewBag.TelephoneSortParm = "telephone_desc";
                    break;
                case "specialization_desc":
                    doctors = doctors.OrderByDescending(d => d.IdSpecialization).ToList();
                    ViewBag.IdSpecializationSortParm = "specialization";
                    break;
                case "specialization":
                    doctors = doctors.OrderBy(d => d.IdSpecialization).ToList();
                    ViewBag.IdSpecializationSortParm = "specialization_desc";
                    break;

                default:  // Name ascending 
                    doctors = doctors.OrderBy(d => d.id).ToList();
                    ViewBag.NameSortParm = "name";
                    ViewBag.SournameSortParm = "sourname";
                    ViewBag.PatronymicSortParm = "patronymic";
                    ViewBag.EmailSortParm = "email";
                    ViewBag.Date_of_birthSortParm = "date_of_birth";
                    ViewBag.TelephoneSortParm = "telephone";
                    ViewBag.IdSpecializationSortParm = "specialization";
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(doctors.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctor.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }
        // GET: Student/Create
        public ActionResult Create()
        {
            ViewBag.SelectList = new SelectList(GetSpecificals(), "id", "Post_Specialization");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Sourname, Patronymic, Email, Date_of_birth, Number_of_telephone, IdSpecialization, Password")] Doctor doctor)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //Card new_card = new Card()
                    //{
                    //    Card_number = pacient.Card_number,
                    //    Start_data = DateTime.Now,
                    //    Stop_data = null
                    //};
                    //db.Card.Add(new_card);
                    db.Doctor.Add(doctor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e   /*RetryLimitExceededException  dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(doctor);
        }
        
        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.SelectList = new SelectList(GetSpecificals(), "id", "Post_Specialization");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctor.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doctorToUpdate = db.Doctor.Find(id);
            if (TryUpdateModel(doctorToUpdate, "", new string[] { "Name", "Sourname", "Patronymic", "Email", "Date_of_birth", "Number_of_telephone", "IdSpecialization" }))
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
            return View(doctorToUpdate);
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
            Doctor doctor = db.Doctor.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }
        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Doctor doctor = db.Doctor.Find(id);
                db.Doctor.Remove(doctor);
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