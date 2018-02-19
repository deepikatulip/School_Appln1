namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Fee_Detail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Fees_Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Class_Id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Section_Id { get; set; }

        public int? Term_Id { get; set; }

        public long? Academic_Year { get; set; }

        public int? Fee_Component_Id1 { get; set; }

        public int? Fee_Component_Id2 { get; set; }

        public int? Fee_Component_Id3 { get; set; }

        public int? Fee_Component_Id4 { get; set; }

        public int? Fee_Component_Id5 { get; set; }

        public int? Fee_Component_Id6 { get; set; }

        public int? Fee_Component_Id7 { get; set; }

        public int? Fee_Component_Id8 { get; set; }

        public int? Fee_Component_Id9 { get; set; }

        public int? Fee_Component_Id10 { get; set; }

        public decimal? Total { get; set; }

        public decimal? Fee_Component_Amount1 { get; set; }

        public decimal? Fee_Component_Amount2 { get; set; }

        public decimal? Fee_Component_Amount3 { get; set; }

        public decimal? Fee_Component_Amount4 { get; set; }

        public decimal? Fee_Component_Amount5 { get; set; }

        public decimal? Fee_Component_Amount6 { get; set; }

        public decimal? Fee_Component_Amount7 { get; set; }

        public decimal? Fee_Component_Amount8 { get; set; }

        public decimal? Fee_Component_Amount9 { get; set; }

        public decimal? Fee_Component_Amount10 { get; set; }

        public bool? Is_Active { get; set; }

        public bool? Is_Deleted { get; set; }

        public DateTime? Created_On { get; set; }

        public int? Created_By { get; set; }

        public DateTime? Updated_On { get; set; }

        public int? Updated_By { get; set; }
    }
}
