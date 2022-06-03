using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace WEB_UI.Models
{
    public class ModelDoctorsAppointment
    {        
        [Required]
        public Pacient Pacient { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        [Display(Name = "Оберіть дату")]
        public DateTime curr_date { get; set; }
        public int? curr_doctor { get; set; }
        public int? curr_time { get; set; }
        public int? id_specialization { get; set; }
        public List<Queue> list_queue { get; set; }
        [Required]
        [Display(Name = "Оберіть лікаря")]
        public SelectList list_doctor { get; set; }

        [Display(Name = "Оберіть час")]
        public SelectList list_time { get; set; }
        public List<VisitPacient> list_vpacient { get; set; }

    }
}