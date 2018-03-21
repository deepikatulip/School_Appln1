using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_AppIn_Model.DataAccessLayer
{

    [Table("InvoiceFrequency")]
    public partial class InvoiceFrequency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InvoiceFrequency()
        {
            Fees_Configuration = new HashSet<Fees_Configuration>();
            Fees_Details = new HashSet<Fees_Details>();
            InvFrequencyCategories = new HashSet<InvFrequencyCategory>();
        }

        [Key]
        public int InvFrequencyId { get; set; }

        [Required]
        [StringLength(50)]
        public string InvFrequencyValue { get; set; }

        [Required]
        [StringLength(1)]
        public string InvFrequencyCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fees_Configuration> Fees_Configuration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fees_Details> Fees_Details { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvFrequencyCategory> InvFrequencyCategories { get; set; }
    }
}
