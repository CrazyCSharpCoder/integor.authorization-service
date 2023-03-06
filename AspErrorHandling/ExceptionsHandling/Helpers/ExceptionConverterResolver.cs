using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AspErrorHandling.ExceptionsHandling.Helpers
{
	using static ExceptionConverterReflector;

	using Types;

	public static class ExceptionConverterResolver
    {
		public static IEnumerable<ExceptionConverterToExceptionType> LookExceptionConvertersHierarchy(IEnumerable<object> converters, Type exceptionType)
		{
			Type? excSearchType = exceptionType;

			while (excSearchType != null)
			{
				ExceptionConverterToExceptionType? converterToExc = converters
					.Select(converter => new ExceptionConverterToExceptionType(converter, GetConverterExceptionType(converter.GetType())!))
					.FirstOrDefault(converterToExc => converterToExc.ExceptionType == excSearchType);

				if (converterToExc != null)
					yield return converterToExc;

				excSearchType = excSearchType.BaseType;
			}
		}

		public static IEnumerable<Type> LookExceptionConverterTypesHierarchy(IEnumerable<Type> converterTypes, Type exceptionType)
		{
			Type? excSearchType = exceptionType;

			while (excSearchType != null)
			{
				Type? converterType = converterTypes.FirstOrDefault(type => GetConverterExceptionType(type) == excSearchType);

				if (converterType != null)
					yield return converterType;

				excSearchType = excSearchType.BaseType;
			}
		}

		public static IErrorConvertationResult? Convert(object converter, Exception exception)
		{
			MethodInfo convertMethod = converter.GetType().GetMethod("Convert")!;
			return (IErrorConvertationResult?)convertMethod.Invoke(converter, new object[] { exception })!;
		}
	}
}
