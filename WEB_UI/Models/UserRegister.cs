namespace WEB_UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pacient")]
    public class UserRegister
    {
        public int id { get; set; }

        [Required]
        [RegularExpression("[A-Za-zА-Яа-яЁёІіЇїЄє]{2,20}", ErrorMessage = "дане поле повинно містити лише букви")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "довжина повинна бути від 3 до 50 символів")]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("[A-Za-zА-Яа-яЁёІіЇїЄє]{2,20}", ErrorMessage = "дане поле повинно містити лише букви")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "довжина повинна бути від 3 до 50 символів")]
        [Display(Name = "Прізвище")]
        public string Sourname { get; set; }

        [Required]
        [RegularExpression("[A-Za-zА-Яа-яЁёІіЇїЄє]{2,20}", ErrorMessage = "дане поле повинно містити лише букви")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "довжина повинна бути від 3 до 50 символів")]
        [Display(Name = "По-батькові")]
        public string Patronymic { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "неправильний Email")]
        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        [Display(Name = "Дата народження")]
        public DateTime Date_of_birth { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Адреса")]
        public string Adress { get; set; }

        [Required]
        [Display(Name = "Номер телефону")]
        public long Number_of_telephone { get; set; }

        [Required]
        [Display(Name = "Номер картки")]
        public int Card_number { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Підтвердити Пароль")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
