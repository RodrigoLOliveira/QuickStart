using Microsoft.AspNetCore.Components;
using MudBlazor;
using Template.Business.Services;
using Template.Client.Components.DynamicGridComponent.Models;
using Template.Domain.Entities;

namespace Template.Server.Components.Pages.Crud
{
    public partial class List
    {
        [Inject]
        private IDialogService DialogService { get; set; } = default!;

        [Inject]
        public HomeService HomeService { get; set; } = default!;

        public IEnumerable<Home> _listHome { get; set; } = new List<Home>();
        private List<ButtonDataGridModel<Home>> Buttons = [];

        protected override void OnInitialized()
        {
            _listHome = HomeService.HomeRepository.GetAll();

            Buttons = new List<ButtonDataGridModel<Home>>
            {
                new ButtonDataGridModel<Home>
                {
                    Text = "Detalhes",
                    Color = MudBlazor.Color.Primary,
                    OnClick = item => View(item)
                },
                new ButtonDataGridModel<Home>
                {
                    Text = "Editar",
                    OnClick = item => Edit(item)
                },
                new ButtonDataGridModel<Home>
                {
                    Text = "Excluir",
                    OnClick = async item => await Delete(item)
                }
            };
        }

        private void View(Home item)
        {
            var parameters = new DialogParameters() { { "Home", item } };
            DialogService.Show<DetailModal>("Detalhes", parameters);
        }

        private void Edit(Home item)
        {
            var parameters = new DialogParameters() { { "Home", item } };
            DialogService.Show<EditModal>("Editar", parameters);
        }

        private async Task Delete(Home item)
        {
            bool? result = await DialogService.ShowMessageBox(
            title: "Confirmar",
            message: "Tem certeza que deseja excluir esse registro?",
            yesText: "Confirmar",
            cancelText: "Cancelar");

            if (result is true)
            {
                HomeService.HomeRepository.Remove(item);
                HomeService.HomeRepository.Save();

                _listHome = HomeService.HomeRepository.GetAll();
            }

            StateHasChanged();
        }
    }
}
