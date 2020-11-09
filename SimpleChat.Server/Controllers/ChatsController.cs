using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleChat.Bll.Extensions;
using SimpleChat.Bll.Interfaces;
using SimpleChat.Server.Extensions;
using SimpleChat.Shared.Contracts;

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
        private readonly IChatService service;

        /// <summary>
        /// Constuctor.
        /// </summary>
        /// <param name="service">Injects <see cref="IChatService"/> in controller.</param>
        public ChatsController(IChatService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Gets chats asynchronously.
        /// </summary>
        /// <param name="pagination">Presents data for pagination.</param>
        /// <returns><see cref="IEnumerable{T}"/>Collection of chats.</returns>
        /// <response code="200">Returns chats.</response>           
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Chat>> GetAsync([FromQuery] Pagination pagination) =>
            (await service.GetAllAsync(pagination.Skip, pagination.Top, Bll.Extensions.ChatExtensions.ToModel)).Select(x => x.ToContract());

        /// <summary>
        /// Gets chat by id.
        /// </summary>
        /// <param name="id">Chat id.</param>
        /// <returns>Instance of <see cref="Chat"/>.</returns>
        /// <response code="200">Returns chat.</response>           
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Chat> GetAsync(int id) =>
           (await service.GetByIdAsync(id, x => 
                { 
                    var model = x.ToModel();
                    model.Users = x.UserChats.Select(u => Bll.Extensions.UserExtensions.ToModel(u.User));
                    return model;
                })).ToContract();

        /// <summary>
        /// Creates a chat.
        /// </summary>
        /// <param name="contract">Chat model.</param>
        /// <returns>A newly created chat.</returns>
        /// <response code="201">Returns the newly created chat.</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Chat>> Post([FromBody] Chat contract)
        {
            contract.Id = await service.CreateAsync(contract.ToModel(), Bll.Extensions.ChatExtensions.ToEntity);

            return CreatedAtAction(nameof(GetAsync), new { id = contract.Id }, contract);
        }

        /// <summary>
        /// Updates existent chat.
        /// </summary>
        /// <param name="id">Existent chat id.</param>
        /// <param name="contract">New chat model.</param>
        /// <response code="204">Returns NoContent response.</response>
        /// <response code="400">If the model is null or id does not match the chat model.</response>            
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] Chat contract)
        {
            if (id != contract.Id)
                return BadRequest();

            await service.UpdateAsync(contract.ToModel(), Bll.Extensions.ChatExtensions.ToEntity);

            return NoContent();
        }

        /// <summary>
        /// Deletes existent chat.
        /// </summary>
        /// <param name="id">Existent chat id.</param>
        /// <response code="204">Returns NoContent response.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);

            return NoContent();
        }
    }
}
