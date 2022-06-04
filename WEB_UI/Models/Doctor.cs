namespace WEB_UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Doctor")]
    public partial class Doctor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Doctor()
        {
            Card_information = new HashSet<Card_information>();
            Queue = new HashSet<Queue>();
        }

        public int id { get; set; }

        [Required]
        [Display(Name = "����� ����������")]
        public int Num_document { get; set; }

        [Required]
        [RegularExpression("[A-Za-z�-��-���������]{2,20}", ErrorMessage = "���� ���� ������� ������ ���� �����")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "������� ������� ���� �� 3 �� 50 �������")]
        [Display(Name = "��'�")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("[A-Za-z�-��-���������]{2,20}", ErrorMessage = "���� ���� ������� ������ ���� �����")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "������� ������� ���� �� 3 �� 50 �������")]
        [Display(Name = "�������")]
        public string Sourname { get; set; }

        [Required]
        [RegularExpression("[A-Za-z�-��-���������]{2,20}", ErrorMessage = "���� ���� ������� ������ ���� �����")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "������� ������� ���� �� 3 �� 50 �������")]
        [Display(Name = "��-�������")]
        public string Patronymic { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "������������ Email")]
        [StringLength(50)]
        [Display(Name = "Email")]

        public string Email { get; set; }

       
        [Display(Name = "����� ��������")]
        public long Number_of_telephone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        [Display(Name = "���� ����������")]
        public DateTime Date_of_birth { get; set; }

        
        [Display(Name = "���")]
        public int IdSpecialization { get; set; }

        [Required]
        
        [StringLength(50)]
        [Display(Name = "������")]
        public string Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Card_information> Card_information { get; set; }

        public virtual Specialization Specialization { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Queue> Queue { get; set; }
    }
}
