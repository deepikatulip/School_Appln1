namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Student_Id { get; set; }

        [StringLength(20)]
        public string Roll_No { get; set; }

        [StringLength(50)]
        public string First_Name { get; set; }

        [StringLength(50)]
        public string Middle_Name { get; set; }

        [StringLength(50)]
        public string Last_Name { get; set; }

        public int Gender_Id { get; set; }

        public DateTime DOB { get; set; }

        public DateTime Enrollment_Date { get; set; }

        [StringLength(50)]
        public string Father_Name { get; set; }

        [StringLength(50)]
        public string Mother_Name { get; set; }

        public int Blood_Group_Id { get; set; }

        [StringLength(500)]
        public string Address_Line1 { get; set; }

        [StringLength(500)]
        public string Address_Line2 { get; set; }

        public int City_Id { get; set; }

        public int State_Id { get; set; }

        public int Country_Id { get; set; }

        [StringLength(10)]
        public string Phone_No1 { get; set; }

        [StringLength(10)]
        public string Phone_No2 { get; set; }

        [StringLength(10)]
        public string LandLine { get; set; }

        [StringLength(30)]
        public string Email_Id { get; set; }

        public long Academic_Year { get; set; }
        [Display(Name = "Created Date")]
        public DateTime? Created_On { get; set; }

        [Display(Name = "Created By")]
        public string Created_By { get; set; }

        [Display(Name = "Updated Date")]
        public DateTime? Updated_On { get; set; }

        [Display(Name = "Updated By")]
        public string Updated_By { get; set; }

        public bool Is_Active { get; set; }

        public bool Is_Deleted { get; set; }

        [StringLength(10)]
        public string Pincode { get; set; }

        public byte[] Photo { get; set; }


        public long Aadhar_No { get; set; }

        public int Class_Id { get; set; }

        public int Section_Id { get; set; }

        public bool Is_HostelStudent { get; set; }

        public bool Is_FeesDueRemaining { get; set; }

        public decimal Fees_Due_Amount { get; set; }


        [ForeignKey("Section_Id")]
        public virtual Section FSection { get; set; }

        [ForeignKey("Class_Id")]
        public virtual Class FClass { get; set; }


        [ForeignKey("Gender_Id")]
        public virtual Gender Gender { get; set; }

        [ForeignKey("Blood_Group_Id")]
        public virtual Blood_Group BloodGroup { get; set; }


        //[ForeignKey("Created_By")]
        //public virtual User Created { get; set; }

        //[ForeignKey("Updated_By")]
        //public virtual User Updated { get; set; }


    }
}
