using System;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.Data
{
	public class ManagementDbContext: DbContext
	{
		public ManagementDbContext(DbContextOptions<ManagementDbContext> options): base(options)
		{

		}
		 public DbSet<User> Users { get; set; }
	}
}

