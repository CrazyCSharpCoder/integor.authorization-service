using Microsoft.Extensions.DependencyInjection;

using IntegorAuthorizationShared.Services;
using IntegorAuthorizationShared.Services.Security;
using IntegorAuthorizationShared.Services.Security.Password;

using IntegorAuthorizationServices;
using IntegorAuthorizationServices.Security;
using IntegorAuthorizationServices.Security.Password;

using IntegorAuthorizationShared.Services.Unsafe;
using IntegorAuthorizationServices.Unsafe;

using IntegorAuthorizationAspServices;

namespace IntegorAuthorization.StartupServices
{
	public static class LogicServicesExtensions
	{
		public static void AddSecurity(this IServiceCollection services)
		{
			services.AddScoped<IPasswordValidationService, PasswordValidationService>();
			services.AddScoped<ISecurityDataAccessService, SecurityDataSaltAccessService>();

			services.AddScoped<IPasswordEncryptionService, PasswordEncryptionService>();
			services.AddSingleton<IPasswordSaltService, PasswordSaltService>();
			services.AddSingleton<IByteStringifyingService, HexadecimalByteStringifyingService>();
		}

		public static IServiceCollection AddUsers(this IServiceCollection services)
		{
			services.AddScoped<IUserValidationService, UserValidationService>();
			services.AddScoped<IUsersService, UsersService>();

			return services;
		}

		public static IServiceCollection AddRoles(this IServiceCollection services)
		{
			services.AddScoped<IUserRolesService, UserRolesService>();
			services.AddScoped<IEditUserRolesService, EditUserRolesService>();

			return services;
		}

		public static IServiceCollection AddAuthenticationLogic(this IServiceCollection services)
		{
			return services.AddScoped<IAuthenticationAbstractionService, AuthenticationAbstractionService>();
		}
	}
}
