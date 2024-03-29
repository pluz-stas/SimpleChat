﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SimpleChat.Bll.Interfaces;
using SimpleChat.Server.Extensions;
using SimpleChat.Server.Hub;
using SimpleChat.Shared.Contracts;
using SimpleChat.Shared.Contracts.Message;
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
        /// Gets messages by chatId.
        /// </summary>
        /// <param name="chatId">Chat id</param>
        /// <param name="pagination">Presents data for pagination.</param>
        /// <returns><see cref="IEnumerable{MessageContract}"/>Collection of messages.</returns>
        /// <response code="200">Returns messages.</response>
        /// <response code="500">There are any server problems.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<MessageContract>> GetByChatAsync(int chatId, [FromQuery] Pagination pagination) =>
            (await messageService.GetByChatAsync(chatId, pagination.Skip, pagination.Top))
            .Select(x => x.ToContract());

        /// <summary>
        /// Gets message by id.
        /// </summary>
        /// <param name="id">Message id.</param>
        /// <returns>Instance of <see cref="MessageContract"/>.</returns>
        /// <response code="200">Returns message.</response>           
        /// <response code="404">A message not found.</response>
        /// <response code="500">There are any server problems.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<MessageContract> GetAsync(int id) => (await messageService.GetByIdAsync(id)).ToContract();

        /// <summary>
        /// Creates a message.
        /// </summary>
        /// <param name="chatId">Chat Id.</param>
        /// <param name="contract">Message model.</param>
        /// <response code="204">Message created.</response>
        /// <response code="400">If the item is null</response>   
        /// <response code="500">There are any server problems.</response>
        [HttpPost("{chatId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(int chatId, [FromBody] CreateMessageContract contract)
        {
            var messageModel = contract.ToModel();
            messageModel.Chat.Id = chatId;

            var messageContract = messageModel.ToContract();
            messageContract.User = contract.User;

            var sendHubMessageTask = hubContext.Clients.Group(chatId.ToString()).SendAsync(HubConstants.ReceiveMessage, messageContract);
            var writeMessageToDbTask = messageService.CreateAsync(messageModel);

            await Task.WhenAll(sendHubMessageTask, writeMessageToDbTask);

            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Updates existent message.
        /// </summary>
        /// <param name="id">Existent message id.</param>
        /// <param name="contract">New message model.</param>
        /// <response code="204">Returns NoContent response.</response>
        /// <response code="400">If the model is null or id does not match the message model.</response>         
        /// <response code="404">A message not found.</response>
        /// <response code="500">There are any server problems.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] EditMessageContract contract)
        {
            if (id != contract.Id)
                return BadRequest();

            await messageService.UpdateAsync(contract.ToModel());

            return NoContent();
        }

        /// <summary>
        /// Deletes existent message.
        /// </summary>
        /// <param name="id">Existent message id.</param>
        /// <response code="204">Returns NoContent response.</response>
        /// <response code="404">A message not found.</response>
        /// <response code="500">There are any server problems.</response>        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await messageService.DeleteAsync(id);

            return NoContent();
        }
    }
}
