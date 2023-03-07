using System.ComponentModel.DataAnnotations;

using static IntegorAuthorizationModelOptions.UserAccountOptions;
using static IntegorDataAnnotationsSettings.ErrorMessages;

namespace IntegorAuthorization.Dto.Authentication
{
	using static DtoValidation.Authentication.LoginUserOptions;
	using static Constants.DtoValidation.UserValidationConstants;

	public class RegisterUserDto
    {
		[Required(ErrorMessage = RequiredErrorMessage)]
		[EmailAddress(ErrorMessage = EmailAddressErrorMessage)]
		[MaxLength(EMailLength, ErrorMessage = MaxStringLengthErrorMessage)]
		public string EMail { get; set; } = null!;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(PasswordHashLength, MinimumLength = MinPasswordLength, ErrorMessage = StringLengthErrorMessage)]
		[RegularExpression(PasswordValidatonRegex, ErrorMessage = IncorrectPasswordErrorMessage)]
		public string Password { get; set; } = null!;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[Compare(nameof(Password), ErrorMessage = CompareErrorMessage)]
        public string PasswordRepeat { get; set; } = null!;
	}
}
