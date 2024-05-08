using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Template.Server.Components.Layout
{
    public partial class MainLayout
    {
        private MudTheme _theme = new();
        private bool _isDarkMode;
        private bool open = true;

        public void ToggleDrawer(MouseEventArgs args)
        {
            open = !open;
        }

        public void Teste(MouseEventArgs args)
        {

        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
