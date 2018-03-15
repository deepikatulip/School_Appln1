namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Staff_Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Staff_Type_Id { get; set; }

        [StringLength(50)]
        public string Staff_Type_Name { get; set; }

        public long? Academic_Year { get; set; }

        //public bool? Is_Active { get; set; }

        //public bool? Is_Deleted { get; set; }

        //public DateTime? Created_On { get; set; }

        //public int? Created_By { get; set; }

        //public DateTime? Deleted_On { get; set; }

        //public int? Deleted_By { get; set; }
    }
}
