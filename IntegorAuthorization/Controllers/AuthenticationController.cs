using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using IntegorPublicDto.Authorization.Users.Input;

using AutoMapper;

using IntegorErrorsHandling;
using IntegorErrorsHandling.Converters;

using IntegorSharedResponseDecorators.Authorization.Attributes;

using IntegorPublicDto.Authorization.Users;

using ExtensibleRefreshJwtAuthentication.Access;
using ExtensibleRefreshJwtAuthentication.Refresh;

using IntegorAuthorizationModel;

using IntegorAuthorizationShared.Dto.Users;
using IntegorAuthorizationShared.Services;
using IntegorAuthorizationShared.Services.Security;
using IntegorAuthorizationShared.Services.Security.Password;

using IntegorAuthorizationResponseDecoration.Attributes;

namespace IntegorAuthorization.Controllers
{
    using static Constants.RouteNames.AuthRouteNames;

	[ApiController]
	[Route("auth")]
	public class AuthenticationController : ControllerBase
	{
		private IMapper _mapper;
		private IStringErrorConverter _stringConverter;

		private IUserValidationService _userValidation;
		private IUsersService _users;

		private IPasswordEncryptionService _passwordEncryption;
		private IPasswordSaltService _saltService;
		private IPasswordValidationService _passwordValidator;
		private ISecurityDataAccessService _securityAccess;

		private IAuthenticationAbstractionService _authentication;

		public AuthenticationController(
			IMapper mapper,
			IStringErrorConverter stringConverter,

			IUserValidationService userValidation,
			IUsersService users,

			IPasswordEncryptionService passwordEncryption,
			IPasswordSaltService saltService,
			IPasswordValidationService passwordValidator,
			ISecurityDataAccessService securityAccess,

			IAuthenticationAbstractionService authentication)
		{
			_userValidation = userValidation;
			_users = users;

			_saltService = saltService;
			_passwordEncryption = passwordEncryption;
			_passwordValidator = passwordValidator;
			_securityAccess = securityAccess;

			_authentication = authentication;

			_stringConverter = stringConverter;
			_mapper = mapper;
		}

		[DecorateUserResponse]
		[DecorateUserToPublicDto]
		[HttpPost("register", Name = RegisterRoute)]
		public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto dto)
		{
			if (await _userValidation.EMailExistsAsync(dto.EMail))
			{
				string errorMessage = "User with this email already exists";
				IErrorConvertationResult error = _stringConverter.Convert(errorMessage)!;

				return BadRequest(error);
			}

			string passwordSalt = _saltService.GenerateSalt();
			string saltedPassword = _saltService.AppendSalt(dto.Password, passwordSalt);

			AddUserAccountDto addDto = _mapper.Map<AddUserAccountDto>(dto);

			addDto.PasswordHash = _passwordEncryption.Encrypt(saltedPassword);
			addDto.PasswordSalt = passwordSalt;

			UserAccountDto user = await _users.AddAsync(addDto);
			return Ok(await LoginAsPublicAsync(user));
		}

		[DecorateUserResponse]
		[DecorateUserToPublicDto]
		[HttpPost("login", Name = LoginRoute)]
		public async Task<IActionResult> LoginAsync([FromBody] LoginUserDto dto)
		{
			UserAccountDto? user = await _users.GetByEmailAsync(dto.EMail);

			if (user == null)
				return WrongCredenrialsProvided();

			SecurityData securityData = (await _securityAccess.GetSecurityDataAsync(user.Id))!;

			string passwordSalt = securityData.PasswordSalt;
			string saltedPassword = _saltService.AppendSalt(dto.Password, passwordSalt);
			string passwordHash = _passwordEncryption.Encrypt(saltedPassword);

			bool passwordValid = await _passwordValidator.IsPasswordValidAsync(user.Id, passwordHash);

			if (!passwordValid)
				return WrongCredenrialsProvided();

			return Ok(await LoginAsPublicAsync(user));
		}

		[Authorize]
		[DecorateUserResponse]
		[DecorateUserToPublicDto]
		[HttpGet("me", Name = GetAccountRoute)]
		public async Task<IActionResult> GetMeAsync()
		{
			UserAccountDto user = await _authentication.GetAuthenticatedUserAsync();
			return Ok(user);
		}

		[DecorateUserResponse]
		[DecorateUserToPublicDto]
		[HttpPost("refresh", Name = RefreshRoute)]
		[Authorize(AuthenticationSchemes = RefreshTokenAuthenticationDefaults.AuthenticationScheme)]
		public async Task<IActionResult> RefreshAsync()
		{
			UserAccountDto user = await _authentication.GetAuthenticatedUserAsync();
			return Ok(await LoginAsPublicAsync(user));
		}

		[DecorateUserResponse]
		[DecorateUserToPublicDto]
		[HttpPost("logout", Name = LogoutRoute)]
		[Authorize(AuthenticationSchemes =
			$"{AccessTokenAuthenticationDefaults.AuthenticationScheme}," +
			$"{RefreshTokenAuthenticationDefaults.AuthenticationScheme}")]
		public async Task<IActionResult> LogoutAsync()
		{
			UserAccountDto user = await _authentication.GetAuthenticatedUserAsync();
			await _authentication.LogoutAsync();

			return Ok(user);
		}

		private IActionResult WrongCredenrialsProvided()
		{
			string errorMessage = "Wrong credentials provided";
			IErrorConvertationResult error = _stringConverter.Convert(errorMessage)!;

			return BadRequest(error);
		}

		private async Task<UserAccountInfoDto> LoginAsPublicAsync([FromBody] UserAccountDto user)
		{
			UserAccountInfoDto userPublic = _mapper.Map<UserAccountInfoDto>(user);
			await _authentication.LoginAsync(userPublic);

			return userPublic;
		}
	}
}
