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
    public class WindowDoctorController : Controller
    {

        private MedicalContext db = new MedicalContext();

        private int? get_idDoctor()
        {
            if (User.Identity.IsAuthenticated)
            {
                int num_doc = int.Parse(User.Identity.Name);
                Doctor doc = db.Doctor.Where(p => p.Num_document == num_doc).FirstOrDefault();
                return doc != null ? (int?)doc.id : null;
            }
            return null;
        }

        // GET: WindowDoctor
        public ActionResult Index()
        {

            int? id = get_idDoctor();
            if (id == null)
            {
                return new HttpUnauthorizedResult();
            }
            Doctor doctor = db.Doctor.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }

            return View(doctor);
        }
        // Расписание
        public ActionResult Schedule(int? id_doctor, DateTime? dt, int? type)
        {

            id_doctor = get_idDoctor();
            if (id_doctor == null)
            {
                return new HttpUnauthorizedResult();
            }
            Doctor doctor = db.Doctor.Find(id_doctor);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            if (dt == null) dt = DateTime.Now.Date;
            List<Queue> list_queue = db.Queue.Where(q => q.idDoctor == id_doctor && q.Date == dt).ToList();
            if (type == null)
            {
                list_queue = list_queue.Where(q => q.idHome == null).ToList();
            }
            else
            {
                list_queue = list_queue.Where(q => q.idVisit == null).ToList();
            }
            return View(list_queue);
        }

        public ActionResult ScheduleDetali(int? id_doctor, DateTime? dt, int? type)
        {
            id_doctor = get_idDoctor();
            if (id_doctor == null)
            {
                return new HttpUnauthorizedResult();
            }
            Doctor doctor = db.Doctor.Find(id_doctor);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            if (dt == null) dt = DateTime.Now.Date;
            List<Queue> list_queue = db.Queue.Where(q => q.idDoctor == id_doctor && q.Date == dt).ToList();
            if (type == null)
            {
                list_queue = list_queue.Where(q => q.idHome == null).ToList();
            }
            else
            {
                list_queue = list_queue.Where(q => q.idVisit == null).ToList();
            }
            ModelSchedule model = new ModelSchedule()
            {
                doctor = doctor,
                list_queue = list_queue,
                type = type
            };
            return View(model);
        }

        public ActionResult Reception(int? id_doctor, int? id_queue)
        {
            id_doctor = get_idDoctor();
            if (id_doctor == null)
            {
                return new HttpUnauthorizedResult();
            }
            Doctor doctor = db.Doctor.Find(id_doctor);
            Queue queue = null;
            Card_information card_information = null;
            if (id_queue != null)
            {
                queue = db.Queue.Find(id_queue);
                card_information = db.Card_information.Where(i => i.Card_number == queue.Pacient.Card_number && i.Closed == null).FirstOrDefault();
            }
            ModelReception model = new ModelReception()
            {
                doctor = doctor,
                queue = queue,
                card_information = card_information
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Information(int? id_queue, [Bind(Include = "Card_number, Date_of_receipt, Complaints, Diagnosis, Recipe, idDoctor")] Card_information information)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Card_information.Add(information);
                    db.SaveChanges();

                    return RedirectToAction("Reception", new { id_doctor = information.idDoctor, id_queue = id_queue });
                }
                else {
                    ModelState.AddModelError("", "Помилка");
                }
            }
            catch (Exception e   /*RetryLimitExceededException  dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction("Reception", new { id_doctor = information.idDoctor, id_queue = id_queue });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Information(int? id, int? id_queue, int? idDoctor)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var card_info_ToUpdate = db.Card_information.Find(id);
            if (TryUpdateModel(card_info_ToUpdate, "", new string[] { "Complaints", "Diagnosis", "Recipe", "Closed" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Reception", new { id_doctor = idDoctor, id_queue = id_queue }); ;
                }
                catch (RetryLimitExceededException)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return RedirectToAction("Reception", new { id_doctor = idDoctor, id_queue = id_queue }); ;
        }

        [ValidateAntiForgeryToken]
        public ActionResult Close_Queue(int? id_queue)
        {
            if (id_queue != null)
            {
                //Queue queue = db.Queue.Find(id_queue);
                var queue = db.Queue.Find(id_queue);
                if (TryUpdateModel(queue, "", new string[] { "Note" }))
                {
                    try
                    {
                        queue.Closed = DateTime.Now;
                        db.SaveChanges();
                    }
                    catch (RetryLimitExceededException)
                    {
                        //Log the error (uncomment dex variable name and add a line here to write a log.
                        ModelState.AddModelError("", "Помилка закриття черги");
                    }
                }
            }
            return RedirectToAction("ScheduleDetali");

        }

        public ActionResult Information(Card_information information)
        {
            return View(information);
        }



    }
}