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


    public class WindowPacientController : Controller
    {
        private MedicalContext db = new MedicalContext();

        private int? get_idPacient() { 
            if (User.Identity.IsAuthenticated) {
                int num_doc = int.Parse(User.Identity.Name);
                Pacient pac = db.Pacient.Where(p => p.Card_number == num_doc).FirstOrDefault();
                return pac != null ? (int?)pac.id : null;
            }
            return null;
        }
       
        public ActionResult Index()
        {
            int? id = get_idPacient();
            if (id == null)
            {
                return new HttpUnauthorizedResult();
            }
            Pacient pacient = db.Pacient.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }
        // Обработка кнопки - запись на прием к терапевту
        public ActionResult BWTherapist(int? id)
        {
            id = get_idPacient();
            if (id == null)
            {
                return new HttpUnauthorizedResult();
            }
            Pacient pacient = db.Pacient.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }
        //Обработка кнопки - запись терапевта на дом
        public ActionResult BHTherapist(int? id)
        {
            id = get_idPacient();
            if (id == null)
            {
                return new HttpUnauthorizedResult();
            }
            Pacient pacient = db.Pacient.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }
        // Медкомисия
        public ActionResult MedicalBoard(int? id_pacient, int? id_link, int? id_queue, DateTime? curr_date, int? curr_doctor, int? curr_time)
        {
            id_pacient = get_idPacient();
            if (id_link == null)
            {
                id_link = 1;
            }
            if (id_pacient == null)
            {
                return new HttpUnauthorizedResult();
            }
            Pacient pacient = db.Pacient.Find(id_pacient);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            // Удалим запись очереди
            if (id_queue != null)
            {
                Queue queue = db.Queue.Find(id_queue);
                if (queue == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    try
                    {
                        db.Queue.Remove(queue);
                        db.SaveChanges();
                        id_queue = null;
                    }
                    catch (RetryLimitExceededException)
                    {
                        ViewBag.ErrorMessage = "Delete failed. Ups!!";
                    }
                }
            }
            // Создадим запись очереди
            if (curr_date != null && curr_doctor != null && curr_time != null)
            {
                DateTime date_curr = DateTime.Now.Date;
                Queue lor = db.Queue.Where(q => q.idPacient == id_pacient && q.idHome == null && q.Closed == null && q.Date >= date_curr && q.Doctor.IdSpecialization == 1).OrderByDescending(c => c.Date).FirstOrDefault();
                Queue okulist = db.Queue.Where(q => q.idPacient == id_pacient && q.idHome == null && q.Closed == null && q.Date >= date_curr && q.Doctor.IdSpecialization == 2).OrderByDescending(c => c.Date).FirstOrDefault();
                Queue hirurg = db.Queue.Where(q => q.idPacient == id_pacient && q.idHome == null && q.Closed == null && q.Date >= date_curr && q.Doctor.IdSpecialization == 3).OrderByDescending(c => c.Date).FirstOrDefault();
                Queue terapevt = db.Queue.Where(q => q.idPacient == id_pacient && q.idHome == null && q.Closed == null && q.Date >= date_curr && q.Doctor.IdSpecialization == 4).OrderByDescending(c => c.Date).FirstOrDefault();

                if ((id_link == 4 && lor != null && hirurg != null && okulist != null) || id_link < 4)
                {
                    Queue queue = db.Queue.Where(q => q.Date == curr_date && q.idDoctor == curr_doctor && q.idPacient == id_pacient && q.Closed == null).OrderByDescending(c => c.Date).FirstOrDefault();
                    if (queue == null)
                    {
                        // Уже выбрана дата и врач и время, создание очереди
                        Queue new_que = new Queue()
                        {
                            id = 0,
                            idDoctor = (int)curr_doctor,
                            idPacient = (int)id_pacient,
                            idVisit = curr_time,
                            idHome = null,
                            Home_address = null,
                            Closed = null,
                            Note = null,
                            Date = (DateTime)curr_date,
                        };
                        db.Queue.Add(new_que);
                        int res = db.SaveChanges();
                        if (res > 0)
                        {
                            //db.Entry(new_que).Reload();
                            curr_time = null;
                        }
                        else
                        {
                            curr_time = null;
                            ModelState.AddModelError("", "Помилка збереження");
                        }
                    }
                    else
                    {
                        curr_time = null;
                        string mess = null;
                        if (queue.idVisit != null)
                        {
                            mess = string.Format("Запис не можливий, ви вже записані до лікаря: {0}, на період {1}", queue.Doctor.Sourname, queue.Visit.Period);
                        }
                        else
                        {
                            mess = string.Format("Запис не можливий, ви вже викликали лікаря: {0} на дім, очікуйте в період {1}", queue.Doctor.Sourname, queue.Home.Period);
                        }
                        ModelState.AddModelError("", mess);
                    }
                }
                else
                {
                    curr_time = null;
                    ModelState.AddModelError("", "Перед прийомом терапевта необхідно пройти вузьких специалістів");
                }


            }
            //
            DateTime current_date = DateTime.Now.Date;
            Queue queue_lor = db.Queue.Where(q => q.idPacient == id_pacient && q.idHome == null && q.Closed == null && q.Date >= current_date && q.Doctor.IdSpecialization == 1).OrderByDescending(c => c.Date).FirstOrDefault();
            Queue queue_okulist = db.Queue.Where(q => q.idPacient == id_pacient && q.idHome == null && q.Closed == null && q.Date >= current_date && q.Doctor.IdSpecialization == 2).OrderByDescending(c => c.Date).FirstOrDefault();
            Queue queue_hirurg = db.Queue.Where(q => q.idPacient == id_pacient && q.idHome == null && q.Closed == null && q.Date >= current_date && q.Doctor.IdSpecialization == 3).OrderByDescending(c => c.Date).FirstOrDefault();
            Queue queue_terapevt = db.Queue.Where(q => q.idPacient == id_pacient && q.idHome == null && q.Closed == null && q.Date >= current_date && q.Doctor.IdSpecialization == 4).OrderByDescending(c => c.Date).FirstOrDefault();

            if (queue_lor != null)
            {
                Doctor doc = db.Doctor.Find(queue_lor.idDoctor);
                if (doc != null) db.Entry(doc).Reload();
                Visit vis = db.Visit.Find(queue_lor.idVisit);
                if (vis != null) db.Entry(vis).Reload();
            }
            if (queue_okulist != null)
            {
                Doctor doc = db.Doctor.Find(queue_okulist.idDoctor);
                if (doc != null) db.Entry(doc).Reload();
                Visit vis = db.Visit.Find(queue_okulist.idVisit);
                if (vis != null) db.Entry(vis).Reload();
            }
            if (queue_hirurg != null)
            {
                Doctor doc = db.Doctor.Find(queue_hirurg.idDoctor);
                if (doc != null) db.Entry(doc).Reload();
                Visit vis = db.Visit.Find(queue_hirurg.idVisit);
                if (vis != null) db.Entry(vis).Reload();
            }
            if (queue_terapevt != null)
            {
                Doctor doc = db.Doctor.Find(queue_terapevt.idDoctor);
                if (doc != null) db.Entry(doc).Reload();
                Visit vis = db.Visit.Find(queue_terapevt.idVisit);
                if (vis != null) db.Entry(vis).Reload();
            }
            //
            ModelMedicalBoard mmb = new ModelMedicalBoard()
            {
                pacient = pacient,
                id_link = (int)id_link,
                queue_lor = queue_lor,
                queue_okulist = queue_okulist,
                queue_hirurg = queue_hirurg,
                queue_terapevt = queue_terapevt,
                curr_time =  curr_time
            };
            return View(mmb);


        }

    }
}