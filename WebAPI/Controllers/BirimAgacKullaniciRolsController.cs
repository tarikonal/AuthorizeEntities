
using Business.Handlers.BirimAgacKullaniciRols.Commands;
using Business.Handlers.BirimAgacKullaniciRols.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// BirimAgacKullaniciRols If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BirimAgacKullaniciRolsController : BaseApiController
    {
        ///<summary>
        ///List BirimAgacKullaniciRols
        ///</summary>
        ///<remarks>BirimAgacKullaniciRols</remarks>
        ///<return>List BirimAgacKullaniciRols</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BirimAgacKullaniciRol>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetBirimAgacKullaniciRolsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>BirimAgacKullaniciRols</remarks>
        ///<return>BirimAgacKullaniciRols List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BirimAgacKullaniciRol))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetBirimAgacKullaniciRolQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add BirimAgacKullaniciRol.
        /// </summary>
        /// <param name="createBirimAgacKullaniciRol"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBirimAgacKullaniciRolCommand createBirimAgacKullaniciRol)
        {
            var result = await Mediator.Send(createBirimAgacKullaniciRol);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update BirimAgacKullaniciRol.
        /// </summary>
        /// <param name="updateBirimAgacKullaniciRol"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBirimAgacKullaniciRolCommand updateBirimAgacKullaniciRol)
        {
            var result = await Mediator.Send(updateBirimAgacKullaniciRol);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete BirimAgacKullaniciRol.
        /// </summary>
        /// <param name="deleteBirimAgacKullaniciRol"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteBirimAgacKullaniciRolCommand deleteBirimAgacKullaniciRol)
        {
            var result = await Mediator.Send(deleteBirimAgacKullaniciRol);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
