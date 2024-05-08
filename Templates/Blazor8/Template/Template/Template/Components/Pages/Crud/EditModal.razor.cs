using Microsoft.AspNetCore.Components;
using Template.Business.Services;
using Template.Domain.Entities;

namespace Template.Server.Components.Pages.Crud
{
    public partial class EditModal
    {
        [Parameter]
        public Home Home { get; set; } = new Home();

        [Inject]
        public HomeService HomeService { get; set; } = default!;

        private void Save(Home home)
        {
            HomeService.HomeRepository.Update(home);
            HomeService.HomeRepository.Save();
        }
    }
}
