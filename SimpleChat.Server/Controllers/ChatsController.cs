using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleChat.Bll.Interfaces;
using SimpleChat.Server.Extensions;
using SimpleChat.Shared.Contracts;

namespace SimpleChat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService service;

        public ChatsController(IChatService service)
        {
            this.service = service;
        }

        // GET: api/<ChatController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Chat>> GetAsync([FromQuery] Pagination pagination) =>
            (await service.GetAllAsync(pagination.Skip, pagination.Top)).Select(x => x.ToContract());

        // GET api/<ChatController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Chat> GetAsync(int id) =>
           (await service.GetByIdAsync(id)).ToContract();

        // POST api/<ChatController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Chat>> Post([FromBody] Chat model)
        {
            model.Id = await service.CreateAsync(model.ToModel());

            return CreatedAtAction(nameof(GetAsync), new { id = model.Id }, model);
        }

        // PUT api/<ChatController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] Chat contract)
        {
            if (id != contract.Id)
                return BadRequest();

            await service.UpdateAsync(contract.ToModel());

            return NoContent();
        }

        // DELETE api/<ChatController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);

            return NoContent();
        }
    }
}
