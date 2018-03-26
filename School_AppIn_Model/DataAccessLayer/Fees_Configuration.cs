using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_AppIn_Model.DataAccessLayer
{
   public partial class Fees_Configuration
    {
        public Fees_Configuration()
        {
            Fees_Details = new HashSet<Fees_Details>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeesId { get; set; }


        [Required(ErrorMessage = "Select Class")]
        [Display(Name = "Class")]
        public int? Class_Id { get; set; }

        [Required(ErrorMessage = "Select Frequency")]
        [Display(Name = "Frequency")]
        public int? FrequencyCategoryId { get; set; }

        [Required(ErrorMessage = "Select Period")]
        [Display(Name = "Period")]
        public int? InvFrequencyId { get; set; }

        public long? Academic_Year { get; set; }

        [Required(ErrorMessage = "Fees Description Required")]
        [Display(Name = "Fees Desc")]
        [StringLength(250)]
        public string FeesDescription { get; set; }

        [Required(ErrorMessage = "Fees Required")]
        [Display(Name = "Fees")]
        public decimal? Fees { get; set; }

        [Display(Name = "Created")]
        public string Created_By { get; set; }

        //   [Column(TypeName = "DateTime2")]
        [Display(Name = "Created Dt")]
        public Nullable<DateTime> Created_On { get; set; }
        // [Column(TypeName = "DateTime2")]
        [Display(Name = "Updated")]
        public Nullable<DateTime> Updated_On { get; set; }

        [Display(Name = "Updated Dt")]
        public string Updated_By { get; set; }

        public bool Is_Active { get; set; }

        public bool? Is_Deleted { get; set; }

        public virtual Class Class { get; set; }

        public virtual InvFrequencyCategory InvFrequencyCategory { get; set; }

        public virtual InvoiceFrequency InvoiceFrequency { get; set; }

        [ForeignKey("Created_By")]
        public virtual User Created { get; set; }

        [ForeignKey("Updated_By")]
        public virtual User Updated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fees_Details> Fees_Details { get; set; }
    }
}
