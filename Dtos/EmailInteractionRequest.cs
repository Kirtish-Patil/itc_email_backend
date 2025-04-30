using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Email_data.Models;

namespace Email_data.Dtos
{
    public class EmailInteractionRequest
    {
        [JsonPropertyName("threadId")]
        public string? ThreadId { get; set; }

        [Required]
        [JsonPropertyName("direction")]
        public string Direction { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("emailInteractionDetails")]
        public EmailInteractionDetails EmailInteractionDetails { get; set; }
    }
}
