using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PrettyUserAuthorizationGlobalConstants;

namespace AdvancedJwtAuthentication.Refresh
{
	public static class JwtRefreshAuthenticationDefaults
	{
		public const string AuthenticationScheme = "JwtRefreshAuthentication";
		public const string RefreshRequiredClaimType = AuthenticationConstants.RefreshRequiredClaimType;
	}
}
