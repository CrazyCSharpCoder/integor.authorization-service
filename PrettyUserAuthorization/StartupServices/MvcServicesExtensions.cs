using System;
using System.Net.Sockets;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Microsoft.EntityFrameworkCore;
using Npgsql;

using PrettyUserAuthorizationAspShared;

using AspErrorHandling;
using AspErrorHandling.Converters;
using AspErrorHandling.Filters;

namespace PrettyUserAuthorization.StartupServices
{
	using Filters;

	public static class MvcServicesExtensions
	{
		public static void AddControllers(this IServiceCollection services)
		{
			services.AddControllers(options =>
			{
				Type[] excConverters = new Type[]
				{
					typeof(IExceptionErrorConverter<Exception>),

					typeof(IExceptionErrorConverter<PostgresException>),
					typeof(IExceptionErrorConverter<SocketException>),
					typeof(IExceptionErrorConverter<DbUpdateException>),

					typeof(IExceptionErrorConverter<ObjectDisposedException>),
					typeof(IExceptionErrorConverter<InvalidOperationException>)
				};

				options.Filters.Add(new ExtensibleExeptionHandlingLazyFilterFactory(excConverters));
				options.Filters.Add(new ServiceFilterAttribute(typeof(SetProcessedFilter)));
			})
			.ConfigureApiBehaviorOptions(options =>
			{
				options.InvalidModelStateResponseFactory = OnInvalidModelStateResponse;
			});
		}

		public static void AddFilters(this IServiceCollection services)
		{
			services.AddScoped<SetProcessedFilter>();
		}

		private static IActionResult OnInvalidModelStateResponse(ActionContext context)
		{
			IServiceProvider services = context.HttpContext.RequestServices;

			IErrorConverter<ModelStateDictionary> converter =
				services.GetRequiredService<IErrorConverter<ModelStateDictionary>>();

			IResponseErrorObjectCompiler errorsCompiler =
				services.GetRequiredService<IResponseErrorObjectCompiler>();

			IHttpContextProcessedMarker processedMarker =
				services.GetRequiredService<IHttpContextProcessedMarker>();

			IErrorConvertationResult convertResult = converter.Convert(context.ModelState)!;
			object errorBody = errorsCompiler.CompileResponse(convertResult);

			processedMarker.SetProcessed(true);

			return new BadRequestObjectResult(errorBody);
		}
	}
}
