using eMix.ConsultaCEP.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace eMix.ConsultaCEP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CepController(IViaCepHttpService viaCepHttpService) : ControllerBase
    {
        [HttpGet("/{zipCode}")]
        public async Task<IActionResult> Get(string zipCode)
        {
            return Ok(await viaCepHttpService.getAddressByZipCode(zipCode));
        }
    }
}
