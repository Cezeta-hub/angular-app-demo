using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace CEZ.AngularDemo.WebAPI.Features.History
{
    [EnableCors("GeneralPolicy")]
    [Route("api/")]
    [ApiController]
    public class HistoryController : Controller
    {
        private IMediator _mediator;
        public HistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region History
        /// <summary>
        /// Get History paginated
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("[controller]")] // HTTP GET api/Users
        [ProducesResponseType(typeof(GetHistoryPaginatedQuery.QueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<JsonResult> GetHistoryPaginated([FromQuery] GetHistoryPaginatedQuery.Query query)
        {
            var result = await _mediator.Send(query);
            return new JsonResult(result);
        }
        #endregion
    }
}
