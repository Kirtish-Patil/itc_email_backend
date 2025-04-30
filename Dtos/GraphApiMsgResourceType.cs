using Email_data.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Email_data.Dtos
{
    public class GraphApiMsgResourceType
    {
        [JsonPropertyName("@odata.etag")]
        public string OdataEtag { get; set; }
        public string Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        public string ChangeKey { get; set; }
        public List<string> Categories { get; set; }
        public DateTime ReceivedDateTime { get; set; }
        public DateTime SentDateTime { get; set; }
        public bool HasAttachments { get; set; }
        public string InternetMessageId { get; set; }
        public string Subject { get; set; }
        public string BodyPreview { get; set; }
        public string Importance { get; set; }
        public string ParentFolderId { get; set; }
        public string ConversationId { get; set; }
        public string ConversationIndex { get; set; }
        public bool? IsDeliveryReceiptRequested { get; set; }
        public bool IsReadReceiptRequested { get; set; }
        public bool IsRead { get; set; }
        public bool IsDraft { get; set; }
        public string WebLink { get; set; }
        public string InferenceClassification { get; set; }
        public Body Body { get; set; }
        public Recipient Sender { get; set; }
        public Recipient From { get; set; }
        public List<Recipient> ToRecipients { get; set; }
        public List<Recipient> CcRecipients { get; set; }
        public List<Recipient> BccRecipients { get; set; }
        public List<Recipient> ReplyTo { get; set; }
        public Flag Flag { get; set; }
    }

    public class Body
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
    }

    public class EmailAddress
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class Flag
    {
        public string FlagStatus { get; set; }
    }

    public class From
    {
        public EmailAddress EmailAddress { get; set; }
    }

    public class Sender
    {
        public EmailAddress EmailAddress { get; set; }
    }

    public class ToRecipient
    {
        public EmailAddress EmailAddress { get; set; }
    }
}
