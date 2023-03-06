using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.Helpers
{
	using ConvertationResults;
	using ExtensibleError;

	public static class ConvertResultShortcuts
	{
		public static IErrorConvertationResult Single(IResponseError error)
		{
			return new SingleErrorConvertationResult(error);
		}

		public static IErrorConvertationResult Single(string errorMessage)
		{
			IResponseError error = NewError().AddMessage(errorMessage).Build();
			return new SingleErrorConvertationResult(error);
		}

		public static IErrorConvertationResult Single(IEnumerable<string> errorMessages)
		{
			IResponseError error = NewError().AddMessages(errorMessages).Build();
			return new SingleErrorConvertationResult(error);
		}

		public static IErrorConvertationResult Single(string key, string errorMessage)
		{
			IResponseError error = NewError().AddKey(key).AddMessage(errorMessage).Build();
			return new SingleErrorConvertationResult(error);
		}

		public static IErrorConvertationResult Single(string key, IEnumerable<string> errorMessages)
		{
			IResponseError error = NewError().AddKey(key).AddMessages(errorMessages).Build();
			return new SingleErrorConvertationResult(error);
		}

		public static IErrorConvertationResult Multiple(params IResponseError[] errors)
		{
			return new MultipleErrorConvertationResult(errors);
		}

		public static ErrorsBuilder NewError()
		{
			return new ErrorsBuilder();
		}

		public static ErrorsBuilder NewError(string message)
		{
			return new ErrorsBuilder().AddMessage(message);
		}

		public static ErrorsBuilder NewError(IEnumerable<string> messages)
		{
			return new ErrorsBuilder().AddMessages(messages);
		}

		public static ErrorsBuilder NewError(string key, string message)
		{
			return new ErrorsBuilder().AddKey(key).AddMessage(message);
		}

		public static ErrorsBuilder NewError(string key, IEnumerable<string> messages)
		{
			return new ErrorsBuilder().AddKey(key).AddMessages(messages);
		}
	}
}
