﻿using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace CEZ.LoymarkTechTest.WebAPI.Features.Countries
{
    [EnableCors("GeneralPolicy")]
    [Route("api/")]
    [ApiController]
    public class CountriesController : Controller
    {
        private IMediator _mediator;
        public CountriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Country
        /// <summary>
        /// Get all Countries
        /// </summary>
        /// <returns></returns>
        [HttpGet("[controller]")] // HTTP GET api/Countries
        [ProducesResponseType(typeof(GetCountriesQuery.QueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<JsonResult> GetCountries()
        {
            var result = await _mediator.Send(new GetCountriesQuery.Query());
            return new JsonResult(result);
        }
        #endregion
    }
}
