using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_AppIn_Model.DataAccessLayer
{
    [Table("InvFrequencyCategory")]
    public partial class InvFrequencyCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InvFrequencyCategory()
        {
            Fees_Configuration = new HashSet<Fees_Configuration>();
            Fees_Details = new HashSet<Fees_Details>();
        }

        [Key]
        public int FrequencyCategoryId { get; set; }

        [Required]
        [StringLength(10)]
        public string FrequencyCategoryCode { get; set; }

        [Required]
        [StringLength(50)]
        public string FrequencyForPeriod { get; set; }

        public int InvFrequencyId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fees_Configuration> Fees_Configuration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fees_Details> Fees_Details { get; set; }

        public virtual InvoiceFrequency InvoiceFrequency { get; set; }
    }
}
