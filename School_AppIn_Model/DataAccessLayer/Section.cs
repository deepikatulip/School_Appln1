namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Section")]
    public partial class Section
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Section_Id { get; set; }

        public int Class_Id { get; set; }

        [Required(ErrorMessage = "Section Name Required")]
        [StringLength(30)]
        [Display(Name = "Section")]
        public string Section_Name { get; set; }

        [Required(ErrorMessage = "Academic Year Required")]
        [Display(Name = "Academic Year")]
        public long? Academic_Year { get; set; }

        public bool Is_Active { get; set; }


        [Display(Name = "Created Date")]
        public DateTime? Created_On { get; set; }

        [Display(Name = "Created By")]
        public string Created_By { get; set; }

        [Display(Name = "Updated Date")]
        public DateTime? Updated_On { get; set; }

        [Display(Name = "Updated By")]
        public string Updated_By { get; set; }

        public bool? Is_Deleted { get; set; }

        [ForeignKey("Created_By")]
        public virtual User Created { get; set; }

        [ForeignKey("Updated_By")]
        public virtual User Updated { get; set; }

        [ForeignKey("Class_Id")]
        public virtual Class Class { get; set; }


    }
}
