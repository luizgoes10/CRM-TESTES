using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Spill.Core.Web.Models;
using static System.Net.WebRequestMethods;

namespace Spill.Core.Web.Components.Layout
{
    public partial class MainLayout
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;
        private string CurrentPage { get; set; } = string.Empty;

        private string sidebarClass = "";
        private bool isExpanded = false;

        private void ExpandSidebar()
        {
            sidebarClass = "expanded";
            isExpanded = true;
        }

        private void CollapseSidebar()
        {
            sidebarClass = "";
            isExpanded = false;
        }
        protected override void OnInitialized()
        {
            // Obter a página atual no carregamento inicial
            UpdateCurrentPage();

            // Inscrever-se no evento de navegação
            NavigationManager.LocationChanged += OnLocationChanged;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var currentPage = NavigationManager.Uri;
                var userId = GetUserId(); 

                var visit = new Visit
                {
                    str_user_id = userId,
                    str_page = currentPage,
                    dt_visit_time = DateTime.UtcNow
                };

                //await Http.PostAsJsonAsync("/api/Visit/log", visit);
            }
        }

        private string GetUserId()
        {
            // Pega um identificador único para o usuário, como ID do banco ou ID de sessão.
            return Guid.NewGuid().ToString();
        }

        private void UpdateCurrentPage()
        {
            // Obter o caminho relativo da página atual
            CurrentPage = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);

            // Atualizar o estado do componente
            StateHasChanged();
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            // Atualizar a página atual quando a URL mudar
            UpdateCurrentPage();
        }

        public void Dispose()
        {
            // Cancelar a inscrição no evento para evitar problemas de memória
            NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}
