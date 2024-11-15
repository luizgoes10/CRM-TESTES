using Spill.Core.Web.Models;

namespace Spill.Core.Web.DBModels.Interfaces
{
    public interface ILeadService
    {
        Task<int> GetWonLeadsCountAsync(string filter);
        Task<int> GetActiveLeadsCountAsync(string filter);
        Task<int> GetLostLeadsCountAsync(string filter);
        Task<List<Lead>> GetLeadsAsync();
        Task<int> CreateLead(Lead lead);
        Task<int> UpdateLeadStatus(Lead lead);
    }
}

