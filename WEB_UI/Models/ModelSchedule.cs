using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace WEB_UI.Models
{
    public class ModelSchedule
    {
        public Doctor doctor { get; set; }
        public List<Queue> list_queue { get; set; }       
        public int? type { get; set; }
        
    }
}