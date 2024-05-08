using Microsoft.AspNetCore.Components;
using Template.Domain.Entities;

namespace Template.Server.Components.Pages.Crud
{
    public partial class DetailModal
    {
        [Parameter]
        public Home Home { get; set; } = new Home();

    }
}
