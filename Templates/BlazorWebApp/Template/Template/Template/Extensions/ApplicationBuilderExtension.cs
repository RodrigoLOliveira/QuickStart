using Template.Client.Pages;
using Template.Components;

namespace Template.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder ConfigurePipeline(this WebApplication app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
               .AddInteractiveWebAssemblyRenderMode()
               .AddAdditionalAssemblies(typeof(Template.Client._Imports).Assembly);
            app.MapAdditionalIdentityEndpoints();

            return app;
        }
    }
}
