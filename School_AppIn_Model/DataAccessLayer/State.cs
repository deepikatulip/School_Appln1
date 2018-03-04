namespace School_AppIn_Model.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("State")]
    public partial class State
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public int Country_Id { get; set; }
    
    }
}
