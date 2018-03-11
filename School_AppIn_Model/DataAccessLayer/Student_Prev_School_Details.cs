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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Student_PrevSchool_Id { get; set; }

        public long? Student_Id { get; set; }

        public int School_Id { get; set; }

        public string Other_School_Name { get; set; }

        public string Other_School_Address { get; set; }

        public long? Academic_Year { get; set; }


        public bool? Is_Active { get; set; }

        public bool? Is_Deleted { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created_On { get; set; }

        public string Created_By { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Updated_On { get; set; }

        public string Updated_By { get; set; }


        public long? From_Year { get; set; }

        public long? To_Year { get; set; }

        public string Leaving_Reason { get; set; }

        public byte[] Upload_Document1 { get; set; }

        public byte[] Upload_Document2 { get; set; }

        public string Comments { get; set; }
    }
}
