namespace WEB_UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Recipe")]
    public partial class Recipe
    {
        public int id { get; set; }

        public int idCard_information { get; set; }

        [Required]
        [StringLength(1000)]
        public string Drug { get; set; }

        [Required]
        [StringLength(1000)]
        public string Dosage { get; set; }

        public virtual Card_information Card_information { get; set; }
    }
}
