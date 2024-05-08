using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Template.Client.Components.DynamicGridComponent.Models
{
    public class ButtonDataGridModel<T>
    {
        public string Text { get; set; }
        public Color Color { get; set; } = Color.Primary;
        public Action<T> OnClick { get; set; }
    }
}
