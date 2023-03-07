using Microsoft.Extensions.DependencyInjection;

using PrettyUserAuthorizationResponseDecoration.Decorators;

namespace PrettyUserAuthorization.StartupServices
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
