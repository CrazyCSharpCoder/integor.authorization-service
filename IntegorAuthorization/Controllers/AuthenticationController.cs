using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using AutoMapper;

using AspErrorHandling;
using AspErrorHandling.Converters;

using PrettyUserAuthorizationResponseDecoration.Attributes;

using PrettyUserAuthorizationModel;

using PrettyUserAuthorizationShared.Dto.Users;
using PrettyUserAuthorizationShared.Services;
using PrettyUserAuthorizationShared.Services.Security;
using PrettyUserAuthorizationShared.Services.Security.Password;

using AdvancedJwtAuthentication.Refresh;

namespace PrettyUserAuthorization.Controllers
{
    using static Constants.RouteNames.AuthRouteNames;

    using Dto.Authentication;

	[ApiController]
	[Route("auth")]
	public class AuthenticationController : ControllerBase
	{
		private IUserValidationService _userValidation;
		private IUsersService _users;

		private IPasswordEncryptionService _passwordEncryption;
		private IPasswordSaltService _saltService;
		private IPasswordValidationService _passwordValidator;
		private ISecurityDataAccessService _securityAccess;

		private IAuthenticationAbstractionService _authentication;

		private IStringErrorConverter _stringConverter;
		private IMapper _mapper;

		public AuthenticationController(
			IUserValidationService userValidation,
			IUsersService users,

			IPasswordEncryptionService passwordEncryption,
			IPasswordSaltService saltService,
			IPasswordValidationService passwordValidator,
			ISecurityDataAccessService securityAccess,

			IAuthenticationAbstractionService authentication,

			IStringErrorConverter stringConverter,
			IMapper mapper)
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

			UserAccountPublicDto user = await _users.AddAsync(addDto);

			await _authentication.LoginAsync(user);

			return Ok(user);
		}

		[DecorateUserResponse]
		[HttpPost("login", Name = LoginRoute)]
		public async Task<IActionResult> LoginAsync(LoginUserDto dto)
		{
			UserAccountPublicDto? user = await _users.GetByEmailAsync(dto.Email);

			if (user == null)
				return WrongCredenrialsProvided();

			SecurityData securityData = (await _securityAccess.GetSecurityDataAsync(user.Id))!;

			string passwordSalt = securityData.PasswordSalt;
			string saltedPassword = _saltService.AppendSalt(dto.Password, passwordSalt);
			string passwordHash = _passwordEncryption.Encrypt(saltedPassword);

			bool passwordValid = await _passwordValidator.IsPasswordValidAsync(user.Id, passwordHash);

			if (!passwordValid)
				return WrongCredenrialsProvided();

			await _authentication.LoginAsync(user);

			return Ok(user);
		}

		[DecorateUserResponse]
		[HttpPost("refresh", Name = RefreshRoute)]
		[Authorize(AuthenticationSchemes = JwtRefreshAuthenticationDefaults.AuthenticationScheme)]
		public async Task<IActionResult> RefreshAsync()
		{
			UserAccountPublicDto user = await _authentication.GetAuthenticatedUserAsync();
			await _authentication.LoginAsync(user);

			return Ok(user);
		}

		private IActionResult WrongCredenrialsProvided()
		{
			string errorMessage = "Wrong credentials provided";
			IErrorConvertationResult error = _stringConverter.Convert(errorMessage)!;

			return BadRequest(error);
		}
	}
}
