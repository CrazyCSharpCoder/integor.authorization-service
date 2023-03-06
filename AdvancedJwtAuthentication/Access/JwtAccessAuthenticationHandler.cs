using System.Collections.Generic;
using System.Threading.Tasks;

using System.Text.Encodings.Web;

using System.Security.Claims;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Microsoft.IdentityModel.Tokens;

using Microsoft.AspNetCore.Authentication;

namespace AdvancedJwtAuthentication.Access
{
    using Services;

	public class JwtAccessAuthenticationHandler : AuthenticationHandler<JwtAccessAuthenticationOptions>
    {
        private IHttpContextTokensAccessor _httpTokens;
        private IResolveTokensService _tokenService;

        public JwtAccessAuthenticationHandler(
            IOptionsMonitor<JwtAccessAuthenticationOptions> options,
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
                token = _httpTokens.GetAccessToken();
            }
            catch
            {
                return FailDefault();
            }

            if (token == null)
                return FailDefault();

			IEnumerable<Claim> claims;

            try
            {
				claims = await _tokenService.ReadAccessTokenAsync(token);
            }
            catch (SecurityTokenExpiredException)
			{
				return AuthenticateResult.Fail("Access has expired");
			}
			catch
            {
                return FailDefault();
            }

            ClaimsIdentity identity = new ClaimsIdentity(claims, "AdvancedJwtAccessAuthentication");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

        private AuthenticateResult FailDefault() => AuthenticateResult.Fail("Unauthorized");
    }
}
