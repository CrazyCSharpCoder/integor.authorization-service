using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static IntegorAuthorizationModelOptions.Security.SecurityDatabaseOptions;

namespace IntegorAuthorizationModel
{
    public class SecurityData
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int UserId { get; set; }

		[MaxLength(PasswordSaltLength)]
		public string PasswordSalt { get; set; } = null!;
	}
}
