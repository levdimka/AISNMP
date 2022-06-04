namespace WEB_UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Queue")]
    public partial class Queue
    {
        public int id { get; set; }

        public int idDoctor { get; set; }

        public int idPacient { get; set; }

        public int? idVisit { get; set; }

        public int? idHome { get; set; }

        [StringLength(50)]
        public string Home_address { get; set; }

        public DateTime? Closed { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Примітка")]
        [StringLength(50)]
        public string Note { get; set; }

        public DateTime Date { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual Home Home { get; set; }

        public virtual Pacient Pacient { get; set; }

        public virtual Visit Visit { get; set; }

    }
}
