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
			services.AddScoped<IErrorConverter<ModelStateDictionary>, ModelStateDictionaryErrorConverter>();
		}

		public static void AddStandartExceptionConverters(this IServiceCollection services)
		{
			services.AddScoped<IExceptionErrorConverter<Exception>, StandardExceptionErrorConverter>();

			services.AddScoped<IExceptionErrorConverter<InvalidOperationException>, InvalidOperationExceptionConverter>();
			services.AddScoped<IExceptionErrorConverter<ObjectDisposedException>, ObjectDisposedExceptionConverter>();
		}

		public static void AddDatabaseExceptionConverters(this IServiceCollection services)
		{
			services.AddScoped<IExceptionErrorConverter<PostgresException>, PostgresExceptionConverter>();
			services.AddScoped<IExceptionErrorConverter<SocketException>, SocketExceptionConverter>();
			services.AddScoped<IExceptionErrorConverter<DbUpdateException>, DbUpdateExceptionConverter>();
		}
	}
}
