using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace School_AppIn_Model.DataAccessLayer
{
	[Table("Qualification")]
	public partial class Qualification
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Qualification_Id { get; set; }

		//[StringLength(200)]
		public string Qualification_Name { get; set; }
	}
}
