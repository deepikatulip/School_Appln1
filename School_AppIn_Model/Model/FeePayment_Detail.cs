namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FeePayment_Detail
    {
        public long? Fees_Payment_Id { get; set; }

        [StringLength(10)]
        public string Fees_Recipt_No { get; set; }

        public long? Student_Id { get; set; }

        public int? Payment_Mode_Id { get; set; }

        public int? Term_Id { get; set; }

        public DateTime? Paid_Date { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Academic_Year { get; set; }

        public decimal? Total_Amount { get; set; }

        public DateTime? Fees_Next_Due_Date { get; set; }

        public bool? Is_Active { get; set; }

        public bool? Is_Deleted { get; set; }

        public DateTime? Created_On { get; set; }

        public int? Created_By { get; set; }

        public DateTime? Updated_On { get; set; }

        public int? Updated_By { get; set; }
    }
}
