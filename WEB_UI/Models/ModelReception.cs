using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace WEB_UI.Models
{
    public class ModelReception
    {
        public Doctor doctor { get; set; }
        public Queue queue { get; set; }

        public Card_information card_information  { get; set; }


    }
}