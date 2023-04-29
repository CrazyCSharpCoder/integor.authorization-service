using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Builder;

using IntegorAspHelpers.Middleware.WebApiResponse;

using ExtensibleRefreshJwtAuthentication.Access;
using ExtensibleRefreshJwtAuthentication.Refresh;

using IntegorAuthorizationAspServices.TokenResolvers;

using IntegorServiceConfiguration;
using IntegorServiceConfiguration.Authentication;
using IntegorServiceConfiguration.Controllers;

namespace IntegorAuthorization
{
	using StartupServices;

	public class Startup
	{
		public IConfiguration Configuration { get; }

		private IConfiguration _cookieTypesConfiguration;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;

			_cookieTypesConfiguration = new ConfigurationBuilder()
				.AddJsonFile("cookie_types_configuration.json")
				.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			// Configuring errors handling
			Type exceptionBaseConverter = services.AddExceptionConverting();

			IEnumerable<Type> exceptionConverters = services
				.AddDatabaseExceptionConverters()
				.Prepend(exceptionBaseConverter);
			
			services.AddPrimaryTypesErrorConverters();
			services.AddResponseErrorsObjectCompiler();

			// Configuring response decorators
			services.AddErrorResponseDecorator();
			services.AddAuthorizationResponseDecorators();

			// Configuring MVC
			services.AddDefaultControllersConfiguration(exceptionConverters.ToArray());

			// Configuring infrastructure
			services.AddDatabase(Configuration.GetConnectionString("IntegorAuthorizationDatabase"));
			
			services.AddAuthenticationSchemes();

			services.AddSingleton<IAccessTokenResolver, JwtAccessTokenResolver>();
			services.AddSingleton<IRefreshTokenResolver, JwtRefreshTokenResolver>();

			services.AddAuthorizationServices(_cookieTypesConfiguration);
			services.AddOnServiceProcessingTokenAuthentication();

			services.AddAutoMapper();

			services.AddHttpContextAccessor();
			services.AddConfigurationProviders();

			services.AddDefaultStatusCodeResponseBodyFactory();

			// Configuring logic
			services.AddSecurity();
			services.AddUsers();
			services.AddRoles();

			services.AddAuthenticationLogic();

			services.AddScoped<ApplicationInitializer>();
		}

		public void Configure(IApplicationBuilder app, IServiceProvider provider)
		{
			app.UseWebApiExceptionsHandling();
			app.UseWebApiStatusCodesHandling();

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			InitApplicationAsync(provider).Wait();
		}

		private async Task InitApplicationAsync(IServiceProvider provider)
		{
			ApplicationInitializer initializer = provider.GetRequiredService<ApplicationInitializer>();

			await initializer.EnsureRolesCreatedAsync();
		}
	}
}
