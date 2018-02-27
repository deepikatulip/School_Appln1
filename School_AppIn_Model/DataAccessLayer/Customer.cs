namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        public int CustomerID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailID { get; set; }

        [StringLength(5)]
        public string IsVisited { get; set; }

        public string Status { get; set; }

        public string LeadSource { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? ContactDate { get; set; }

        [StringLength(255)]
        public string OtherReason { get; set; }
    }
}
