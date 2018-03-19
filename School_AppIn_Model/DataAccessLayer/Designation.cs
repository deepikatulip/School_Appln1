namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Designation")]
    public partial class Designation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Designation_Id { get; set; }

       // [StringLength(200)]
        public string Designation_Name { get; set; }

        //public bool? Is_Active { get; set; }

        //public bool? Is_Deleted { get; set; }

        //public DateTime? Created_On { get; set; }

        //public int? Created_By { get; set; }

        //public DateTime? Updated_On { get; set; }

        //public int? Updated_By { get; set; }
    }
}
