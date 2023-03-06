using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.Converters
{
	using ExtensibleError;
	using Helpers;

	public abstract class ConverterBase
	{
		protected static IErrorConvertationResult Single(IResponseError error) => ConvertResultShortcuts.Single(error);

		protected static IErrorConvertationResult Single(string errorMessage) => ConvertResultShortcuts.Single(errorMessage);
		protected static IErrorConvertationResult Single(IEnumerable<string> errorMessages) => ConvertResultShortcuts.Single(errorMessages);

		protected static IErrorConvertationResult Single(string key, string errorMessage) => ConvertResultShortcuts.Single(key, errorMessage);
		protected static IErrorConvertationResult Single(string key, IEnumerable<string> errorMessages) => ConvertResultShortcuts.Single(key, errorMessages);

		protected static IErrorConvertationResult Multiple(params IResponseError[] errors) => ConvertResultShortcuts.Multiple(errors);

		public static ErrorsBuilder NewError() => ConvertResultShortcuts.NewError();

		public static ErrorsBuilder NewError(string message) => ConvertResultShortcuts.NewError(message);
		public static ErrorsBuilder NewError(IEnumerable<string> messages) => ConvertResultShortcuts.NewError(messages);

		public static ErrorsBuilder NewError(string key, string message) => ConvertResultShortcuts.NewError(key, message);
		public static ErrorsBuilder NewError(string key, IEnumerable<string> messages) => ConvertResultShortcuts.NewError(key, messages);
	}
}
