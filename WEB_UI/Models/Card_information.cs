namespace WEB_UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Card_information
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Card_information()
        {

        }
        public int id { get; set; }

        public int Card_number { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        [Display(Name = "Дата визита")]
        public DateTime Date_of_receipt { get; set; }

        public bool? Medical_board { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Жалобы")]
        [StringLength(1000)]
        public string Complaints { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Диагноз")]
        [StringLength(1000)]
        public string Diagnosis { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Рецепт")]
        [StringLength(1000)]
        public string Recipe { get; set; }

        public int idDoctor { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        [Display(Name = "Закрыть бальничный")]
        public DateTime? Closed { get; set; }

        public virtual Card Card { get; set; }

        public virtual Doctor Doctor { get; set; }

    }
}
