using Email_data.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Email_data.Models
{
    public class EmailInteraction
    {
        [Key]
        public int Id { get; set; }

        public required string Direction { get; set; }

        public required string Status { get; set; }

        public string? Queues { get; set; }

        public string? ConversationId { get; set; }

        public string? ReplyToInteractionId { get; set; }
        public string? LatestConvoResBodyPreview { get; set; }
        public bool? Top { get; set; }

        [ForeignKey("EmailInteractionDetailsId")]
        public EmailInteractionDetails EmailInteractionDetails { get; set; } = new();

        [ForeignKey("DraftInteractionId")]
        public EmailInteraction? DraftInteraction { get; set; }
    }


    public class EmailInteractionDetails
    {
        [Key]
        public int Id { get; set; }

        public string GraphId { get; set; } = string.Empty;

        [Column("BccRecipients")]
        public string BccRecipientsJson { get; set; } = string.Empty;

        [NotMapped]
        public List<Recipient> BccRecipients
        {
            get => string.IsNullOrEmpty(BccRecipientsJson)
                ? []
                : JsonConvert.DeserializeObject<List<Recipient>>(BccRecipientsJson) ?? [];
            set => BccRecipientsJson = JsonConvert.SerializeObject(value);
        }
        public EmailBody Body { get; set; }
        public string BodyPreview { get; set; } = string.Empty;
        public ICollection<string> Categories { get; set; }

        [Column("CcRecipients")]
        public string CcRecipientsJson { get; set; } = string.Empty;

        [NotMapped]
        public List<Recipient> CcRecipients
        {
            get => string.IsNullOrEmpty(CcRecipientsJson)
                ? []
                : JsonConvert.DeserializeObject<List<Recipient>>(CcRecipientsJson) ?? [];
            set => CcRecipientsJson = JsonConvert.SerializeObject(value);
        }

        public string ChangeKey { get; set; } = string.Empty;
        public string? ConversationId { get; set; } = string.Empty;
        public string ConversationIndex { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; }
        public FollowupFlag? Flag { get; set; }
        public Recipient From { get; set; }
        public bool HasAttachments { get; set; }
        public string Importance { get; set; } = string.Empty;
        public string InferenceClassification { get; set; } = string.Empty;
        public ICollection<InternetMessageHeader> InternetMessageHeaders { get; set; }
        public string InternetMessageId { get; set; } = string.Empty;
        public bool? IsDeliveryReceiptRequested { get; set; }
        public bool IsDraft { get; set; }
        public bool IsRead { get; set; }
        public bool IsReadReceiptRequested { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        public string? ParentFolderId { get; set; }
        public DateTime? ReceivedDateTime { get; set; }

        [Column("ReplyTo")]
        public string ReplyToJson { get; set; } = string.Empty;

        [NotMapped]
        public List<Recipient> ReplyTo
        {
            get => string.IsNullOrEmpty(ReplyToJson)
                ? []
                : JsonConvert.DeserializeObject<List<Recipient>>(ReplyToJson) ?? [];
            set => ReplyToJson = JsonConvert.SerializeObject(value);
        }

        public Recipient Sender { get; set; }
        public DateTime? SentDateTime { get; set; }
        public string Subject { get; set; } = string.Empty;

        [Column("ToRecipients")]
        public string ToRecipientsJson { get; set; } = string.Empty;

        [NotMapped]
        public List<Recipient> ToRecipients
        {
            get => string.IsNullOrEmpty(ToRecipientsJson)
                ? []
                : JsonConvert.DeserializeObject<List<Recipient>>(ToRecipientsJson) ?? [];
            set => ToRecipientsJson = JsonConvert.SerializeObject(value);
        }

        public EmailBody? UniqueBody { get; set; }
        public string WebLink { get; set; } = string.Empty;
        public ICollection<Attachment> Attachments { get; set; } = [];
    }


    [Owned]
    public class EmailBody
    {
        public string ContentType { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }

    [Owned]
    public class EmailAddressEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    [Owned]
    public class Recipient
    {
        public EmailAddressEntity EmailAddress { get; set; }
    }

    [Owned]
    public class EmailFlag
    {
        public string FlagStatus { get; set; }
    }

    public class FollowupFlag
    {
        [Key]
        public int? Id { get; set; }

        public DateTimeTimeZone? CompletedDateTime { get; set; }
        //public virtual int CompletedDateTimeId { get; set; }

        public DateTimeTimeZone? DueDateTime { get; set; }
        //public virtual int DueDateTimeId { get; set; }
        public string? FlagStatus { get; set; }
        public DateTimeTimeZone? StartDateTime { get; set; }
        //public virtual int StartDateTimeId { get; set; }
    }


    [Owned]
    public class DateTimeTimeZone
    {
        public DateTime? DateTime { get; set; }
        public string? TimeZone { get; set; }
    }

    public class InternetMessageHeader
    {
        [Key]
        public int? Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Value { get; set; } = string.Empty;
    }

    public class Attachment
    {
        [Key]
        public int Id { get; set; }
        public string ContentType { get; set; }
        public bool IsInline { get; set; }
        public string LastModifiedDateTime { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
    }
}
