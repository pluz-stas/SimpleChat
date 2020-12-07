using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SimpleChat.Bll.Extensions;
using SimpleChat.Bll.Interfaces;
using SimpleChat.Server.Extensions;
using SimpleChat.Server.Hub;
using SimpleChat.Shared.Contracts;
using SimpleChat.Shared.Hub;

namespace SimpleChat.Server.Controllers
{
    /// <summary>
    /// Messages controller.
    /// </summary>
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService messageService;
        private readonly IHubContext<ChatHub> hubContext;

        /// <summary>
        /// Constuctor.
        /// </summary>
        /// <param name="messageService">Injects <see cref="IMessageService"/> in controller.</param>
        /// <param name="hubContext">Injects <see cref="IHubContext{ChatHub}"/> in controller.</param>
        public MessagesController(IMessageService messageService, IHubContext<ChatHub> hubContext)
        {
            this.messageService = messageService;
            this.hubContext = hubContext;
        }

        /// <summary>
        /// Gets message by id.
        /// </summary>
        /// <param name="id">Message id.</param>
        /// <returns>Instance of <see cref="Message"/>.</returns>
        /// <response code="200">Returns message.</response>           
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Message> GetAsync(int id)
        {
            var messageModel = await messageService.GetByIdAsync(id);

            var messageContract = messageModel.ToContract();
            messageContract.Chat = messageModel.Chat.ToContract();

            return messageContract;
        }

        /// <summary>
        /// Creates a message.
        /// </summary>
        /// <param name="contract">Message model.</param>
        /// <returns>A newly created message.</returns>
        /// <response code="201">Returns the newly created message.</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Message>> Post([FromBody] Message contract)
        {
            var messageModel = contract.ToModel();
            messageModel.Chat = contract.Chat.ToModel();

            var sendHubMessageTask = hubContext.Clients.Group(messageModel.Chat.Id.ToString()).SendAsync(HubConstants.ReceiveMessage, messageModel);
            var writeMessageToDbTask = messageService.CreateAsync(messageModel);

            await Task.WhenAll(sendHubMessageTask, writeMessageToDbTask);

            contract.Id = writeMessageToDbTask.Result;

            return CreatedAtAction("Get", new { id = contract.Id }, contract);
        }

        /// <summary>
        /// Updates existent message.
        /// </summary>
        /// <param name="id">Existent message id.</param>
        /// <param name="contract">New message model.</param>
        /// <response code="204">Returns NoContent response.</response>
        /// <response code="400">If the model is null or id does not match the message model.</response>            
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] Message contract)
        {
            if (id != contract.Id)
                return BadRequest();

            var messageModel = contract.ToModel();
            messageModel.Chat = contract.Chat.ToModel();

            await messageService.UpdateAsync(messageModel);

            return NoContent();
        }

        /// <summary>
        /// Deletes existent message.
        /// </summary>
        /// <param name="id">Existent message id.</param>
        /// <response code="204">Returns NoContent response.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await messageService.DeleteAsync(id);

            return NoContent();
        }
    }
}
