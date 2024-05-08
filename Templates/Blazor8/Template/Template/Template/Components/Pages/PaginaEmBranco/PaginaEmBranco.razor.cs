using Microsoft.AspNetCore.Components;
using Template.Business.Services;

namespace Template.Server.Components.Pages.PaginaEmBranco
{
    public partial class PaginaEmBranco
    {
        [Inject]
        public HomeService HomeService { get; set; } = default!;
    }
}
