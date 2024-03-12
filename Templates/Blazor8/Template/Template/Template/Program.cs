using Inteleduc.Web.API.Filters;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Template.Components.Account;
using Template.Domain.Authorization;
using Template.Infra.Contexts;
using Template.Server.Components;

namespace Template
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services, builder.Configuration);

            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            var app = builder.Build();
            ConfigureApp(app, app.Environment);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Configuração do DbContext e Connection String
            ConfigureDb(services, configuration);

            // Configuração de autenticação
            services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            services.AddCascadingAuthenticationState();
            services.AddScoped<IdentityUserAccessor>();
            services.AddScoped<IdentityRedirectManager>();
            services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

            // Configuração do Identity
            services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<TemplateDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();
            services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            // Configuração de Controllers e Views
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new TemplateExceptionFilter());
            });

            // Configuração de Swagger
            services.AddSwaggerGen();
        }

        private static void ConfigureApp(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.MapAdditionalIdentityEndpoints();
            app.MapControllers();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseSerilogRequestLogging();
        }

        private static void ConfigureDb(IServiceCollection services, IConfiguration configuration)
        {
            var debugInMemory = configuration.GetValue<bool>("DebugInMemory");
            var connectionString = configuration.GetConnectionString("DefaultConnection");

#if DEBUG
            if (debugInMemory)
                services.AddDbContext<TemplateDbContext>(options => options.UseInMemoryDatabase("DdTemplate"));
            else
                services.AddDbContext<TemplateDbContext>(options => options.UseSqlServer(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();
#else
            services.AddDbContext<TemplateDbContext>(options => options.UseSqlServer(connectionString));
#endif
        }
    }
}
