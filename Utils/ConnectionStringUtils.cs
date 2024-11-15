namespace Spill.Core.Web.Utils
{
    public static class ConnectionStringUtils
    {
        private const string ConnectionStringApplication = "server=localhost;database=spill_db_users;user id=root;password=Ber12@05;";


        public static string ConnectionString { get { return ConnectionStringApplication; } }
    }
}
