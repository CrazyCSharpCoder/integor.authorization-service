using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using IntegorAuthorizationModel;

namespace IntegorAuthorizationServices.DatabaseContext
{
	public class IntegorAuthorizationDbContext : DbContext
	{
		public DbSet<UserAccount> Users { get; set; } = null!;
		public DbSet<UserRole> UserRoles { get; set; } = null!;
		public DbSet<SecurityData> SecurityData { get; set; } = null!;

		public IntegorAuthorizationDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserAccount>(user =>
			{
				user.HasOne<UserRole>().WithMany().HasForeignKey(user => user.RoleId);
				user.HasIndex(user => user.Email).IsUnique();
			});

			modelBuilder.Entity<UserRole>(role =>
			{
				role.HasIndex(role => role.Title).IsUnique();
			});

			modelBuilder.Entity<SecurityData>(security =>
			{
				security.HasOne<UserAccount>().WithOne().HasForeignKey<SecurityData>(security => security.UserId);
			});
		}
	}
}
