using System;
using System.Net.Sockets;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Microsoft.EntityFrameworkCore;
using Npgsql;

using IntegorErrorsHandling;
using IntegorErrorsHandling.Converters;
using IntegorErrorsHandling.DefaultImplementations;

using IntegorSharedErrorHandlers;
using IntegorSharedErrorHandlers.Converters;

using IntegorSharedErrorHandlers.ExceptionConverters;
using IntegorSharedErrorHandlers.ExceptionConverters.DataAccess;

namespace IntegorAuthorization.StartupServices
{
	public static class ErrorConvertersServicesExtensions
	{
		public static void AddTypesErrorConverters(this IServiceCollection services)
		{
			services.AddSingleton<IStringErrorConverter, StandardStringErrorConverter>();
			services.AddSingleton<IResponseErrorObjectCompiler, StandardResponseErrorObjectCompiler>();
			services.AddSingleton<IErrorConverter<ModelStateDictionary>, ModelStateDictionaryErrorConverter>();

			services.AddSingleton<StatusCodeErrorConverter>();
		}

		public static void AddStandardExceptionConverters(this IServiceCollection services)
		{
			services.AddSingleton<IExceptionErrorConverter<Exception>, StandardExceptionErrorConverter>();

			services.AddSingleton<IExceptionErrorConverter<InvalidOperationException>, InvalidOperationExceptionConverter>();
			services.AddSingleton<IExceptionErrorConverter<ObjectDisposedException>, ObjectDisposedExceptionConverter>();
		}

		public static void AddDatabaseExceptionConverters(this IServiceCollection services)
		{
			services.AddSingleton<IExceptionErrorConverter<PostgresException>, PostgresExceptionConverter>();
			services.AddSingleton<IExceptionErrorConverter<SocketException>, SocketExceptionConverter>();
			services.AddSingleton<IExceptionErrorConverter<DbUpdateException>, DbUpdateExceptionConverter>();
		}
	}
}
