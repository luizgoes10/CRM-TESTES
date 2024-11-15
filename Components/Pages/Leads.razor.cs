using Spill.Core.Web.Models;
using Spill.Core.Web.Models.AuxiliarModels;

namespace Spill.Core.Web.Components.Pages
{
    public partial class Leads
    {
        private Lead newLead = new Lead();
        private List<Lead> leads = new List<Lead>();

        protected override async Task OnInitializedAsync()
        {
            // Carregar os leads ao inicializar a página
            leads = await leadService.GetLeadsAsync();
        }

        private async Task CreateLead()
        {
            newLead.int_status_id = 9; //status novo lead
            newLead.dt_created = DateTime.Now;
            await leadService.CreateLead(newLead);
            leads.Add(newLead);
            newLead = new Lead(); // Limpar o formulário
        }

        private async Task SendMessage(Lead lead)
        {
            var leadMessage = new SendWhatsappMessageRequest
            {

            };
            await leadApplication.SendMessageAsync(leadMessage);
            lead.int_status_id = 10; // Atualiza o status localmente para contacted
            await leadService.UpdateLeadStatus(lead);
        }
    }
}
