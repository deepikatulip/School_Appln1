namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Country")]
    public partial class Country
    { 
    
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
