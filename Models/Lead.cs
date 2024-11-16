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
    public class Visit
    {
        public int int_id { get; set; }
        public string str_user_id { get; set; }
        public string str_page { get; set; }
        public DateTime dt_visit_time { get; set; }
    }

    public class Campaign
    {
        public int int_id { get; set; }
        public string str_name { get; set; }
        public string str_description { get; set; }
        public List<string> target_pages { get; set; }
        public string str_messsage_templates { get; set; }
    }
}
