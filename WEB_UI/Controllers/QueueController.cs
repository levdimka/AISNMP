using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_UI.DAL;
using WEB_UI.Models;
using System.Net;
using System.Data.Entity.Infrastructure;

namespace WEB_UI.Controllers
{
    public class QueueController : Controller
    {
        private MedicalContext db = new MedicalContext();

        private int? get_idPacient()
        {
            if (User.Identity.IsAuthenticated)
            {
                int num_doc = int.Parse(User.Identity.Name);
                Pacient pac = db.Pacient.Where(p => p.Card_number == num_doc).FirstOrDefault();
                return pac != null ? (int?)pac.id : null;
            }
            return null;
        }
        // GET: Queue
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QueueToDoctor1(int? id_pacient, int? id_visit, int? id_specialization, DateTime? curr_date, int? curr_doctor, int? curr_time)
        {

            List<Doctor> list_doctor = new List<Doctor>();
            List<Visit> list_free_time = null;
            List<VisitPacient> list_vpacient = null;
            id_pacient = get_idPacient();
            // Определим пациента
            if (id_pacient == null)
            {
                return new HttpUnauthorizedResult();
            }
            Pacient pacient = db.Pacient.Find(id_pacient);
            if (pacient == null)
            {
                return HttpNotFound();
            }

            if (id_visit != null)
            {
                Queue queue = db.Queue.Find(id_visit);
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
                        curr_time = null;
                    }
                    catch (RetryLimitExceededException)
                    {
                        ViewBag.ErrorMessage = "Delete failed. Ups!!";
                    }
                }
            }


            if (curr_date == null || (curr_date != null && ((DateTime)curr_date).Date >= DateTime.Now.Date))
            {
                // Читаем ранее сохраненые в прошлом обращении переменные текущая дата идоктор
                object o_curr_date = Session["curr_date" + (id_specialization != null ? id_specialization.ToString() : "0")];
                DateTime? s_curr_date = o_curr_date != null ? (DateTime?)DateTime.Parse(o_curr_date.ToString()) : null;
                object o_curr_doctor = Session["curr_doctor" + (id_specialization != null ? id_specialization.ToString() : "0")];
                int? s_curr_doctor = o_curr_doctor != null ? (int?)int.Parse(o_curr_doctor.ToString()) : null;
                // Изменилась дата
                if (s_curr_date != curr_date)
                {
                    curr_time = null;
                    Session["curr_date" + (id_specialization != null ? id_specialization.ToString() : "0")] = curr_date;
                }
                if (s_curr_doctor != curr_doctor)
                {
                    curr_time = null;
                    Session["curr_doctor" + (id_specialization != null ? id_specialization.ToString() : "0")] = curr_doctor;
                }

                // Получим список терапевтов

                if (id_specialization != null)
                {
                    list_doctor = db.Doctor.Where(d => d.IdSpecialization == id_specialization).ToList();
                }
                else
                {
                    list_doctor = db.Doctor.ToList();
                }

                // Выполнем операцию добавить строку очереди
                if (curr_date != null && curr_doctor != null && curr_time != null && id_visit == null)
                {
                    Queue queue = db.Queue.Where(q => q.Date == curr_date && q.idDoctor == curr_doctor && q.idPacient == id_pacient && q.Closed == null).FirstOrDefault();
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
                            curr_time = null;
                        }
                        else
                        {
                            curr_time = null;
                            ModelState.AddModelError("", "Ошибка сохранеия");
                        }
                    }
                    else
                    {
                        curr_time = null;
                        string mess = null;
                        if (queue.idVisit != null)
                        {
                            mess = string.Format("Запись не возможна, вы уже записаны к врачу: {0}, на период {1}", queue.Doctor.Sourname, queue.Visit.Period);
                        }
                        else
                        {
                            mess = string.Format("Запись не возможна, вы уже вызвали врача: {0} надом, ожидайте в период {1}", queue.Doctor.Sourname, queue.Home.Period);
                        }
                        ModelState.AddModelError("", mess);
                    }
                }
            }
            else
            {
                curr_time = null;
                ModelState.AddModelError("", "Дата записи к врачу не может быть меньше текущей даты");
            }

            // Выбор свободного периода для записи к врачу, и список записей к врачу
            if (curr_date != null && curr_doctor != null && curr_time == null)
            {
                // Уже выбрана дата и врач
                //string sql = "SELECT * FROM [Medical].[dbo].[Visit] where [id] not in  (SELECT [idVisit] FROM [Medical].[dbo].[Queue] where [idDoctor]=" + curr_doctor.ToString() + " and [Date] = CONVERT(date, '" + ((DateTime)curr_date).ToString("yyyy-MM-dd 00:00:00") + "', 120))";
                //list_free_time = db.Database.SqlQuery<Visit>(sql).ToList();

                string sql = "if ((SELECT [idVisit] FROM [Medical].[dbo].[Queue] where [idDoctor]=" + curr_doctor.ToString() + " and [Date] = CONVERT(date, '" + ((DateTime)curr_date).ToString("yyyy-MM-dd 00:00:00") + "', 120)) is not null) " +
                    "begin SELECT* FROM[Medical].[dbo].[Visit] where[id] not in (SELECT [idVisit] FROM[Medical].[dbo].[Queue] where[idDoctor] = " + curr_doctor.ToString() + " and[Date] = CONVERT(date, '" + ((DateTime)curr_date).ToString("yyyy-MM-dd 00:00:00") + "', 120)) " +
                    "end else begin SELECT* FROM[Medical].[dbo].[Visit] where[id] not in (0) end";
                list_free_time = db.Database.SqlQuery<Visit>(sql).ToList();
                // получим очередь
                sql = "SELECT q.id, v.Period ,p.Sourname FROM [Medical].[dbo].[Queue] as q Left JOIN [dbo].[Visit] as v ON v.id = q.[idVisit] Left JOIN [dbo].[Pacient] as p ON p.id= q.[idPacient] where q.[Date] = CONVERT(date , '" + ((DateTime)curr_date).ToString("yyyy-MM-dd 00:00:00") + "',120) and q.[idDoctor]=" + curr_doctor.ToString() + " AND q.Closed is NULL AND q.idHome is null";
                list_vpacient = db.Database.SqlQuery<VisitPacient>(sql).ToList();
            }
            // Подготовим модель
            ModelDoctorsAppointment qp = new ModelDoctorsAppointment()
            {
                Pacient = pacient,
                curr_doctor = curr_doctor,
                curr_date = curr_date != null ? (DateTime)curr_date : DateTime.Now,
                list_doctor = new SelectList(list_doctor.ToList(), "id", "Sourname"),
                list_time = list_free_time != null ? new SelectList(list_free_time.ToList(), "id", "Period") : null,
                list_vpacient = list_vpacient,
                id_specialization = id_specialization,
            };
            return View(qp);
        }
        public ActionResult QueueToDoctor(int? id_pacient, int? id_specialization, int? id_queue, DateTime? curr_date, int? curr_doctor, int? curr_time)
        {

            List<Doctor> list_doctor = new List<Doctor>();
            List<Visit> list_free_time = null;
            List<VisitPacient> list_vpacient = null;
            Queue queue = null;
            id_pacient = get_idPacient();
            // Определим пациента
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
                queue = db.Queue.Find(id_queue);
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
            if (curr_date == null || (curr_date != null && ((DateTime)curr_date).Date >= DateTime.Now.Date))
            {
                // Читаем ранее сохраненые в прошлом обращении переменные текущая дата идоктор
                object o_curr_date = Session["curr_date" + (id_specialization != null ? id_specialization.ToString() : "0")];
                DateTime? s_curr_date = o_curr_date != null ? (DateTime?)DateTime.Parse(o_curr_date.ToString()) : null;
                object o_curr_doctor = Session["curr_doctor" + (id_specialization != null ? id_specialization.ToString() : "0")];
                int? s_curr_doctor = o_curr_doctor != null ? (int?)int.Parse(o_curr_doctor.ToString()) : null;
                // Изменилась дата
                if (s_curr_date != curr_date)
                {
                    curr_time = null;
                    Session["curr_date" + (id_specialization != null ? id_specialization.ToString() : "0")] = curr_date;
                }
                if (s_curr_doctor != curr_doctor)
                {
                    curr_time = null;
                    Session["curr_doctor" + (id_specialization != null ? id_specialization.ToString() : "0")] = curr_doctor;
                }

                // Получим список терапевтов

                if (id_specialization != null)
                {
                    list_doctor = db.Doctor.Where(d => d.IdSpecialization == id_specialization).ToList();
                }
                else
                {
                    list_doctor = db.Doctor.ToList();
                }

                // Выполнем операцию добавить строку очереди
                if (curr_date != null && curr_doctor != null && curr_time != null)
                {
                    queue = db.Queue.Where(q => q.Date == curr_date && q.idDoctor == curr_doctor && q.idPacient == id_pacient && q.Closed == null).OrderByDescending(c=>c.Date).FirstOrDefault();
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
                            ModelState.AddModelError("", "Ошибка сохранеия");
                        }
                    }
                    else
                    {
                        curr_time = null;
                    }
                }
            }
            else
            {
                curr_time = null;
                ModelState.AddModelError("", "Дата записи к врачу не может быть меньше текущей даты");
            }
            // Выбор свободного периода для записи к врачу, и список записей к врачу
            DateTime current_date = DateTime.Now.Date;
            List<Queue> list_queue = db.Queue.Where(q => q.idPacient == id_pacient && q.idHome==null && q.Date>=current_date && q.Doctor.IdSpecialization == id_specialization).OrderByDescending(c=>c.Date).ToList();
            if (curr_doctor != null) {
                list_queue = list_queue.Where(q => q.Doctor.id == curr_doctor).ToList();
            }
            foreach (Queue q in list_queue) {
                Visit visit = db.Visit.Find(q.idVisit);
                if (visit!=null) db.Entry(visit).Reload();
            }
            // Выбор свободного периода для записи к врачу, и список записей к врачу
            if (curr_date != null && curr_doctor != null && curr_time == null)
            {
                // Уже выбрана дата и врач
                string sql = "if ((SELECT top(1) [idVisit] FROM [Medical].[dbo].[Queue] where [idDoctor]=" + curr_doctor.ToString() + " and [Date] = CONVERT(date, '" + ((DateTime)curr_date).ToString("yyyy-MM-dd 00:00:00") + "', 120)) is not null) " +
                    "begin SELECT* FROM[Medical].[dbo].[Visit] where[id] not in (SELECT [idVisit] FROM[Medical].[dbo].[Queue] where[idDoctor] = " + curr_doctor.ToString() + " and[Date] = CONVERT(date, '" + ((DateTime)curr_date).ToString("yyyy-MM-dd 00:00:00") + "', 120)) " +
                    "end else begin SELECT* FROM[Medical].[dbo].[Visit] where[id] not in (0) end";
                list_free_time = db.Database.SqlQuery<Visit>(sql).ToList();
            }
            // Подготовим модель
            ModelDoctorsAppointment qp = new ModelDoctorsAppointment()
            {
                Pacient = pacient, // Пациент
                id_specialization = id_specialization, // Выбранная специализация доктора
                curr_doctor = curr_doctor, // текущий доктор
                curr_date = curr_date != null ? (DateTime)curr_date : DateTime.Now, // текущая дата
                list_doctor = new SelectList(list_doctor.ToList(), "id", "Sourname"), // список докторов по специальности
                list_time = list_free_time != null ? new SelectList(list_free_time.ToList(), "id", "Period") : null, // список свободного времени для выбранного доктора
                list_vpacient = list_vpacient, // Может урать стал list_queue
                list_queue =  list_queue, // 
            };
            return View(qp);
        }
        public ActionResult QueueToHomeDoctor(int? id_pacient, int? id_visit, int? id_specialization, string curr_address, DateTime? curr_date, int? curr_doctor, int? curr_time)
        {

            List<Doctor> list_doctor = new List<Doctor>();
            List<Visit> list_free_time = null;
            List<VisitPacient> list_vpacient = null;
            id_pacient = get_idPacient();
            // Определим пациента
            if (id_pacient == null)
            {
                return new HttpUnauthorizedResult();
            }
            Pacient pacient = db.Pacient.Find(id_pacient);
            if (pacient == null)
            {
                return HttpNotFound();
            }

            if (id_visit != null)
            {
                Queue queue = db.Queue.Find(id_visit);
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
                        curr_time = null;
                    }
                    catch (RetryLimitExceededException)
                    {
                        ViewBag.ErrorMessage = "Delete failed. Ups!!";
                    }
                }
            }


            if (curr_date == null || (curr_date != null && ((DateTime)curr_date).Date >= DateTime.Now.Date))
            {
                // Читаем ранее сохраненые в прошлом обращении переменные текущая дата идоктор
                object o_curr_date = Session["curr_date"];
                DateTime? s_curr_date = o_curr_date != null ? (DateTime?)DateTime.Parse(o_curr_date.ToString()) : null;
                object o_curr_doctor = Session["curr_doctor"];
                int? s_curr_doctor = o_curr_doctor != null ? (int?)int.Parse(o_curr_doctor.ToString()) : null;
                // Изменилась дата
                if (s_curr_date != curr_date)
                {
                    curr_time = null;
                    Session["curr_date"] = curr_date;
                }
                if (s_curr_doctor != curr_doctor)
                {
                    curr_time = null;
                    Session["curr_doctor"] = curr_doctor;
                }

                // Получим список терапевтов

                if (id_specialization != null)
                {
                    list_doctor = db.Doctor.Where(d => d.IdSpecialization == id_specialization).ToList();
                }
                else
                {
                    list_doctor = db.Doctor.ToList();
                }

                // Выполнем операцию добавить строку очереди
                if (curr_address != null && curr_date != null && curr_doctor != null && curr_time != null && id_visit == null)
                {
                    Queue queue = db.Queue.Where(q => q.Date == curr_date && q.idDoctor == curr_doctor && q.idPacient == id_pacient && q.Closed == null).FirstOrDefault();
                    if (queue == null)
                    {
                        // Уже выбрана дата и врач и время, создание очереди
                        Queue new_que = new Queue()
                        {
                            id = 0,
                            idDoctor = (int)curr_doctor,
                            idPacient = (int)id_pacient,
                            idVisit = null,
                            idHome = curr_time,
                            Home_address = curr_address,
                            Closed = null,
                            Note = null,
                            Date = (DateTime)curr_date,
                        };
                        db.Queue.Add(new_que);
                        int res = db.SaveChanges();
                        if (res > 0)
                        {
                            curr_time = null;
                        }
                        else
                        {
                            curr_time = null;
                            ModelState.AddModelError("", "Ошибка сохранеия");
                        }
                    }
                    else
                    {
                        curr_time = null;
                        string mess = null;
                        if (queue.idVisit != null)
                        {
                            mess = string.Format("Запись не возможна, вы уже записаны к врачу: {0}, на период {1}", queue.Doctor.Sourname, queue.Visit.Period);
                        }
                        else
                        {
                            mess = string.Format("Запись не возможна, вы уже вызвали врача: {0} надом, ожидайте в период {1}", queue.Doctor.Sourname, queue.Home.Period);
                        }
                        ModelState.AddModelError("", mess);



                    }
                }
            }
            else
            {
                curr_time = null;
                ModelState.AddModelError("", "Дата записи к врачу не может быть меньше текущей даты");
            }

            // Выбор свободного периода для записи к врачу, и список записей к врачу
            if (curr_date != null && curr_doctor != null && curr_time == null)
            {
                // Уже выбрана дата и врач
                string sql = "if ((SELECT top(1) [idHome] FROM [Medical].[dbo].[Queue] where [idDoctor]=" + curr_doctor.ToString() + " and [Date] = CONVERT(date, '" + ((DateTime)curr_date).ToString("yyyy-MM-dd 00:00:00") + "', 120)) is not null) " +
                            "begin SELECT* FROM[Medical].[dbo].[Home] where[id] not in (SELECT[idHome] FROM[Medical].[dbo].[Queue] where[idDoctor] = " + curr_doctor.ToString() + " and[Date] = CONVERT(date, '" + ((DateTime)curr_date).ToString("yyyy-MM-dd 00:00:00") + "', 120)) " +
                            "end else begin SELECT* FROM[Medical].[dbo].[Home] where[id] not in (0) end";
                list_free_time = db.Database.SqlQuery<Visit>(sql).ToList();
                // получим очередь
                sql = "SELECT q.id, v.Period ,p.Sourname FROM [Medical].[dbo].[Queue] as q Left JOIN [dbo].[Home] as v ON v.id = q.[idHome] Left JOIN [dbo].[Pacient] as p ON p.id= q.[idPacient] where q.[Date] = CONVERT(date , '" + ((DateTime)curr_date).ToString("yyyy-MM-dd 00:00:00") + "',120) and q.[idDoctor]=" + curr_doctor.ToString() + " AND q.Closed is NULL AND q.idVisit is null";
                list_vpacient = db.Database.SqlQuery<VisitPacient>(sql).ToList();
            }
            // Подготовим модель
            ModelDoctorsHouseCall qp = new ModelDoctorsHouseCall()
            {
                Pacient = pacient,
                curr_address = pacient.Adress,
                curr_doctor = curr_doctor,
                curr_date = curr_date != null ? (DateTime)curr_date : DateTime.Now,
                list_doctor = new SelectList(list_doctor.ToList(), "id", "Sourname"),
                list_time = list_free_time != null ? new SelectList(list_free_time.ToList(), "id", "Period") : null,
                list_vpacient = list_vpacient,
                id_specialization = id_specialization,
            };
            return View(qp);
        }
        public ActionResult ListQueue(int? id_pacient)
        {
            id_pacient = get_idPacient();
            List<Queue> list_queue = db.Queue.Where(q => q.idPacient == id_pacient && q.Closed == null).ToList();
            return View(list_queue);
        }

    }
}