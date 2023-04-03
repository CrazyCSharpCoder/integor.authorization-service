using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using ExtensibleRefreshJwtAuthentication.Access.Tokens;
using ExtensibleRefreshJwtAuthentication.Refresh.Tokens;

using IntegorPublicDto.Authorization.Users;
using IntegorAspHelpers.MicroservicesInteraction.Authorization;

using IntegorAuthorizationShared.Dto.Users;
using IntegorAuthorizationShared.Services;

namespace IntegorAuthorizationAspServices
{
	public class AuthenticationAbstractionService : IAuthenticationAbstractionService
	{
		private HttpContext _http;

		private IProcessRequestAccessTokenAccessor _accessTokenAccess;
		private IProcessRequestRefreshTokenAccessor _refreshTokenAccess;

		private IAccessTokenResolver _accessResolver;
		private IRefreshTokenResolver _refreshResolver;

		private IUserClaimsParser _claimsParser;
		private IUsersService _users;

		public AuthenticationAbstractionService(
			IHttpContextAccessor httpAccessor,

			IProcessRequestAccessTokenAccessor accessTokenAccess,
			IProcessRequestRefreshTokenAccessor refreshTokenAccess,

			IAccessTokenResolver accessResolver,
			IRefreshTokenResolver refreshResolver,
			IUserClaimsParser claimsParser,

			IUsersService users)
		{
			_http = httpAccessor.HttpContext;

			_accessTokenAccess = accessTokenAccess;
			_refreshTokenAccess = refreshTokenAccess;

			_accessResolver = accessResolver;
			_refreshResolver = refreshResolver;
			_claimsParser = claimsParser;

			_users = users;
		}

		public async Task LoginAsync(UserAccountInfoDto user)
		{
			_claimsParser.UserToClaims(user);

			Claim[] claims = _claimsParser.UserToClaims(user);

			string accessToken = await _accessResolver.GenerateTokenAsync(claims);
			string refreshToken = await _refreshResolver.GenerateTokenAsync(claims);

			_accessTokenAccess.AttachToResponse(accessToken);
			_refreshTokenAccess.AttachToResponse(refreshToken);
		}

		public async Task LogoutAsync()
		{
			// TODO implement logout

			//_httpTokens.DeleteAccessToken();
			//_httpTokens.DeleteRefreshToken();
		}

		public async Task<UserAccountDto> GetAuthenticatedUserAsync()
		{
			ClaimsPrincipal principal = _http.User;

			// TODO understand why claim.Type != ClaimsIdentity.DefaultNameClaimType and how to parse URI of a claim type
			// Claim? usernameClaim = principal.Claims.FirstOrDefault(claim => claim.Type == ClaimsIdentity.DefaultNameClaimType);

			string usernameClaimName = _claimsParser.GetClaimNames().UsernameClaimName;
			Claim? usernameClaim = principal.Claims.FirstOrDefault(claim => claim.Type == usernameClaimName);

			if (usernameClaim == null)
				// TODO to think about a better exception message
				throw new InvalidOperationException("User is not authenticated");

			UserAccountDto? user = await _users.GetByEmailAsync(usernameClaim.Value);

			if (user == null)
				// TODO to think about a better exception message
				throw new InvalidOperationException("User data is deprecated. Cannot authorize");

			return user;
		}
	}
}
