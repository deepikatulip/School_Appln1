namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("School")]
    public partial class School
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int School_Id { get; set; }

        [StringLength(50)]
        public string School_Name { get; set; }

        public bool? Is_Active { get; set; }

        public bool? Is_Deleted { get; set; }

        public DateTime? Created_On { get; set; }

        public int? Created_By { get; set; }

        public DateTime? Updated_On { get; set; }

        public int? Updated_By { get; set; }
    }
}
