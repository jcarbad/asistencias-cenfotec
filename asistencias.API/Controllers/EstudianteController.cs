using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ausencias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : Controller
    {
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(Estudiante estudiante)
        {
            try
            {
                estudiante.EstudianteId = Guid.NewGuid().ToString();
                await estudiante.InsertAsync();
                return Ok(new { Result = "Estudiante created successfully", EstudianteId = estudiante.EstudianteId });
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
                var estudiantes = await Estudiante.Get();
                return Ok(new { Result = estudiantes });
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
                var estudiantes = await Estudiante.Get($"Where EstudianteId = '{id}'");
                return Ok(new { Result = estudiantes });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }

        [HttpPatch("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(Estudiante estudiante)
        {
            try
            {
                await estudiante.UpdateAsync("EstudianteId");
                return Ok(new { Result = "Estudiante updated successfully" });
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
                var estudiante = new Estudiante { EstudianteId = id.ToString() };
                await estudiante.DeleteNoIdAsync("EstudianteId");
                return Ok(new { Result = "Estudiante deleted successfully" });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }
    }
}
