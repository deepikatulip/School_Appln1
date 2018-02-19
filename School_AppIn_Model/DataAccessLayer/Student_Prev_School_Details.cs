namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Student_Prev_School_Details
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Student_PrevSchool_Id { get; set; }

        public long? Student_Id { get; set; }

        [StringLength(200)]
        public string School_Name { get; set; }

        [StringLength(100)]
        public string Address_Line1 { get; set; }

        public int? City_Id { get; set; }

        public int? State_Id { get; set; }

        public int? Country_Id { get; set; }

        public long? Academic_Year { get; set; }

        public bool? Is_Active { get; set; }

        public bool? Is_Deleted { get; set; }

        public DateTime? Created_On { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Created_By { get; set; }

        public DateTime? Deleted_On { get; set; }

        public int? Deleted_By { get; set; }

        public long? From_Year { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long To_Year { get; set; }

        [Key]
        [Column(Order = 3)]
        public string Leaving_Reason { get; set; }

        public byte[] Upload_Document1 { get; set; }

        public byte[] Upload_Document2 { get; set; }

        public string Comments { get; set; }
    }
}
