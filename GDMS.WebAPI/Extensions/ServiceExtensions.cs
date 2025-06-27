using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PEL.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
		public static void ConfigureCors(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials());
			});
		}
		public static void ConfigureIISIntegration(this IServiceCollection services)
		{
			services.Configure<IISOptions>(options =>
			{

			});
		}
		public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
		{
			var connectionString = config["mysqlconnection:connectionString"];
			//services.AddConnections(connectionString);
			//services.AddDbContext<RepositoryContext>(o => o.UseMySql(connectionString));
		}
	}
}
