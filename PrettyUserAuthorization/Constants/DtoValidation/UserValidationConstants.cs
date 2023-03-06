namespace PrettyUserAuthorization.Constants.DtoValidation
{
	public static class UserValidationConstants
	{
		public const string IncorrectPasswordErrorMessage =
			"The \"{0}\" field can contain only the Latin alphabet characters and digits. " +
			"Additionally it must have at least one digit, one lowercase and one uppercase Latin characters";

		public const string PasswordValidatonRegex = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]+$";
	}
}
