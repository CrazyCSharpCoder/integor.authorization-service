using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.ExceptionsHandling.Helpers
{
	public static class ExceptionConverterHierarchyHandler
	{
		public static async Task<IErrorConvertationResult?> ConvertAsync(Exception exception, params object[] converters)
		{
			IEnumerable<object> hierarchy = ExceptionConverterResolver
				.LookExceptionConvertersHierarchy(converters, exception.GetType());

			foreach(object converter in hierarchy)
			{
				IErrorConvertationResult? errors = ExceptionConverterResolver.Convert(converter, exception);

				if (errors != null)
					return errors;
			}

			return null;
		}
	}
}
