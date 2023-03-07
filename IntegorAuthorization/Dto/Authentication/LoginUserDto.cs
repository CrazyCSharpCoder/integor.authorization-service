using System.ComponentModel.DataAnnotations;

using static PrettyUserAuthorizationModelOptions.UserAccountOptions;
using static PrettyUserDataAnnotationsSettings.ErrorMessages;

namespace PrettyUserAuthorization.Dto.Authentication
{
	using static Constants.DtoValidation.UserValidationConstants;

	public class LoginUserDto
    {
		[Required(ErrorMessage = RequiredErrorMessage)]
        [EmailAddress(ErrorMessage = EmailAddressErrorMessage)]
		[MaxLength(EMailLength, ErrorMessage = MaxStringLengthErrorMessage)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = RequiredErrorMessage)]
		[RegularExpression(PasswordValidatonRegex, ErrorMessage = IncorrectPasswordErrorMessage)]
		public string Password { get; set; } = null!;
    }
}
