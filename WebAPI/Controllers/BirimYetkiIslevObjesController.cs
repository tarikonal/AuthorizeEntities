
using Business.Handlers.BirimYetkiIslevObjes.Commands;
using Business.Handlers.BirimYetkiIslevObjes.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// BirimYetkiIslevObjes If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BirimYetkiIslevObjesController : BaseApiController
    {
        ///<summary>
        ///List BirimYetkiIslevObjes
        ///</summary>
        ///<remarks>BirimYetkiIslevObjes</remarks>
        ///<return>List BirimYetkiIslevObjes</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BirimYetkiIslevObje>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetBirimYetkiIslevObjesQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>BirimYetkiIslevObjes</remarks>
        ///<return>BirimYetkiIslevObjes List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BirimYetkiIslevObje))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetBirimYetkiIslevObjeQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add BirimYetkiIslevObje.
        /// </summary>
        /// <param name="createBirimYetkiIslevObje"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBirimYetkiIslevObjeCommand createBirimYetkiIslevObje)
        {
            var result = await Mediator.Send(createBirimYetkiIslevObje);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update BirimYetkiIslevObje.
        /// </summary>
        /// <param name="updateBirimYetkiIslevObje"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBirimYetkiIslevObjeCommand updateBirimYetkiIslevObje)
        {
            var result = await Mediator.Send(updateBirimYetkiIslevObje);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete BirimYetkiIslevObje.
        /// </summary>
        /// <param name="deleteBirimYetkiIslevObje"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteBirimYetkiIslevObjeCommand deleteBirimYetkiIslevObje)
        {
            var result = await Mediator.Send(deleteBirimYetkiIslevObje);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
