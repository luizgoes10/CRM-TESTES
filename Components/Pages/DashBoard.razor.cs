using Microsoft.AspNetCore.Components;
using Spill.Core.Web.DBModels.Interfaces;
using System.Text;

namespace Spill.Core.Web.Components.Pages
{
    public partial class DashBoard
    {
        private int receivedMessages = 0;
        private int onlineChats = 0;
        private int currentConversations = 0;
        private int unansweredChats = 0;
        private double averageResponseTime = 0;
        private int wonLeads = 0;
        private int activeLeads = 0;
        private int lostLeads = 0;

        //componete dinâmico

        [Parameter] public string Title { get; set; }
        [Parameter] public string Content { get; set; }

        private const string _today = "today";
        private const string _yesterday = "yesterday";
        private const string _week = "week";
        private const string _month = "month";
        private const string _all = "all";



        // Injeção dos serviços (deve ser feita por propriedade)
        [Inject]
        private IChatService _chatService { get; set; }
        [Inject]
        private ILeadService _leadService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            // Carregar os dados do dashboard ao inicializar
            await LoadDashboardDataAsync("all");
        }

        public async Task LoadDashboardDataAsync(string filter)
        {
            try
            {
                // Carregar os dados do serviço usando o filtro
                receivedMessages = await _chatService.GetReceivedMessagesCountAsync(filter);
                onlineChats = await _chatService.GetOnlineChatsCountAsync(filter);
                currentConversations = await _chatService.GetCurrentConversationsCountAsync(filter);
                unansweredChats = await _chatService.GetUnansweredChatsCountAsync(filter);
                averageResponseTime = await _chatService.GetAverageResponseTimeAsync(filter);

                wonLeads = await _leadService.GetWonLeadsCountAsync(filter);
                activeLeads = await _leadService.GetActiveLeadsCountAsync(filter);
                lostLeads = await _leadService.GetLostLeadsCountAsync(filter);
            }
            catch (Exception ex)
            {
                // Trate exceções, exiba mensagens de erro, etc.
                Console.Error.WriteLine($"Erro ao carregar dados do dashboard: {ex.Message}");
            }
        }
        private async Task ApplyFilter(string filter)
        {
            await this.LoadDashboardDataAsync(filter);
        }



    }
}
