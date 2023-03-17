using Microsoft.Extensions.DependencyInjection;

using IntegorAspHelpers.Http;
using IntegorAspHelpers.Middleware.WebApiResponse.Internal;

using IntegorSharedAspHelpers.Http;

using AdvancedJwtAuthentication.Access;
using AdvancedJwtAuthentication.Refresh;

using AdvancedJwtAuthentication.Services;

using IntegorAuthorizationAspShared.ConfigurationProviders;

using IntegorAuthorizationAspServices;
using IntegorAuthorizationAspServices.Authentication;
using IntegorAuthorizationAspServices.ConfigurationProviders;

using IntegorAuthorizationShared.Services;
using IntegorAuthorizationShared.Helpers;

using IntegorAuthorizationServices.DatabaseContext;

using Microsoft.EntityFrameworkCore;

namespace IntegorAuthorization.StartupServices
{
	using Mapper.Profiles;
	using Mapper.Profiles.AspDtoMap;

	public static class InfrastructureServicesExtensions
	{
		public static void AddHttpContextServices(this IServiceCollection services)
		{
			services.AddHttpContextAccessor();
			services.AddScoped<IHttpContextProcessedMarker, HttpContextProcessedMarker>();
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
			services.AddDbContext<IntegorAuthorizationDbContext>(options =>
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
