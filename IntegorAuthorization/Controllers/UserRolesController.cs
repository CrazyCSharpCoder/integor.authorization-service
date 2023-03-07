using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using IntegorAuthorizationResponseDecoration.Attributes;

using IntegorAuthorizationModel;
using IntegorAuthorizationShared.Services;

namespace IntegorAuthorization.Controllers
{
	using static Constants.RouteNames.UserRolesRouteNames;

	[ApiController]
	[Route("roles")]
	public class UserRolesController : ControllerBase
	{
		private IUserRolesService _roles;

		public UserRolesController(IUserRolesService roles)
		{
			_roles = roles;
		}

		[DecorateUserRolesCollectionResponse]
		[HttpGet("get-all", Name = GetAllRolesRoute)]
		public async Task<IActionResult> GetAllRolesAsync()
		{
			IEnumerable<UserRole> roles = await _roles.GetAllAsync();
			return Ok(roles);
		}
	}
}
