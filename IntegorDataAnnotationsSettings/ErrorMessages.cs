namespace PrettyUserDataAnnotationsSettings
{
	public static class ErrorMessages
	{
		public const string MaxStringLengthErrorMessage = "Maximum allowed length of the \"{0}\" field is {1} characters";
		public const string MinStringLengthErrorMessage = "Minimum allowed length of the \"{0}\" field is {1} characters";
		public const string StringLengthErrorMessage = "Length of the \"{0}\" field must be between {2} and {1} characters";

		public const string RequiredErrorMessage = "The \"{0}\" field is a required value";
		public const string CompareErrorMessage = "The \"{0}\" field must match the \"{1}\" field";

		public const string EmailAddressErrorMessage = "The \"{0}\" field is an incorrect e-mail address";
	}
}