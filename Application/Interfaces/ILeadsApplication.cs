using Spill.Core.Web.Models.AuxiliarModels;

namespace Spill.Core.Web.Application.Interfaces
{
    public interface ILeadsApplication
    {
        Task SendMessageAsync(SendWhatsappMessageRequest request);
    }
}
