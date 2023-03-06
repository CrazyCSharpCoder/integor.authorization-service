using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;

namespace AdvancedJwtAuthentication.Refresh
{
	public class JwtRefreshAuthenticationOptions : AuthenticationSchemeOptions
	{
		public string RefreshRequiredClaimType { get; set; } = JwtRefreshAuthenticationDefaults.RefreshRequiredClaimType;
	}
}
