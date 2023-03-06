using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using System.Security.Claims;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Microsoft.IdentityModel.Tokens;

using Microsoft.AspNetCore.Authentication;

namespace AdvancedJwtAuthentication.Refresh
{
	using Services;

	public class JwtRefreshAuthenticationHandler : AuthenticationHandler<JwtRefreshAuthenticationOptions>, IAuthenticationHandler
	{
		private IHttpContextTokensAccessor _httpTokens;
		private IResolveTokensService _tokenService;

		public JwtRefreshAuthenticationHandler(
			IOptionsMonitor<JwtRefreshAuthenticationOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder,
			ISystemClock clock,

			IHttpContextTokensAccessor httpTokens,
			IResolveTokensService tokenService) : base(options, logger, encoder, clock)
		{
			_httpTokens = httpTokens;
			_tokenService = tokenService;
		}

		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			string? token;

			try
			{
				token = _httpTokens.GetRefreshToken();
			}
			catch
			{
				return FailDefault();
			}

			if (token == null)
				return FailDefault();

			List<Claim> claims;

			try
			{
				claims = (await _tokenService.ReadAccessTokenAsync(token)).ToList();
			}
			catch (SecurityTokenExpiredException)
			{
				return AuthenticateResult.Fail("Session has expired");
			}
			catch
			{
				return FailDefault();
			}

			claims.Add(new Claim(Options.RefreshRequiredClaimType, true.ToString()));

			ClaimsIdentity identity = new ClaimsIdentity(claims, "AdvancedJwtRefreshAuthentication");
			ClaimsPrincipal principal = new ClaimsPrincipal(identity);

			AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

			return AuthenticateResult.Success(ticket);
		}

		private AuthenticateResult FailDefault() => AuthenticateResult.Fail("Unauthorized");
	}
}
