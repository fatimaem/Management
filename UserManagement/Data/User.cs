using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Data
{
	[Table("User")]
	public class User
	{
		[Key]	
		public int Id { get; set; }
		public string UserName { get; set; }
		public string LastName { get; set; }
		public string City { get; set; }
		
	}
}

