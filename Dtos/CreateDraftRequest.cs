using Email_data.Models;

namespace Email_data.Dtos
{
    public class CreateDraftRequest
    {
        public  List<Recipient>? ReplyTo { get; set; } = new List<Recipient>();
        public List<Recipient>? ToRecipients { get; set; } = new List<Recipient>();
        public string? ConversationID { get; set; }
        public List<Recipient>? CCRecipients { get; set; } = new List<Recipient>();
    }
}
