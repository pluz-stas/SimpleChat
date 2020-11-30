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
    /// Users controller.
    /// </summary>
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService service;

        /// <summary>
        /// Constuctor.
        /// </summary>
        /// <param name="service">Injects <see cref="IUserService"/> in controller.</param>
        public UsersController(IUserService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Gets user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>Instance of <see cref="User"/>.</returns>
        /// <response code="200">Returns user.</response>           
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<User> GetAsync(int id)
        {
            var userModel = await service.GetByIdAsync(id);
            var userContract = userModel.ToContract();
            //if user == httpcontext.Session.User
            userContract.Chats = userModel.Chats.Select(x => x.ToContract());
            
            return userContract;
        }

        /// <summary>
        /// Gets users by Chat id.
        /// </summary>
        /// <param name="id">Chat id.</param>
        /// <param name="pagination">Presents data for pagination.</param>
        /// <returns><see cref="IEnumerable{T}"/>Collection of users.</returns>
        /// <response code="200">Returns user.</response>           
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<User>> GetByChatAsync(int id, [FromQuery] Pagination pagination) =>
            (await service.GetByChatIdAsync(id, pagination.Skip, pagination.Top)).Select(x => x.ToContract());


        /// <summary>
        /// Updates existent user.
        /// </summary>
        /// <param name="id">Existent user id.</param>
        /// <param name="contract">New user model.</param>
        /// <response code="204">Returns NoContent response.</response>
        /// <response code="400">If the model is null or id does not match the user model.</response>            
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] User contract)
        {
            if (id != contract.Id)
                return BadRequest();

            await service.UpdateAsync(contract.ToModel(), Bll.Extensions.UserExtensions.ToEntity);

            return NoContent();
        }

        /// <summary>
        /// Deletes existent user.
        /// </summary>
        /// <param name="id">Existent user id.</param>
        /// <response code="204">Returns NoContent response.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);

            return NoContent();
        }
        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="contract">User model.</param>
        /// <returns>A newly created user.</returns>
        /// <response code="201">Returns the newly created user.</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Post([FromBody] User contract)
        {
            var userModel = contract.ToModel();

            //userModel.Sessions = "some random session";

            contract.Id = await service.CreateAsync(userModel);

            return CreatedAtAction("Get", new { id = contract.Id }, contract);
        }
    }
}
