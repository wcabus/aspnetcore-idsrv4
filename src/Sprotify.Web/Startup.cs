using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sprotify.Web.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sprotify.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public static IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Enable custom options
            services.AddOptions();

            // Add services

            // register an IHttpContextAccessor so we can access the current
            // HttpContext in services by injecting it
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // register a SprotifyHttpClient
            services.AddScoped<SprotifyHttpClient>();

            services.AddScoped<PlaylistService>();
            services.AddScoped<StatsService>();
         
            //services.AddScoped(isp => new PlaylistService(client, baseUri));
            //services.AddScoped(isp => new StatsService(client, baseUri));

            // Add MVC services
            services.AddMvc();
        }

        // You can also create a specific service configuration method per environment by using the environment name:
        /*
         * public void ConfigureProductionServices(IServiceCollection services) 
         * {
         * 
         * }
         */

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Models.Api.Playlist, Models.Music.PlaylistInYourMusic>();
                cfg.CreateMap<Models.Api.PlaylistWithSongs, Models.Music.PlaylistDetails>();
                cfg.CreateMap<Models.Api.Song, Models.Music.Song>();
            });
            
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies"
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                Authority = "https://localhost:44375/",
                RequireHttpsMetadata = true,
                ClientId = "sprotifyclient",
                Scope = {"openid", "profile", "sprotifyapi", "offline_access" },
                ResponseType = "code id_token",
                SignInScheme = "Cookies",
                SaveTokens = true,
                ClientSecret = "secret",
                GetClaimsFromUserInfoEndpoint = true,
                Events = new OpenIdConnectEvents()
                {
                    // optional: get rid of claims that aren't needed
                    //OnTokenValidated = tokenValidatedContext =>
                    //{
                    //    var identity = tokenValidatedContext.Ticket.Principal.Identity
                    //        as ClaimsIdentity;

                    //    var subjectClaim = identity.Claims.FirstOrDefault(z => z.Type == "sub");

                    //    var newClaimsIdentity = new ClaimsIdentity(
                    //      tokenValidatedContext.Ticket.AuthenticationScheme,
                    //      "given_name",
                    //      "role");

                    //    newClaimsIdentity.AddClaim(subjectClaim);

                    //    tokenValidatedContext.Ticket = new AuthenticationTicket(
                    //        new ClaimsPrincipal(newClaimsIdentity),
                    //        tokenValidatedContext.Ticket.Properties,
                    //        tokenValidatedContext.Ticket.AuthenticationScheme);

                    //    return Task.CompletedTask; 
                    //}
                }
            });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
