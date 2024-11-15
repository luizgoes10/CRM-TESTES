namespace Spill.Core.Web.Models
{
    public class WhatsAppMessage
    {
        public int int_id { get; set; }
        public string str_sender_id { get; set; }
        public string str_receiver_id { get; set; }
        public string txt_content { get; set; }
        public DateTime dt_timestamp { get; set; }
        public int int_status_id { get; set; }
        public string str_chat_id { get; set; }
    }
}
