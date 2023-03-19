﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using IntegorAuthorizationGlobalConstants;

using IntegorAuthorizationAspShared.ConfigurationProviders;

using IntegorAuthorizationShared.Dto.Users;
using IntegorAuthorizationShared.Services;

using AdvancedJwtAuthentication.Services;

namespace IntegorAuthorizationAspServices
{
	public class AuthenticationAbstractionService : IAuthenticationAbstractionService
	{
		private HttpContext _http;

		private IHttpContextTokensAccessor _httpTokens;
		private IResolveTokensService _tokenService;

		private IUsersService _users;

		public AuthenticationAbstractionService(
			IHttpContextAccessor httpAccessor,
			IHttpContextTokensAccessor httpTokens,
			IResolveTokensService tokenService,

			IUsersService users)
		{
			_http = httpAccessor.HttpContext;

			_httpTokens = httpTokens;
			_tokenService = tokenService;

			_users = users;
		}

		public async Task LoginAsync(UserAccountDto user)
		{
			Claim[] claims = new Claim[]
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.EMail),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Id.ToString())
			};

			string accessToken = await _tokenService.GenerateAccessTokenAsync(claims);
			string refreshToken = await _tokenService.GenerateRefreshTokenAsync(claims);

			_httpTokens.PutAccessToken(accessToken);
			_httpTokens.PutRefreshToken(refreshToken);
		}

		public async Task LogoutAsync()
		{
			_httpTokens.DeleteAccessToken();
			_httpTokens.DeleteRefreshToken();
		}

		public async Task<bool> RefreshRequiredAsync()
		{
			Claim? refreshClaim = _http.User.Claims.FirstOrDefault(
				claim => claim.Type == AuthenticationConstants.RefreshRequiredClaimType);

			return refreshClaim != null && refreshClaim.Value == true.ToString();
		}

		public async Task<UserAccountDto> GetAuthenticatedUserAsync()
		{
			ClaimsPrincipal principal = _http.User;

			// TODO understand why claim.Type != ClaimsIdentity.DefaultNameClaimType and how to parse URI of a claim type
			// Claim? usernameClaim = principal.Claims.FirstOrDefault(claim => claim.Type == ClaimsIdentity.DefaultNameClaimType);

			Claim? usernameClaim = principal.Claims.FirstOrDefault(claim => claim.Type == "unique_name");

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
