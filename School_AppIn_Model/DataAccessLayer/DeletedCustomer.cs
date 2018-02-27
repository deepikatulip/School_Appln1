namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DeletedCustomer
    {
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailID { get; set; }

        [Required]
        public string IsVisited { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string LeadSource { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string DeletedBy { get; set; }

        [StringLength(255)]
        public string OtherReason { get; set; }
    }
}
