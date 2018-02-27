namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TUser
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
