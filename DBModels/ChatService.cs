using Spill.Core.Web.DBModels.Interfaces;
using Spill.Core.Web.Models;

namespace Spill.Core.Web.DBModels
{
    public class ChatService : IChatService
    {
        private readonly IDatabaseService _databaseService;

        public ChatService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<int> GetReceivedMessagesCountAsync(string filter)
        {
            var query = $"SELECT COUNT(*) FROM whats_app_messages WHERE {CreateQueryReiceivedMessages(filter).FirstOrDefault()}";
            return await _databaseService.QuerySingleOrDefaultAsync<int>(query);
        }

        public async Task<int> GetOnlineChatsCountAsync(string filter)
        {
            var query = $"SELECT COUNT(*) FROM whats_app_chats WHERE {CreateQueryOnlineChats(filter).FirstOrDefault()}";
            return await _databaseService.QuerySingleOrDefaultAsync<int>(query);
        }

        public async Task<int> GetCurrentConversationsCountAsync(string filter)
        {
            var query = @$"SELECT COUNT(DISTINCT c.int_id) FROM whats_app_chats c 
                              INNER JOIN whats_app_messages m ON c.int_id = m.int_chat_id WHERE m.int_status_id IN (1,2,3,6) AND 
                                 {CreateQueryCurrentConversations(filter).FirstOrDefault()}";
            return await _databaseService.QuerySingleOrDefaultAsync<int>(query);
        }

        public async Task<int> GetUnansweredChatsCountAsync(string filter)
        {
            var query = @$"SELECT COUNT(DISTINCT c.int_id) FROM whats_app_chats c 
                              INNER JOIN whats_app_messages m ON c.int_id = m.int_chat_id WHERE m.int_status_id = 3 AND 
                                 {CreateQueryCurrentConversations(filter).FirstOrDefault()}";
            return await _databaseService.QuerySingleOrDefaultAsync<int>(query);
        }

        public async Task<double> GetAverageResponseTimeAsync(string filter)
        {
            var query = @$"SELECT COALESCE(AVG(TIMESTAMPDIFF(SECOND, dt_message_sent, dt_message_received)), 0) AS avg_response_time FROM whats_app_messages m
                                WHERE m.int_status_id = 6 AND {CreateQueryCurrentConversations(filter).FirstOrDefault()}";
            return await _databaseService.QuerySingleOrDefaultAsync<double>(query);
        }

        private List<string> CreateQueryReiceivedMessages(string filter)
        {
            var _whereList = new List<string>();
            switch (filter)
            {
                case "today":
                    _whereList.Add($"DATE(dt_message_sent) = CURDATE();");
                    break;
                case "yesterday":
                    _whereList.Add($"DATE(dt_message_sent) = CURDATE() - INTERVAL 1 DAY;");
                    break;
                case "month":
                    _whereList.Add($"YEAR(dt_message_sent) = YEAR(CURDATE()) AND MONTH(dt_timestamp) = MONTH(CURDATE());");
                    break;
                case "week":
                    _whereList.Add($"YEARWEEK(dt_message_sent, 1) = YEARWEEK(CURDATE(), 1);");
                    break;
                case "all":
                    _whereList.Add("TRUE;");
                    break;
                default:
                    _whereList.Add("TRUE;");
                    break;

            }
            return _whereList;
        }

        private List<string> CreateQueryOnlineChats(string filter)
        {
            var _whereList = new List<string>();
            switch (filter)
            {
                case "today":
                    _whereList.Add($"DATE(dt_chat_start) = CURDATE();");
                    break;
                case "yesterday":
                    _whereList.Add($"DATE(dt_chat_start) = CURDATE() - INTERVAL 1 DAY;");
                    break;
                case "month":
                    _whereList.Add($"YEAR(dt_chat_start) = YEAR(CURDATE()) AND MONTH(dt_chat_start) = MONTH(CURDATE());");
                    break;
                case "week":
                    _whereList.Add($"YEARWEEK(dt_chat_start, 1) = YEARWEEK(CURDATE(), 1);");
                    break;
                case "all":
                    _whereList.Add("TRUE;");
                    break;
                default:
                    _whereList.Add("TRUE;");
                    break;

            }
            return _whereList;
        }

        private List<string> CreateQueryCurrentConversations(string filter)
        {
            var _whereList = new List<string>();
            switch (filter)
            {
                case "today":
                    _whereList.Add($"DATE(m.dt_message_sent) = CURDATE();");
                    break;
                case "yesterday":
                    _whereList.Add($"DATE(m.dt_message_sent) = CURDATE() - INTERVAL 1 DAY;");
                    break;
                case "month":
                    _whereList.Add($"YEAR(m.dt_message_sent) = YEAR(CURDATE()) AND MONTH(m.dt_message_sent) = MONTH(CURDATE());");
                    break;
                case "week":
                    _whereList.Add($"YEARWEEK(m.dt_message_sent, 1) = YEARWEEK(CURDATE(), 1);");
                    break;
                case "all":
                    _whereList.Add("TRUE;");
                    break;
                default:
                    _whereList.Add("TRUE;");
                    break;

            }
            return _whereList;
        }

    }
}
