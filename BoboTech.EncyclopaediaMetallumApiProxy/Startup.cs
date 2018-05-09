using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoboTech.EncyclopaediaMetallumApiProxy
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) => 
            services
                .Configure<Settings.EncyclopaediaMetallum>(Configuration.GetSection(nameof(Settings.EncyclopaediaMetallum)))
                .AddMvc(o => o.Filters.Add<Filters.LogAllExceptions>());

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStatusCodePages()
                .UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
                .UseMvc();
        }
    }
}