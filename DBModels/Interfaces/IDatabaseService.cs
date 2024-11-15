using System.Data;

namespace Spill.Core.Web.DBModels.Interfaces
{
    public interface IDatabaseService
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null);
        Task<T> QuerySingleOrDefaultAsync<T>(string sql, object parameters = null);
        Task<int> ExecuteAsync(string sql, object parameters = null);
        Task<T> ExecuteScalarAsync<T>(string sql, object parameters = null);

    }
}
