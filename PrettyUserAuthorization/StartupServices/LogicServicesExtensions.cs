using Microsoft.Extensions.DependencyInjection;

using PrettyUserAuthorizationShared.Services;
using PrettyUserAuthorizationShared.Services.Security;
using PrettyUserAuthorizationShared.Services.Security.Password;

using PrettyUserAuthorizationServices;
using PrettyUserAuthorizationServices.Security;
using PrettyUserAuthorizationServices.Security.Password;

using PrettyUserAuthorizationShared.Services.Unsafe;
using PrettyUserAuthorizationServices.Unsafe;

namespace PrettyUserAuthorization.StartupServices
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

		public static void AddUsers(this IServiceCollection services)
		{
			services.AddScoped<IUserValidationService, UserValidationService>();
			services.AddScoped<IUsersService, UsersService>();
		}

		public static void AddRoles(this IServiceCollection services)
		{
			services.AddScoped<IUserRolesService, UserRolesService>();
			services.AddScoped<IEditUserRolesService, EditUserRolesService>();
		}
	}
}
