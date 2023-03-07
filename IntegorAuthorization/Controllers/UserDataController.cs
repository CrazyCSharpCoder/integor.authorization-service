using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using AspErrorHandling;
using AspErrorHandling.Converters;

using PrettyUserAuthorizationResponseDecoration.Attributes;

using PrettyUserAuthorizationShared.Dto.Users;
using PrettyUserAuthorizationShared.Services;

namespace PrettyUserAuthorization.Controllers
{
	using static Constants.RouteNames.UserDataRouteNames;

	[ApiController]
	[Route("users")]
	public class UserDataController : ControllerBase
	{
		private IAuthenticationAbstractionService _authentication;
		private IUsersService _users;
		private IStringErrorConverter _stringErrorConverter;

		public UserDataController(
			IAuthenticationAbstractionService authentication,
			IUsersService users,
			IStringErrorConverter stringErrorConverter)
		{
			_authentication = authentication;
			_users = users;
			_stringErrorConverter = stringErrorConverter;
		}

		[Authorize]
		[DecorateUserResponse]
		[HttpGet("me", Name = GetAccountRoute)]
		public async Task<IActionResult> GetMeAsync()
		{
			UserAccountPublicDto user = await _authentication.GetAuthenticatedUserAsync();
			return Ok(user);
		}

		[DecorateUserResponse]
		[HttpGet("by-id/{id}", Name = GetByIdRoute)]
		public async Task<IActionResult> GetUserByIdAsync(int id)
		{
			UserAccountPublicDto? user =  await _users.GetByIdAsync(id);

			if (user != null)
				return Ok(user);

			string errorMessage = "User with specified id does not exist";
			IErrorConvertationResult error = _stringErrorConverter.Convert(errorMessage)!;

			return NotFound(error);
		}

		[DecorateUserResponse]
		[HttpGet("by-email/{email}", Name = GetByEmailRoute)]
		public async Task<IActionResult> GetUserByEmailAsync([System.ComponentModel.DataAnnotations.EmailAddress] string email)
		{
			UserAccountPublicDto? user = await _users.GetByEmailAsync(email);

			if (user != null)
				return Ok(user);

			string errorMessage = "User with specified email does not exist";
			IErrorConvertationResult error = _stringErrorConverter.Convert(errorMessage)!;

			return NotFound(error);
		}
	}
}
