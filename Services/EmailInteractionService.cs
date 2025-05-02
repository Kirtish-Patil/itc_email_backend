
using Azure;
using Email_data.Configs;
using Email_data.Constanst;
using Email_data.Controllers;
using Email_data.Dtos;
using Email_data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using NLog;
using System.Linq;
using System.Threading.Tasks;

namespace Email_data.Services
{
    public class EmailInteractionService
    {
        private readonly HttpClient _httpClient;
        private readonly EmailInteractionDbContext _context;
        private readonly IConfiguration _configuration;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EmailInteractionService(HttpClient httpClient, EmailInteractionDbContext context, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _context = context;
            _configuration = configuration;
        }

        public List<EmailInteraction> CreateInboundInteractionArr(GraphApiMsgResourceType[] msgs)
        {
            List<EmailInteraction> interactions = new();
            foreach (var msg in msgs)
            {
                var interaction = new EmailInteraction()
                {
                    Direction = InteractionDirection.INBOUND,
                    Status = InteractionStatus.OPEN,
                    EmailInteractionDetails = new EmailInteractionDetails()
                    {
                        Attachments = [],
                        BccRecipients = msg.BccRecipients,
                        Body = new EmailBody
                        {
                            Content = msg.Body.Content,
                            ContentType = msg.Body.ContentType,
                        },
                        BodyPreview = msg.BodyPreview,
                        Categories = msg.Categories,
                        CcRecipients = msg.CcRecipients,
                        ChangeKey = msg.ChangeKey,
                        CreatedDateTime = msg.CreatedDateTime,
                        ConversationId = msg.ConversationId,
                        Flag = new FollowupFlag
                        {
                            FlagStatus = msg.Flag.FlagStatus,
                            StartDateTime = null,
                            CompletedDateTime = null,
                            DueDateTime = null
                        },
                        ConversationIndex = msg.ConversationIndex,
                        From = msg.From,
                        HasAttachments = msg.HasAttachments,
                        Importance = msg.Importance,
                        InferenceClassification = msg.InferenceClassification,
                        InternetMessageId = msg.InternetMessageId,
                        IsDraft = msg.IsDraft,
                        IsRead = msg.IsRead,
                        IsDeliveryReceiptRequested = msg.IsDeliveryReceiptRequested,
                        IsReadReceiptRequested = msg.IsReadReceiptRequested,
                        LastModifiedDateTime = msg.LastModifiedDateTime,
                        ParentFolderId = msg.ParentFolderId,
                        ReplyTo = msg.ReplyTo,
                        Sender = msg.Sender,
                        SentDateTime = msg.SentDateTime,
                        Subject = msg.Subject,
                        GraphId = msg.Id,
                        WebLink = msg.WebLink,
                        ReceivedDateTime = msg.ReceivedDateTime,
                    }
                };

                interactions.Add(interaction);
            }

            return interactions;
        }

        public EmailInteraction CreateInboundInteraction(GraphApiMsgResourceType msg)
        {
            var existingConversation = _context.EmailInteractions
                    .Where(i => i.ConversationId == msg.ConversationId)
                    .FirstOrDefault();

            var interaction = new EmailInteraction()
            {
                Direction = InteractionDirection.INBOUND,
                Status = InteractionStatus.OPEN,
                Top = existingConversation == null ? true : false,
                ConversationId = msg.ConversationId,
                EmailInteractionDetails = new EmailInteractionDetails()
                {
                    Attachments = [],
                    BccRecipients = msg.BccRecipients,
                    Body = new EmailBody
                    {
                        Content = msg.Body.Content,
                        ContentType = msg.Body.ContentType,
                    },
                    BodyPreview = msg.BodyPreview,
                    Categories = msg.Categories,
                    CcRecipients = msg.CcRecipients,
                    ChangeKey = msg.ChangeKey,
                    CreatedDateTime = msg.CreatedDateTime,
                    ConversationId = msg.ConversationId,
                    Flag = new FollowupFlag
                    {
                        FlagStatus = msg.Flag.FlagStatus,
                        StartDateTime = null,
                        CompletedDateTime = null,
                        DueDateTime = null
                    },
                    ConversationIndex = msg.ConversationIndex,
                    From = msg.From,
                    HasAttachments = msg.HasAttachments,
                    Importance = msg.Importance,
                    InferenceClassification = msg.InferenceClassification,
                    InternetMessageId = msg.InternetMessageId,
                    IsDraft = msg.IsDraft,
                    IsRead = msg.IsRead,
                    IsDeliveryReceiptRequested = msg.IsDeliveryReceiptRequested,
                    IsReadReceiptRequested = msg.IsReadReceiptRequested,
                    LastModifiedDateTime = msg.LastModifiedDateTime,
                    ParentFolderId = msg.ParentFolderId,
                    ReplyTo = msg.ReplyTo,
                    Sender = msg.Sender,
                    SentDateTime = msg.SentDateTime,
                    Subject = msg.Subject,
                    GraphId = msg.Id,
                    WebLink = msg.WebLink,
                    ReceivedDateTime = msg.ReceivedDateTime,
                }
            };

            return interaction;
        }

        public async Task<IActionResult> GetInboundInteractions(EmailInteractionController controller, int pageNo = 1, int recPerPage = 10)
        {
            var response = new GenericApiResponse<List<EmailInteraction>>();

            try
            {
                var interactions = await _context.EmailInteractions
                .Include(i => i.EmailInteractionDetails)
                .Include(i => i.DraftInteraction)
                .Include(i => i.EmailInteractionDetails.Flag)
                .OrderBy(i => i.EmailInteractionDetails.CreatedDateTime)
                .Where(i => i.Direction == InteractionDirection.INBOUND && i.Top == true)
                .Skip(recPerPage * (pageNo - 1))
                .Take(recPerPage)
                .ToListAsync();

                if (interactions == null)
                {
                    response.Status = 500;
                    response.Message = "Something went wrong while fetching interactions";
                    response.Data = [];
                    return controller.StatusCode(500, response);
                }



                response.Status = 200;
                response.Message = "Interaction found successfully";
                response.Data = interactions;
                return controller.Ok(response);
            }
            catch (Exception ex)
            {
                logger.Error("Something went wrong while getting interactions", ex);
                response.Status = 500;
                response.Message = "Something went wrong while getting interactions";
                response.Data = null;
                return controller.StatusCode(500, response);
            }
        }

        public async Task<IActionResult> GetConversation(EmailInteractionController controller, string conversationId)
        {
            var response = new GenericApiResponse<List<EmailInteraction>>();
            try
            {
                var interactions = await _context.EmailInteractions
                    .Include(i => i.EmailInteractionDetails)
                    .Include(i => i.DraftInteraction)
                    .Include(i => i.EmailInteractionDetails.Flag)
                    .Include(i => i.DraftInteraction.EmailInteractionDetails)
                    .OrderBy(i => i.EmailInteractionDetails.ReceivedDateTime)
                    .Where(i => i.EmailInteractionDetails.ConversationId == conversationId
                    && !i.EmailInteractionDetails.IsDraft)
                    .ToListAsync();

                response.Status = 200;
                response.Message = "Conversation retrieved successfully";
                response.Data = interactions;
                return controller.Ok(response);
            }
            catch (Exception ex)
            {
                logger.Error("Something went wrong while getting conversation", ex);
                response.Status = 500;
                response.Message = "Something went wrong while getting conversation";
                response.Data = null;
                return controller.StatusCode(500, response);
            }
        }

        public async Task<IActionResult> CreateDraftInteraction(EmailInteractionController controller, CreateDraftRequest req)
        {
            var response = new GenericApiResponse<EmailInteraction>();
            var bccRecipients = req.BCCRecipients ?? new List<Recipient>();
            var ccRecipient = req.CCRecipients ?? new List<Recipient>();
            var toRecipients = req.ToRecipients ?? new List<Recipient>();
            string? convoId = req.ConversationID;
            var sender = new Recipient()
            {
                EmailAddress = req.Sender.EmailAddress,
            };

            var from = new Recipient()
            {
                EmailAddress = req.Sender.EmailAddress,
            };
            var draftInteraction = new EmailInteraction()
            {
                Direction = InteractionDirection.OUTBOUND,
                Status = InteractionStatus.OPEN,
                ConversationId = req.ConversationID,
                ReplyToInteractionId = null,
                EmailInteractionDetails = new EmailInteractionDetails()
                {
                    Attachments = [],
                    BccRecipients = bccRecipients,
                    Body = new EmailBody
                    {
                        Content = string.Empty,
                        ContentType = "html",
                    },
                    BodyPreview = string.Empty,
                    Categories = [],
                    CcRecipients = ccRecipient,
                    ChangeKey = "",
                    CreatedDateTime = DateTime.Now,
                    ConversationId = convoId,
                    Flag = new FollowupFlag
                    {
                        FlagStatus = FollowupFlagStatus.NOTFLAGGED,
                        StartDateTime = null,
                        CompletedDateTime = null,
                        DueDateTime = null
                    },
                    ConversationIndex = "",
                    From = from,
                    HasAttachments = false,
                    Importance = MsgImportance.NORMAL,
                    InferenceClassification = "",
                    InternetMessageId = "",
                    IsDraft = true,
                    IsRead = false,
                    IsDeliveryReceiptRequested = false,
                    IsReadReceiptRequested = false,
                    LastModifiedDateTime = DateTime.Now,
                    ParentFolderId = null,
                    ReplyTo = bccRecipients,
                    Sender = sender,
                    SentDateTime = null,
                    Subject = "",
                    WebLink = "",
                    ReceivedDateTime = null,
                    ToRecipients = toRecipients,
                }
            };

            try
            {
                EmailInteraction? message = null;
                EmailInteraction? existingReply = null;

                if (req.Id != null && req.ConversationID != null)
                {
                    message = await _context.EmailInteractions.Include(i => i.DraftInteraction).Where(i => i.Id == req.ReplyToInteractionId).FirstOrDefaultAsync();

                    if (message != null)
                    {
                        if (message.DraftInteraction != null)
                        {
                            response.Status = 200;
                            response.Message = "Existing Reply Found Successfully";
                            response.Data = message.DraftInteraction;
                            return controller.Ok(response);
                        }
                        else
                        {
                            var msg = await _context.EmailInteractions.AddAsync(draftInteraction);
                            logger.Debug("EmailInteractionDetail added successfully", msg);
                            message.DraftInteraction = draftInteraction;
                            await _context.SaveChangesAsync();

                            response.Status = 200;
                            response.Message = "New Draft Created for Parent";
                            response.Data = draftInteraction;
                            return controller.Ok(response)
                        }
                    }
                    else
                    {
                        _context.EmailInteractions.Remove(draftInteraction);
                        await _context.SaveChangesAsync();
                        logger.Error("Draft was created but couldn't update parent interaction");
                        logger.Error("Failed to create Draft");
                        throw new Exception("Failed to create Draft");
                    }
                }else
                {
                    var msg = await _context.EmailInteractions.AddAsync(draftInteraction);
                    logger.Debug("EmailInteractionDetail added successfully", msg);
                    await _context.SaveChangesAsync();


                    response.Status = 201;
                    response.Message = "reply Interaction created Successfully";
                    response.Data = draftInteraction;
                    return controller.Ok(response);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
