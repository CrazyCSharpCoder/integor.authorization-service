using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AspErrorHandling.ExceptionsHandling.Helpers
{
	using Exceptions;
	using Converters;

    public static class ExceptionConverterReflector
    {
        public static Type? GetExceptionConverterInterface(Type converterType)
        {
			if (IsExceptionConverterInterface(converterType))
				return converterType;

			Type? converterInterface = TypeExtensions.GetInterfaces(converterType)
				.FirstOrDefault(converterInterface => IsExceptionConverterInterface(converterInterface));

			return converterInterface;
        }

		public static Type GetRequiredExceptionConverterInterface(Type converterType)
		{
			Type? converterInterface = GetExceptionConverterInterface(converterType);

			if (converterInterface == null)
				throw NewInvalidConverterException(converterType);

			return converterInterface;
		}

		public static bool IsExceptionConverterInterface(Type type)
		{
			return type.IsGenericType ? type.GetGenericTypeDefinition() == typeof(IExceptionErrorConverter<>) : false;
		}

        public static Type? GetConverterInterfaceExceptionType(Type converterInterface)
        {
			if (!IsExceptionConverterInterface(converterInterface))
				throw NewInvalidConverterException(converterInterface);

			return converterInterface.IsGenericTypeDefinition ? null : converterInterface.GetGenericArguments().First();
        }

        public static Type? GetConverterExceptionType(Type converterType)
        {
            Type converterInterface = GetExceptionConverterInterface(converterType)!;
            return GetConverterInterfaceExceptionType(converterInterface);
        }

		private static Exception NewInvalidConverterException(Type invalidConverterType)
			=> new InvalidConverterException(
				$"Argument of type \"{invalidConverterType.FullName}\" is not converter type");
	}
}
