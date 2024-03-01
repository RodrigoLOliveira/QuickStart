using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Template.Components.Account;
using Template.Domain.Authentication;
using Template.Domain.Interfaces;
using Template.Infra.Data;

namespace Template.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRazorAndAuthenticationServices(this IServiceCollection services)
        {
            // Configurações do Razor Components e autenticação
            services.AddRazorComponents()
                    .AddInteractiveWebAssemblyComponents();

            services.AddCascadingAuthenticationState();
            services.AddScoped<IdentityUserAccessor>();
            services.AddScoped<IdentityRedirectManager>();
            services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();

            services.AddAuthorization();

            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
        {
            // Configurações específicas de autenticação
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuração do contexto do banco de dados
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddSignInManager()
                    .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddEmailSender(this IServiceCollection services)
        {
            // Configuração do serviço de envio de e-mail
            services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
            return services;
        }

        public static IServiceCollection AddScopedServices(this IServiceCollection services, params string[] assemblies)
        {
            foreach (var assemblyString in assemblies)
            {
                var assembly = Assembly.Load(assemblyString);

                var typesFromAssemblies = assembly.GetTypes()
                    .Where(type => typeof(IServiceScoped).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    .ToList();

                foreach (var type in typesFromAssemblies)
                {
                    foreach (var itype in type.GetInterfaces().Where(i => typeof(IServiceScoped).IsAssignableFrom(i)))
                    {
                        services.AddScoped(type);
                    }
                }
            }

            return services;
        }
    }
}
