namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Student_Sibling_Details
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? Sibling_Id { get; set; }

        [StringLength(50)]
        public string Sibling_Name { get; set; }

        public long? Student_Id { get; set; }

        public int Class_Id { get; set; }

        public int Section_Id { get; set; }

        public long Academic_Year { get; set; }

        public bool Is_Active { get; set; }

        public bool Is_Deleted { get; set; }

        public DateTime Created_On { get; set; }

        public string Created_By { get; set; }

        public DateTime? Deleted_On { get; set; }

        public string Deleted_By { get; set; }
    }
}
