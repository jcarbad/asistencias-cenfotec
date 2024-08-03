using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ausencias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoController : Controller
    {
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(Grupo grupo)
        {
            try
            {
                grupo.GrupoId = Guid.NewGuid().ToString();
                await grupo.InsertAsync();
                return Ok(new { Result = "Grupo created successfully", GrupoId = grupo.GrupoId });
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }

        [HttpGet("Get")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var grupos = await Grupo.Get();
                return Ok(new { Result = grupos });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }

        [HttpGet("Get/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Get(string id)
        {
            try
            {
                var grupos = await Grupo.Get($"Where grupoid = '{id}'");
                return Ok(new { Result = grupos });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }

        [HttpPatch("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(Grupo grupo)
        {
            try
            {
                await grupo.UpdateAsync("GrupoId");
                return Ok(new { Result = "Grupo updated successfully" });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var grupo = new Grupo { GrupoId = id.ToString() };
                await grupo.DeleteNoIdAsync("GrupoId");
                return Ok(new { Result = "Grupo deleted successfully" });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }
    }
}
