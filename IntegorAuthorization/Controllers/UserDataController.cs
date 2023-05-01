using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using IntegorErrorsHandling;
using IntegorErrorsHandling.Converters;

using IntegorSharedResponseDecorators.Authorization.Attributes;

using IntegorAuthorizationShared.Dto.Users;
using IntegorAuthorizationShared.Services;

using IntegorAuthorizationResponseDecoration.Attributes;

namespace IntegorAuthorization.Controllers
{
	using static Constants.RouteNames.UserDataRouteNames;

	[ApiController]
	[Route("users")]
	public class UserDataController : ControllerBase
	{
		private IUsersService _users;
		private IStringErrorConverter _stringErrorConverter;

		public UserDataController(
			IUsersService users,
			IStringErrorConverter stringErrorConverter)
		{
			_users = users;
			_stringErrorConverter = stringErrorConverter;
		}

		[DecorateUserResponse]
		[DecorateUserToPublicDto]
		[HttpGet("by-id/{id}", Name = GetByIdRoute)]
		public async Task<IActionResult> GetUserByIdAsync(int id)
		{
			UserAccountDto? user =  await _users.GetByIdAsync(id);

			if (user != null)
				return Ok(user);

			string errorMessage = "User with specified id does not exist";
			IErrorConvertationResult error = _stringErrorConverter.Convert(errorMessage)!;

			return NotFound(error);
		}

		[DecorateUserResponse]
		[DecorateUserToPublicDto]
		[HttpGet("by-email/{email}", Name = GetByEmailRoute)]
		public async Task<IActionResult> GetUserByEmailAsync([System.ComponentModel.DataAnnotations.EmailAddress] string email)
		{
			UserAccountDto? user = await _users.GetByEmailAsync(email);

			if (user != null)
				return Ok(user);

			string errorMessage = "User with specified email does not exist";
			IErrorConvertationResult error = _stringErrorConverter.Convert(errorMessage)!;

			return NotFound(error);
		}
	}
}
