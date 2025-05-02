using Email_data.Models;

namespace Email_data.Dtos
{
    public class CreateDraftRequest
    {
        public  List<Recipient>? ToRecipients { get; set; } = new List<Recipient>();
        public List<Recipient>? CCRecipients { get; set; } = new List<Recipient>();
        public string? ConversationID { get; set; }
        public int? ReplyToInteractionId { get; set; }
        public List<Recipient>? BCCRecipients { get; set; } = new List<Recipient>();
        public required Recipient Sender { get; set; }
    }
}
