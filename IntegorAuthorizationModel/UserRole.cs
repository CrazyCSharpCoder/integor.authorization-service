using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static IntegorGlobalConstants.ModelOptions.Authorization.UserRoleOptions;

namespace IntegorAuthorizationModel
{
	public class UserRole
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }

		[Required]
		[MaxLength(TitleLength)]
		public string Title { get; set; } = null!;

		public string Description { get; set; } = null!;
	}
}
