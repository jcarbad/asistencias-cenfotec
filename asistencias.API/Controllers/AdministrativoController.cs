using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ausencias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrativoController : Controller
    {

        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(Administrativo administrativo)
        {
            try
            {
                administrativo.AdministrativoId = Guid.NewGuid().ToString();
                administrativo.Estatus = "Activo";
                await administrativo.InsertAsync();
                return Ok(new { Result = "Administrativo created successfully", AdministrativoId = administrativo.AdministrativoId });
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
                var administrativos = await Administrativo.Get();
                return Ok(new { Result = administrativos });
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
                var administrativos = await Administrativo.Get($"Where administrativoId = '{id}'");
                return Ok(new { Result = administrativos });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }

        [HttpPatch("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(Administrativo administrativo)
        {
            try
            {
                administrativo.Estatus = "Activo";
                await administrativo.UpdateAsync("AdministrativoId");
                return Ok(new { Result = "Administrativo updated successfully" });
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
                var administrativo = new Administrativo { AdministrativoId = id.ToString() };
                await administrativo.DeleteNoIdAsync("AdministrativoId");
                return Ok(new { Result = "Administrativo deleted successfully" });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }

    }
}
