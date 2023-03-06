using Microsoft.Extensions.DependencyInjection;

using AdvancedJwtAuthentication.Access;
using AdvancedJwtAuthentication.Refresh;

using AdvancedJwtAuthentication.Services;

using PrettyUserAuthorizationAspShared;
using PrettyUserAuthorizationAspShared.ConfigurationProviders;

using PrettyUserAuthorizationAspServices;
using PrettyUserAuthorizationAspServices.Authentication;
using PrettyUserAuthorizationAspServices.ConfigurationProviders;

using PrettyUserAuthorizationShared.Services;
using PrettyUserAuthorizationShared.Helpers;

using PrettyUserAuthorizationServices.DatabaseContext;

using Microsoft.EntityFrameworkCore;

namespace PrettyUserAuthorization.StartupServices
{
	using Middleware;

	using Mapper.Profiles;
	using Mapper.Profiles.AspDtoMap;

	public static class InfrastructureServicesExtensions
	{
		public static void AddHttpContextServices(this IServiceCollection services)
		{
			services.AddHttpContextAccessor();
			services.AddScoped<IHttpContextProcessedMarker, HttpContextProcessedMarker>();
		}

		public static void AddMiddleware(this IServiceCollection services)
		{
			services.AddScoped<ExceptionsHandlingMiddleware>();
			services.AddScoped<StatusCodesHandlingMiddleware>();
		}

		public static void AddConfigurationProviders(this IServiceCollection services)
		{
			services.AddSingleton<IAuthenticationConfigurationProvider, AuthenticationConfigurationProvider>();
		}

		public static void AddAuthenticationSchemes(this IServiceCollection services)
		{
			services.AddAuthentication(JwtAccessAuthenticationDefaults.AuthenticationScheme)
				.AddScheme<JwtAccessAuthenticationOptions, JwtAccessAuthenticationHandler>(
					JwtAccessAuthenticationDefaults.AuthenticationScheme, options =>
					{
						// TODO add options
					})
				.AddScheme<JwtRefreshAuthenticationOptions, JwtRefreshAuthenticationHandler>(
					JwtRefreshAuthenticationDefaults.AuthenticationScheme, options =>
					{
						// TODO add options
					});
		}

		public static void AddAuthenticationServices(this IServiceCollection services)
		{
			services.AddScoped<IHttpContextTokensAccessor, CookieTokensAccessor>();
			services.AddScoped<IResolveTokensService, ResolveJwtService>();

			services.AddScoped<IAuthenticationAbstractionService, AuthenticationAbstractionService>();
		}

		public static void AddDatabase(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<PrettyUserAuthorizationDbContext>(options =>
			{
				options.UseNpgsql(connectionString);
			});
		}

		public static void AddAutoMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(
				typeof(UsersMapperProfile),
				typeof(UserRolesMapperProfile),
				typeof(AuthenticationDtoMapperProfile));
		}

		public static void AddSpecial(this IServiceCollection services)
		{
			services.AddSingleton<UserRolesConverter>();
			services.AddScoped<ApplicationInitializer>();
		}
	}
}
