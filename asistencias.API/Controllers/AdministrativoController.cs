using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ausencias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrativoController : Controller
    {

        [HttpPost("CreateAdministrativo")]
        public async void Create(Administrativo estudiante)
        {
            //List<Administrativo> estudiantes = await Ausencias.API.Entity.GetAllAsync<Administrativo>();
            //estudiante.InsertAsync();
            //return Ok(new { Result = "Entity created successfully" });
        }

    }
}
