
using Business.Handlers.KullaniciYetkiIslevEngels.Commands;
using Business.Handlers.KullaniciYetkiIslevEngels.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// KullaniciYetkiIslevEngels If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class KullaniciYetkiIslevEngelsController : BaseApiController
    {
        ///<summary>
        ///List KullaniciYetkiIslevEngels
        ///</summary>
        ///<remarks>KullaniciYetkiIslevEngels</remarks>
        ///<return>List KullaniciYetkiIslevEngels</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<KullaniciYetkiIslevEngel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetKullaniciYetkiIslevEngelsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>KullaniciYetkiIslevEngels</remarks>
        ///<return>KullaniciYetkiIslevEngels List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(KullaniciYetkiIslevEngel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetKullaniciYetkiIslevEngelQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add KullaniciYetkiIslevEngel.
        /// </summary>
        /// <param name="createKullaniciYetkiIslevEngel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateKullaniciYetkiIslevEngelCommand createKullaniciYetkiIslevEngel)
        {
            var result = await Mediator.Send(createKullaniciYetkiIslevEngel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update KullaniciYetkiIslevEngel.
        /// </summary>
        /// <param name="updateKullaniciYetkiIslevEngel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateKullaniciYetkiIslevEngelCommand updateKullaniciYetkiIslevEngel)
        {
            var result = await Mediator.Send(updateKullaniciYetkiIslevEngel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete KullaniciYetkiIslevEngel.
        /// </summary>
        /// <param name="deleteKullaniciYetkiIslevEngel"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteKullaniciYetkiIslevEngelCommand deleteKullaniciYetkiIslevEngel)
        {
            var result = await Mediator.Send(deleteKullaniciYetkiIslevEngel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
