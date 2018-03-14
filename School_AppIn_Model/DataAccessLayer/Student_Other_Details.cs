namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class Student_Other_Details
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long StudentDetail_Id { get; set; }

        public long Student_Id { get; set; }

        [StringLength(500)]
        public string Identification_Mark1 { get; set; }

        [StringLength(500)]
        public string Identification_Mark2 { get; set; }

        public bool Is_Allergic { get; set; }

        public string Allergy_Details { get; set; }

        public int Father_Occupation_Id { get; set; }

        public decimal Father_Annual_Income { get; set; }

        public int Mother_Occupation_Id { get; set; }

        public decimal? Mother_Annual_Income { get; set; }

        public int Category_Id { get; set; }

        [StringLength(50)]
        public string Caste { get; set; }

        [StringLength(50)]
        public string Religion { get; set; }

        [StringLength(10)]
        public string Languages_Known { get; set; }

        public int? Second_Language_Opted_Id { get; set; }

        public byte[] Birth_Certificate { get; set; }

        public byte[] Upload_Document1 { get; set; }

        public byte[] UpLoad_Document2 { get; set; }

        public long Academic_Year { get; set; }

        public bool Is_Active { get; set; }

        public bool Is_Deleted { get; set; }

        public DateTime? Created_On { get; set; }

        public string Created_By { get; set; }

        public DateTime? Updated_On { get; set; }

        public string Updated_By { get; set; }
    }
}
