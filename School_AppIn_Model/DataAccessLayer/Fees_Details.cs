using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_AppIn_Model.DataAccessLayer
{
    public partial class Fees_Details
    {
        [Key]
        public int Fees_DtlId { get; set; }

        public long? Student_Id { get; set; }

        public int? Class_Id { get; set; }

        [StringLength(100)]
        public string Roll_No { get; set; }

        public DateTime? DueDate { get; set; }

        public int? GracePeriod { get; set; }

        public int InvFrequencyId { get; set; }

        public int FrequencyCategoryId { get; set; }

        public int? Fees_Id { get; set; }

        public decimal? Fees { get; set; }

        public decimal? Penalty { get; set; }

        [StringLength(250)]
        public string Other_Fees_Desc { get; set; }

        public decimal? Other_Fees { get; set; }

        public decimal? Tax { get; set; }

        public decimal? Total_Fees { get; set; }

        public virtual Class Class { get; set; }

        public virtual Fees_Configuration Fees_Configuration { get; set; }

        public virtual InvFrequencyCategory InvFrequencyCategory { get; set; }

        public virtual InvoiceFrequency InvoiceFrequency { get; set; }

        public virtual Student Student { get; set; }
    }
}
