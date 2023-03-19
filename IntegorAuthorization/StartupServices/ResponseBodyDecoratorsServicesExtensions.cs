using Microsoft.Extensions.DependencyInjection;

using IntegorSharedResponseDecorators.Decorators;
using IntegorSharedResponseDecorators.Decorators.Authorization;

using IntegorAuthorizationResponseDecoration.Decorators;

namespace IntegorAuthorization.StartupServices
{
	public static class ResponseBodyDecoratorsServicesExtensions
	{
		public static void AddResponseDecorators(this IServiceCollection services)
		{
			services.AddSingleton<ErrorResponseBodyDecorator>();

			services.AddSingleton<UserToPublicDtoDecorator>();
			services.AddSingleton<UserResponseBodyDecorator>();

			services.AddSingleton<RolesEnumerableToPublicDtoDecorator>();
			services.AddSingleton<UserRolesCollectionResponseBodyDecorator>();
		}
	}
}
