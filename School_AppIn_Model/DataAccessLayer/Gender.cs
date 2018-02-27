namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Gender
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }

        //[StringLength(50)]
        //public string Name { get; set; }

        //public bool? Is_Active { get; set; }

        //public bool? Is_Deleted { get; set; }

        //public DateTime? Created_On { get; set; }

        //public int? Created_By { get; set; }

        //public DateTime? Updated_On { get; set; }

        //public int? Updated_By { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

    }
}
