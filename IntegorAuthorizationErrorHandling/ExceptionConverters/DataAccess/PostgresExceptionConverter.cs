using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AspErrorHandling;
using AspErrorHandling.Converters;

using Npgsql;

namespace IntegorAuthorizationErrorHandling.ExceptionConverters.DataAccess
{
	using static Internal.DatabaseErrorsConstants;

	public class PostgresExceptionConverter : ConverterBase, IExceptionErrorConverter<PostgresException>
	{
		private static Dictionary<string[], Func<PostgresException, IErrorConvertationResult>> _errorCodesToErrors =
			new Dictionary<string[], Func<PostgresException, IErrorConvertationResult>>()
			{
				{
					new string[] { PostgresErrorCodes.AdminShutdown, PostgresErrorCodes.CrashShutdown },
						exc => Single(InternalDatabaseErrorMessage)
				}
			};

		public IErrorConvertationResult? Convert(PostgresException exception)
		{
			Func<PostgresException, IErrorConvertationResult>? handler = GetExceptionHandler(exception.SqlState);

			if (handler == null)
				return Single(exception.MessageText);

			return handler(exception);
		}

		private Func<PostgresException, IErrorConvertationResult>? GetExceptionHandler(string errorCode)
		{
			KeyValuePair<string[], Func<PostgresException, IErrorConvertationResult>> errorsToHandler;

			try
			{
				errorsToHandler = _errorCodesToErrors.First(pair => pair.Key.Contains(errorCode));
			}
			catch (InvalidOperationException)
			{
				return null;
			}

			return errorsToHandler.Value;
		}
	}
}
