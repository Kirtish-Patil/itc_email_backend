using Email_data.Configs;
using Email_data.Dtos;
using Email_data.Models;
using Email_data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;


namespace Email_data.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmailInteractionController : ControllerBase
    {
        private static readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly EmailInteractionDbContext _context;
        private readonly EmailInteractionService _service;

        public EmailInteractionController(EmailInteractionDbContext context, EmailInteractionService service)
        {
            _context = context;
            logger.Info("Db :",_context);
            _service = service;
        }

        [HttpPost("createInboundInteractions")]
        public async Task<ActionResult> RecieveEmail([FromBody] GraphApiMsgResourceType[] emails)
        {
            //var interactions = _service.CreateInboundInteractionArr(emails);
            emails = emails.OrderBy(i => i.ReceivedDateTime).ToArray();

            try
            {
                logger.Info("inserting emails to db");

                foreach (var item in emails)
                {
                    var interaction = _service.CreateInboundInteraction(item);
                    _context.EmailInteractions.Add(interaction);
                    _context.SaveChanges();
                }
                logger.Info("done inserting emails to db");

                return Ok(new { message = "Saved!" });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception occurred in RecieveEmail method");
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet("getInteractions")]
        public async Task<IActionResult> GetInteractions([FromQuery] int pageNo = 1, int recPerPage = 10)
        {
            return await _service.GetInboundInteractions(this, pageNo, recPerPage);
        }

        [HttpGet("getConversation/{id}")]
        public async Task<IActionResult> GetConversation([FromRoute] string id)
        {
            return await _service.GetConversation(this, id);
        }
    }
}
