using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ausencias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrativoController : Controller
    {

        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Administrativo administrativo)
        {
            administrativo.InsertAsync();
            return Ok(new { Result = "Administrativo created successfully" });
        }

        [HttpGet("Get")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Get()
        {
            var administrativos = await Administrativo.Get();
            return Ok(new { Result = administrativos });
        }

        [HttpGet("Get/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Get(int id)
        {
            var administrativo = new Administrativo();
            var administrativos = await administrativo.GetByIdAsync<Administrativo>(id);
            return Ok(new { Result = administrativos });
        }

        [HttpPatch("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(Administrativo administrativo)
        {
            await administrativo.UpdateAsync();
            return Ok(new { Result = "Administrativo updated successfully" });
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var administrativo = new Administrativo();
            administrativo.Id = id.ToString();
            await administrativo.DeleteAsync();
            return Ok(new { Result = "Administrativo Deleted successfully" });
        }

    }
}
