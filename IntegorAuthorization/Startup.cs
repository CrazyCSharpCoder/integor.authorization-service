using System;
using System.Threading.Tasks;

using IntegorAspHelpers.Middleware.WebApiResponse;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using Microsoft.EntityFrameworkCore;

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
			services.AddControllers();
			services.AddFilters();

			services.AddDatabase(Configuration.GetConnectionString("IntegorAuthorizationDatabase"));

			services.AddAuthenticationSchemes();
			services.AddAuthenticationServices();

			services.AddAutoMapper();

			services.AddHttpContextServices();
			services.AddConfigurationProviders();

			services.AddResponseDecorators();

			services.AddTypesErrorConverters();
			services.AddStandardExceptionConverters();
			services.AddDatabaseExceptionConverters();

			services.AddSecurity();
			services.AddUsers();
			services.AddRoles();

			services.AddSpecial();
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
