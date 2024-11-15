using Spill.Core.Web.Components.Pages;
using Spill.Core.Web.DBModels.Interfaces;
using Spill.Core.Web.Models;
using System.Data.Common;

namespace Spill.Core.Web.DBModels
{
    public class LeadService : ILeadService
    {
        private readonly IDatabaseService _databaseService;

        public LeadService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<int> GetWonLeadsCountAsync(string filter)
        {
            var query = @$"SELECT COUNT(*) FROM leads WHERE int_status_id = (SELECT int_id FROM status WHERE str_name = 'closed_won') AND {CreateQuery(filter).FirstOrDefault()}";
            return await _databaseService.QuerySingleOrDefaultAsync<int>(query);
        }

        public async Task<int> GetActiveLeadsCountAsync(string filter)
        {
            var query = @$"SELECT COUNT(*) FROM leads WHERE int_status_id = (SELECT int_id FROM status WHERE str_name = 'new') AND {CreateQuery(filter).FirstOrDefault()}";
            return await _databaseService.QuerySingleOrDefaultAsync<int>(query);
        }

        public async Task<int> GetLostLeadsCountAsync(string filter)
        {
            var query = @$"SELECT COUNT(*) FROM leads WHERE int_status_id = (SELECT int_id FROM status WHERE str_name = 'closed_lost') AND {CreateQuery(filter).FirstOrDefault()}";
            return await _databaseService.QuerySingleOrDefaultAsync<int>(query);
        }

        public async Task<List<Lead>> GetLeadsAsync()
        {
            var sql = "SELECT int_id, str_name, str_email, str_phone, dt_created, int_status_id FROM Leads;";
            var leads = await _databaseService.QueryAsync<Lead>(sql);
            return leads.ToList();
        }

        public async Task<int> CreateLead(Lead lead)
        {
            var sql = "INSERT INTO leads (str_name, str_email, str_phone, int_status_id) VALUES (@str_name, @str_email, @str_phone, @int_status_id)";
            return await _databaseService.ExecuteAsync(sql, lead);
        }
        public async Task<int> UpdateLeadStatus(Lead lead)
        {
            var sql = "UPDATE leads SET int_status_id = @int_status_id WHERE int_id = @int_id";
            return await _databaseService.ExecuteAsync(sql, lead);
        }
        private List<string> CreateQuery(string filter)
        {
            var _whereList = new List<string>();
            switch (filter)
            {
                case "today":
                    _whereList.Add($"DATE(dt_created) = CURDATE();");
                    break;
                case "yesterday":
                    _whereList.Add($"DATE(dt_created) = CURDATE() - INTERVAL 1 DAY;");
                    break;
                case "month":
                    _whereList.Add($"YEAR(dt_created) = YEAR(CURDATE()) AND MONTH(dt_created) = MONTH(CURDATE());");
                    break;
                case "week":
                    _whereList.Add($"YEARWEEK(dt_created, 1) = YEARWEEK(CURDATE(), 1);");
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
