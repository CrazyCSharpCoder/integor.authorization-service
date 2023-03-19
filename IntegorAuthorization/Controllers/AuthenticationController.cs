using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using IntegorPublicDto.Authorization.Users.Input;

using AutoMapper;

using IntegorErrorsHandling;
using IntegorErrorsHandling.Converters;

using IntegorSharedResponseDecorators.Attributes.Authorization;

using IntegorAuthorizationResponseDecoration.Attributes;

using IntegorAuthorizationModel;

using IntegorAuthorizationShared.Dto.Users;
using IntegorAuthorizationShared.Services;
using IntegorAuthorizationShared.Services.Security;
using IntegorAuthorizationShared.Services.Security.Password;

using AdvancedJwtAuthentication.Refresh;

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
			await _authentication.LoginAsync(user);

			return Ok(user);
		}

		[DecorateUserResponse]
		[DecorateUserToPublicDto]
		[HttpPost("login", Name = LoginRoute)]
		public async Task<IActionResult> LoginAsync(LoginUserDto dto)
		{
			UserAccountDto? user = await _users.GetByEmailAsync(dto.Email);

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
		[DecorateUserToPublicDto]
		[HttpPost("refresh", Name = RefreshRoute)]
		[Authorize(AuthenticationSchemes = JwtRefreshAuthenticationDefaults.AuthenticationScheme)]
		public async Task<IActionResult> RefreshAsync()
		{
			UserAccountDto user = await _authentication.GetAuthenticatedUserAsync();
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
