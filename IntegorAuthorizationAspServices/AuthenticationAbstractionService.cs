using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Claims;

using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

using ExtensibleRefreshJwtAuthentication.Access;
using ExtensibleRefreshJwtAuthentication.Refresh;

using IntegorPublicDto.Authorization.Users;
using IntegorAspHelpers.MicroservicesInteraction.Authorization.Claims;

using IntegorAuthorizationShared.Dto.Users;
using IntegorAuthorizationShared.Services;
using ExtensibleRefreshJwtAuthentication;

namespace IntegorAuthorizationAspServices
{
	public class AuthenticationAbstractionService : IAuthenticationAbstractionService
	{
		private HttpContext _http;

		private IOnServiceProcessingAccessTokenAccessor _accessTokenAccessor;
		private IOnServiceProcessingRefreshTokenAccessor _refreshTokenAccessor;

		private IAccessTokenResolver _accessResolver;
		private IRefreshTokenResolver _refreshResolver;
		private IUserClaimsParser _claimsParser;

		private IUsersService _users;

		private ClaimTypeNames _claimTypes;

		public AuthenticationAbstractionService(
			IHttpContextAccessor httpAccessor,

			IOnServiceProcessingAccessTokenAccessor accessTokenAccessor,
			IOnServiceProcessingRefreshTokenAccessor refreshTokenAccessor,

			IAccessTokenResolver accessResolver,
			IRefreshTokenResolver refreshResolver,
			IUserClaimsParser claimsParser,

			IUsersService users,

			IOptions<ClaimTypeNames> claimTypesOptions)
		{
			_http = httpAccessor.HttpContext!;

			_accessTokenAccessor = accessTokenAccessor;
			_refreshTokenAccessor = refreshTokenAccessor;

			_accessResolver = accessResolver;
			_refreshResolver = refreshResolver;
			_claimsParser = claimsParser;

			_users = users;

			_claimTypes = claimTypesOptions.Value;
		}

		public async Task LoginAsync(UserAccountInfoDto user)
		{
			_claimsParser.UserToClaims(user);

			Claim[] claims = _claimsParser.UserToClaims(user);

			string accessToken = await _accessResolver.GenerateTokenAsync(claims);
			string refreshToken = await _refreshResolver.GenerateTokenAsync(claims);

			_accessTokenAccessor.AttachToResponse(accessToken);
			_refreshTokenAccessor.AttachToResponse(refreshToken);
		}

		public async Task LogoutAsync()
		{
			_accessTokenAccessor.DeleteFromResponse();
			_refreshTokenAccessor.DeleteFromResponse();
		}

		public async Task<UserAccountDto> GetAuthenticatedUserAsync()
		{
			ClaimsPrincipal principal = _http.User;
			Claim? usernameClaim = principal.Claims.FirstOrDefault(claim => claim.Type == _claimTypes.UsernameClaimType);

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
