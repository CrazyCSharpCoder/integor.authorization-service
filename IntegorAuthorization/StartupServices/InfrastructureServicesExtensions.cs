using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using ExtensibleRefreshJwtAuthentication;

using ExtensibleRefreshJwtAuthentication.Access;
using ExtensibleRefreshJwtAuthentication.Refresh;

using IntegorLogicShared.MicroserviceSpecific.Authorization;

using IntegorAuthorizationAspShared.ConfigurationProviders;
using IntegorAuthorizationAspServices.ConfigurationProviders;

using IntegorAuthorizationServices.DatabaseContext;

namespace IntegorAuthorization.StartupServices
{
	using Mapper.Profiles;
	using Mapper.Profiles.Dto;

	public static class InfrastructureServicesExtensions
	{
		public static IServiceCollection AddConfigurationProviders(this IServiceCollection services)
		{
			return services.AddSingleton<IAuthenticationConfigurationProvider, AuthenticationConfigurationProvider>();
		}

		public static void AddAuthenticationSchemes(this IServiceCollection services)
		{
			services.AddAuthentication(AccessTokenAuthenticationDefaults.AuthenticationScheme)
				.AddScheme<TokenAuthenticationOptions, AccessTokenAuthenticationHandler>(
					AccessTokenAuthenticationDefaults.AuthenticationScheme, options =>
					{
						options.AuthenticationType = AccessTokenAuthenticationDefaults.AuthenticationType;
					})
				.AddScheme<TokenAuthenticationOptions, RefreshTokenAuthenticationHandler>(
					RefreshTokenAuthenticationDefaults.AuthenticationScheme, options =>
					{
						options.AuthenticationType = RefreshTokenAuthenticationDefaults.AuthenticationType;
					});
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
			services.AddScoped<ApplicationInitializer>();
		}
	}
}
