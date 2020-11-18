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
    /// Messages controller.
    /// </summary>
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService service;

        /// <summary>
        /// Constuctor.
        /// </summary>
        /// <param name="service">Injects <see cref="IMessageService"/> in controller.</param>
        public MessagesController(IMessageService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Gets messages asynchronously.
        /// </summary>
        /// <param name="pagination">Presents data for pagination.</param>
        /// <returns><see cref="IEnumerable{T}"/>Collection of messages.</returns>
        /// <response code="200">Returns messages.</response>           
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Message>> GetAsync([FromQuery] Pagination pagination) =>
            (await service.GetAllAsync(pagination.Skip, pagination.Top, Bll.Extensions.MessageExtensions.ToModel)).Select(x => x.ToContract());

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
            var message = await service.GetByIdAsync(id, Bll.Extensions.MessageExtensions.ToModel);
            return message.ToContract();
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
            contract.Id = await service.CreateAsync(contract.ToModel(), Bll.Extensions.MessageExtensions.ToEntity);

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

            await service.UpdateAsync(contract.ToModel(), Bll.Extensions.MessageExtensions.ToEntity);

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
            await service.DeleteAsync(id);

            return NoContent();
        }
    }
}
