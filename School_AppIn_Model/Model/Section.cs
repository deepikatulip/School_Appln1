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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Section_Id { get; set; }

        public int? Class_Id { get; set; }

        [StringLength(30)]
        public string Section_Name { get; set; }

        public long? Academic_Year { get; set; }

        public bool? Is_Active { get; set; }

        public bool? Is_Deleted { get; set; }

        public DateTime? Created_On { get; set; }

        public int? Created_By { get; set; }

        public DateTime? Updated_On { get; set; }

        public int? Updated_By { get; set; }

        public virtual Class Class { get; set; }
    }
}
