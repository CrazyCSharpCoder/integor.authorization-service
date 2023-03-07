using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Builder;

using Microsoft.EntityFrameworkCore;

namespace PrettyUserAuthorization
{
	using StartupServices;
	using Middleware;

	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddFilters();

			services.AddDatabase(Configuration.GetConnectionString("PrettyUserAuthorizationDatabase"));

			services.AddAuthenticationSchemes();
			services.AddAuthenticationServices();

			services.AddAutoMapper();

			services.AddMiddleware();
			services.AddHttpContextServices();
			services.AddConfigurationProviders();

			services.AddResponseDecorators();

			services.AddTypesErrorConverters();
			services.AddStandartExceptionConverters();
			services.AddDatabaseExceptionConverters();

			services.AddSecurity();
			services.AddUsers();
			services.AddRoles();

			services.AddSpecial();
		}

		public void Configure(IApplicationBuilder app, IServiceProvider provider)
		{
			app.UseMiddleware<ExceptionsHandlingMiddleware>();
			app.UseMiddleware<StatusCodesHandlingMiddleware>();

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
