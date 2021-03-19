using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleChat.Bll.Interfaces;
using SimpleChat.Server.Extensions;
using SimpleChat.Shared.Contracts;
using SimpleChat.Shared.Contracts.Chat;

namespace SimpleChat.Server.Controllers
{
    /// <summary>
    /// Chats controller.
    /// </summary>
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService chatService;

        /// <summary>
        /// Constuctor.
        /// </summary>
        /// <param name="chatService">Injects <see cref="IChatService"/> in controller.</param>
        public ChatsController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        /// <summary>
        /// Gets chats asynchronously.
        /// </summary>
        /// <param name="pagination">Presents data for pagination.</param>
        /// <returns><see cref="IEnumerable{T}"/>Collection of chats.</returns>
        /// <response code="200">Returns chats.</response>
        /// <response code="500">There are any server problems.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<ChatContract>> GetAsync([FromQuery] Pagination pagination) =>
            (await chatService.GetAllAsync(pagination.Skip, pagination.Top))
                .Select(x => x.ToContract());

        /// <summary>
        /// Gets chat by id.
        /// </summary>
        /// <param name="id">Chat id.</param>
        /// <returns>Instance of <see cref="ChatContract"/>.</returns>
        /// <response code="200">Returns chat.</response>       
        /// <response code="404">A chat not found.</response>
        /// <response code="500">There are any server problems.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ChatContract> GetAsync(int id) => (await chatService.GetByIdAsync(id)).ToContract();

        /// <summary>
        /// Creates a chat.
        /// </summary>
        /// <param name="createChatContract">Chat model.</param>
        /// <returns>A newly created chat.</returns>
        /// <response code="201">Returns the newly created chat.</response>
        /// <response code="400">If the item is null</response>        
        /// <response code="500">There are any server problems.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ChatContract>> Post([FromBody] CreateChatContract createChatContract)
        {
            var chatModel = createChatContract.ToModel();

            chatModel.Id = await chatService.CreateAsync(chatModel);

            return CreatedAtAction(nameof(GetAsync), new { id = chatModel.Id }, chatModel.ToContract());
        }

        /// <summary>
        /// Updates existent chat.
        /// </summary>
        /// <param name="id">Existent chat id.</param>
        /// <param name="contract">Updated chat model.</param>
        /// <response code="204">Returns NoContent response.</response>
        /// <response code="400">If the model is null or id does not match the chat model.</response>  
        /// <response code="404">A chat not found.</response>
        /// <response code="500">There are any server problems.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] EditChatContract contract)
        {
            if (id != contract.Id)
                return BadRequest();

            await chatService.UpdateAsync(contract.ToModel());

            return NoContent();
        }

        /// <summary>
        /// Deletes existent chat.
        /// </summary>
        /// <param name="id">Existent chat id.</param>
        /// <response code="204">Returns NoContent response.</response>
        /// <response code="404">A chat not found.</response>
        /// <response code="500">There are any server problems.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await chatService.DeleteAsync(id);

            return NoContent();
        }
    }
}
