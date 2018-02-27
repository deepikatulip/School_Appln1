namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Subject_Detail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Subject_Detail_Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Class_Id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Section_Id { get; set; }

        public long? Academic_Year { get; set; }

        public int? Subject_Id1 { get; set; }

        public int? Subject_Id2 { get; set; }

        public int? Subject_Id3 { get; set; }

        public int? Subject_Id4 { get; set; }

        public int? Subject_Id5 { get; set; }

        public int? Subject_Id6 { get; set; }

        public int? Subject_Id7 { get; set; }

        public int? Subject_Id8 { get; set; }

        public int? Subject_Id9 { get; set; }

        public int? Subject_Id10 { get; set; }

        public bool? Is_Active { get; set; }

        public bool? Is_Deleted { get; set; }

        public DateTime? Created_On { get; set; }

        public int? Created_By { get; set; }

        public DateTime? Updated_On { get; set; }

        public int? Updated_By { get; set; }
    }
}
