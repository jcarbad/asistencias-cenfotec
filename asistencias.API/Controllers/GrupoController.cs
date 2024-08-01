using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ausencias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoController : Controller
    {
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Grupo grupo)
        {
            grupo.GrupoId = Guid.NewGuid().ToString();
            grupo.InsertAsync();
            return Ok(new { Result = "Grupo created successfully" });
        }

        [HttpGet("Get")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Get()
        {
            var grupos = await Grupo.Get();
            return Ok(new { Result = grupos });
        }

        [HttpGet("Get/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Get(int id)
        {
            var grupo = new Grupo();
            var grupos = await grupo.GetByIdAsync<Grupo>(id);
            return Ok(new { Result = grupos });
        }

        [HttpPatch("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(Grupo grupo)
        {
            await grupo.UpdateAsync();
            return Ok(new { Result = "Grupo updated successfully" });
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var grupo = new Grupo();
            grupo.GrupoId = id.ToString();
            await grupo.DeleteNoIdAsync("grupoId");
            return Ok(new { Result = "Administrativo Deleted successfully" });
        }
    }
}
