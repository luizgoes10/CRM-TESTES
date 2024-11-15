namespace Spill.Core.Web.Models
{
    public class Lead
    {
        public int int_id { get; set; }
        public string str_name { get; set; }
        public string str_email { get; set; }
        public string str_phone { get; set; }
        public DateTime dt_created { get; set; }
        public int int_status_id { get; set; }
    }
}
