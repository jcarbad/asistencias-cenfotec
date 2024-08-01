using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Ausencias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroAusenciasController : Controller
    {
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(Ausencia ausencias)
        {
            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            await RegistroAusencia.CreateAsync(ausencias, emailClaim.Value);
            return Ok(new { Result = "Grupo created successfully" });
        }
    }
}
