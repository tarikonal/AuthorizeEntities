
using Business.Handlers.KullaniciYetkiIslevObjes.Commands;
using Business.Handlers.KullaniciYetkiIslevObjes.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// KullaniciYetkiIslevObjes If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class KullaniciYetkiIslevObjesController : BaseApiController
    {
        ///<summary>
        ///List KullaniciYetkiIslevObjes
        ///</summary>
        ///<remarks>KullaniciYetkiIslevObjes</remarks>
        ///<return>List KullaniciYetkiIslevObjes</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<KullaniciYetkiIslevObje>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetKullaniciYetkiIslevObjesQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>KullaniciYetkiIslevObjes</remarks>
        ///<return>KullaniciYetkiIslevObjes List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(KullaniciYetkiIslevObje))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetKullaniciYetkiIslevObjeQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add KullaniciYetkiIslevObje.
        /// </summary>
        /// <param name="createKullaniciYetkiIslevObje"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateKullaniciYetkiIslevObjeCommand createKullaniciYetkiIslevObje)
        {
            var result = await Mediator.Send(createKullaniciYetkiIslevObje);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update KullaniciYetkiIslevObje.
        /// </summary>
        /// <param name="updateKullaniciYetkiIslevObje"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateKullaniciYetkiIslevObjeCommand updateKullaniciYetkiIslevObje)
        {
            var result = await Mediator.Send(updateKullaniciYetkiIslevObje);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete KullaniciYetkiIslevObje.
        /// </summary>
        /// <param name="deleteKullaniciYetkiIslevObje"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteKullaniciYetkiIslevObjeCommand deleteKullaniciYetkiIslevObje)
        {
            var result = await Mediator.Send(deleteKullaniciYetkiIslevObje);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
