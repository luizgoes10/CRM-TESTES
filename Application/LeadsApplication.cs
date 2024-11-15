using Spill.Core.Web.Application.Interfaces;
using Spill.Core.Web.Models.AuxiliarModels;

namespace Spill.Core.Web.Application
{
    public class LeadsApplication : ILeadsApplication
    {
        public Task SendMessageAsync(SendWhatsappMessageRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
