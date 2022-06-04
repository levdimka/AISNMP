using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace WEB_UI.Models
{
    public class ModelDoctorsHouseCall : ModelDoctorsAppointment {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "довжина повинна бути від 3 до 50 символів")]
        [Display(Name = "Адреса виклику")]
        public string curr_address { get; set; }
    }

}