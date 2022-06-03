using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_UI.Models
{
    public class UserLogin
    {
        [Required]
        public int NumDocument { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}