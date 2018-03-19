using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace School_AppIn_Model.DataAccessLayer
{
	[Table("Specialization")]
	public partial class Specialization
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Specialization_Id { get; set; }

		[StringLength(50)]
		public string Specialization_Name { get; set; }
	}
}
