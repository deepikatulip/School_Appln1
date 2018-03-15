namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Staff_Id { get; set; }

        public int? Staff_Type_Id { get; set; }

        [StringLength(50)]
        public string First_Name { get; set; }

		[StringLength(50)]
		public string Employee_Id { get; set; }

		[StringLength(50)]
        public string Middle_Name { get; set; }

        [StringLength(50)]
        public string Last_Name { get; set; }

        public int? Gender_Id { get; set; }

        public DateTime? DOB { get; set; }

        public DateTime? Date_Of_Joining { get; set; }

        [StringLength(50)]
        public string Father_Name { get; set; }

        [StringLength(10)]
        public string Mobile_No { get; set; }

        [StringLength(10)]
        public string Alt_Mobile_No { get; set; }

        [StringLength(30)]
        public string Email_Id { get; set; }

        public int? Blood_Group_Id { get; set; }

        [StringLength(500)]
        public string Address_Line1 { get; set; }

        [StringLength(500)]
        public string Address_Line2 { get; set; }

        public int? City_Id { get; set; }

        public int? State_Id { get; set; }

        public int? Country_Id { get; set; }

		public int Experience_in_Years { get; set; }

		public bool Is_Married { get; set; }

		[StringLength(10)]
        public string PinCode { get; set; }

        //[StringLength(50)]
        //public string Experience { get; set; }

        public long? Academic_Year { get; set; }

        public bool? Is_Active { get; set; }

        public bool? Is_Deleted { get; set; }

        public DateTime? Created_On { get; set; }


		[Display(Name = "Created By")]
		public string Created_By { get; set; }

		[Display(Name = "Updated Date")]
		public DateTime? Updated_On { get; set; }

		[Display(Name = "Aadhar No")]
		public string Aadhar_Number { get; set; }

		

		[Display(Name = "Updated By")]
		public string Updated_By { get; set; }

		public byte[] Photo { get; set; }

		//[ForeignKey("Staff_Type_Id")]
		//public virtual Staff_Type FStaff_Type { get; set; }

		[ForeignKey("Gender_Id")]
		public virtual Gender Gender { get; set; }

		[ForeignKey("Blood_Group_Id")]
		public virtual Blood_Group BloodGroup { get; set; }

	}
}
