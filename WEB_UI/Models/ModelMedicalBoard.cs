using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_UI.Models
{
    public class ModelMedicalBoard
    {
        public int id_link { get; set; }
        public Pacient pacient { get; set; }
        public Queue queue_lor { get; set; }
        public Queue queue_okulist { get; set; }
        public Queue queue_hirurg { get; set; }
        public Queue queue_terapevt { get; set; }

        public int? curr_time { get; set; }
    }
}