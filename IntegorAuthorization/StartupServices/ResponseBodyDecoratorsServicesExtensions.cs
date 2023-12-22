using Microsoft.Extensions.DependencyInjection;

using IntegorSharedResponseDecorators.Authorization.Decorators;
using IntegorAuthorizationResponseDecoration.Decorators;

namespace IntegorAuthorization.StartupServices
{
	public static class ResponseBodyDecoratorsServicesExtensions
	{
		public static void AddAuthorizationResponseDecorators(this IServiceCollection services)
		{
			services.AddSingleton<UserToPublicDtoDecorator>();
			services.AddSingleton<UserResponseObjectDecorator>();

			services.AddSingleton<RolesEnumerableToPublicDtoDecorator>();
			services.AddSingleton<UserRolesEnumerableResponseObjectDecorator>();
		}
	}
}
