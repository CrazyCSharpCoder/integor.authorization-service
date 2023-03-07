using Microsoft.Extensions.DependencyInjection;

using IntegorAuthorizationResponseDecoration.Decorators;

namespace IntegorAuthorization.StartupServices
{
	public static class ResponseBodyDecoratorsServicesExtensions
	{
		public static void AddResponseDecorators(this IServiceCollection services)
		{
			services.AddSingleton<ErrorResponseBodyDecorator>();
			services.AddSingleton<UserResponseBodyDecorator>();
			services.AddSingleton<UserRolesCollectionResponseBodyDecorator>();
		}
	}
}
