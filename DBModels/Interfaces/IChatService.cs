using Spill.Core.Web.Models;
using System;

namespace Spill.Core.Web.DBModels.Interfaces
{
    public interface IChatService
    {
        Task<int> GetReceivedMessagesCountAsync(string filter);
        Task<int> GetOnlineChatsCountAsync(string filter);
        Task<int> GetCurrentConversationsCountAsync(string filter);
        Task<int> GetUnansweredChatsCountAsync(string filter);
        Task<double> GetAverageResponseTimeAsync(string filter); // Manter double para tempo médio
    }


}
