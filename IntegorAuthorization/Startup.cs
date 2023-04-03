using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using Microsoft.EntityFrameworkCore;

using IntegorAspHelpers.Middleware.WebApiResponse;

using ExtensibleRefreshJwtAuthentication.Access.Tokens;
using ExtensibleRefreshJwtAuthentication.Refresh.Tokens;

using IntegorServiceConfiguration;
using IntegorAuthorizationAspServices.TokenResolvers;

namespace IntegorAuthorization
{
	using StartupServices;

	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			Type exceptionBaseConverter = services.AddExceptionConverting();

			IEnumerable<Type> exceptionConverters = services
				.AddDatabaseExceptionConverters()
				.Prepend(exceptionBaseConverter);

			services.AddPrimaryTypesErrorConverters();

			services.AddConfiguredControllers(exceptionConverters.ToArray());

			services.AddDatabase(Configuration.GetConnectionString("IntegorAuthorizationDatabase"));

			services.AddAuthenticationSchemes();
			services.AddAuthenticationServices();
			services.AddAuthenticationTokensProcessing();

			services.AddSingleton<IAccessTokenResolver, JwtAccessTokenResolver>();
			services.AddSingleton<IRefreshTokenResolver, JwtRefreshTokenResolver>();

			services.AddAutoMapper();

			services.AddHttpContextServices();
			services.AddConfigurationProviders();

			services.AddResponseDecorators();
			services.AddAuthorizationResponseDecorators();

			services.AddSecurity();
			services.AddUsers();
			services.AddRoles();

			services.AddAuthenticationLogic();

			services.AddScoped<ApplicationInitializer>();
		}

		public void Configure(IApplicationBuilder app, IServiceProvider provider)
		{
			app.UseWebApiExceptionsHandling(WriteJsonBody);
			app.UseWebApiStatusCodesHandling(WriteJsonBody);

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

		private async Task WriteJsonBody(HttpResponse response, object body)
			 => await response.WriteAsJsonAsync(body);
	}
}
