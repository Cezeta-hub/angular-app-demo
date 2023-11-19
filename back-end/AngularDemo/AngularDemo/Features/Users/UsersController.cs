using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CEZ.AngularDemo.WebAPI.Features.Users
{
    [EnableCors("GeneralPolicy")]
    [Route("api/")]
    [ApiController]
    public class UsersController : Controller
    {
        private IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region User
        /// <summary>
        /// Get Users paginated
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("[controller]")] // HTTP GET api/Users
        [ProducesResponseType(typeof(GetUsersPaginatedQuery.QueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<JsonResult> GetUsersPaginated([FromQuery] GetUsersPaginatedQuery.Query query)
        {
            var result = await _mediator.Send(query);
            return new JsonResult(result);
        }

        /// <summary>
        /// Get User corresponding to the provided Id 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("[controller]/{Id}")] // HTTP GET api/Users/{id}
        [ProducesResponseType(typeof(GetUserByIdQuery.QueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<JsonResult> GetUser([FromRoute] GetUserByIdQuery.Query query)
        {
            var result = await _mediator.Send(query);
            return new JsonResult(result);
        }

        /// <summary>
        /// Create a new User
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[controller]")] // HTTP POST api/Users
        [ProducesResponseType(typeof(CreateUserCommand.CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<JsonResult> CreateUser([FromBody] CreateUserCommand.Command command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }

        /// <summary>
        /// Update an existing User
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[controller]")] // HTTP PUT api/Users
        [ProducesResponseType(typeof(UpdateUserCommand.CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<JsonResult> UpdateUser([FromBody] UpdateUserCommand.Command command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("[controller]/{Id}")] // HTTP DELETE api/Users/{id}
        [ProducesResponseType(typeof(DeleteUserCommand.CommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<JsonResult> DeleteUser([FromRoute] DeleteUserCommand.Command command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        #endregion
    }
}
