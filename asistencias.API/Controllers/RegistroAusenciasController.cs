using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System;

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
            try
            {
                var emailClaim = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                if (emailClaim == null)
                {
                    return Unauthorized(new { Error = "User email claim not found." });
                }

                await RegistroAusencia.CreateAsync(ausencias, emailClaim.Value);
                return Ok(new { Result = "Ausencia created successfully", Number = 3 });
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
            }
        }
    }
}
