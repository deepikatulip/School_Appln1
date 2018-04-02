using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace School_AppIn_Model.DataAccessLayer
{
	[Table("Exam_Configuration")]
	public partial class Exam_Configuration
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Exam_Id { get; set; }
		
		public string Exam_Name { get; set; }
		
		public long Academic_Year { get; set; }
	
		public bool Is_Active { get; set; }

		[Display(Name = "Created By")]
		public string Created_By { get; set; }

		[Display(Name = "Updated Date")]
		public DateTime? Updated_On { get; set; }

		[Display(Name = "Updated By")]
		public string Updated_By { get; set; }

		public DateTime? Created_On { get; set; }

		public bool? Is_Deleted { get; set; }


	}
}
