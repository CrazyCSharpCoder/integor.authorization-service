using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

using PrettyUserAuthorizationModelOptions;

namespace PrettyUserAuthorizationModel
{
	public class UserAccount
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[EmailAddress]
		[MaxLength(UserAccountOptions.EMailLength)]
		public string Email { get; set; } = null!;

		[Required]
		[MaxLength(UserAccountOptions.PasswordHashLength)]
		public string PasswordHash { get; set; } = null!;
		public DateTime PasswordUpdatedDate { get; set; } = DateTime.UtcNow;

		public bool IsActive { get; set; } = true;
		public DateTime? IsActiveUpdatedDate { get; set; }

		public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

		public int RoleId { get; set; }
		public DateTime RoleUpdatedDate { get; set; } = DateTime.UtcNow;
	}
}
