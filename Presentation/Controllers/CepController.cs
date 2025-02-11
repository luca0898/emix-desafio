using eMix.ConsultaCEP.Contracts.Services;
using eMix.ConsultaCEP.Models;
using Microsoft.AspNetCore.Mvc;

namespace eMix.ConsultaCEP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CepController(IAddressService adressService) : ControllerBase
    {
        [HttpGet("/history")]
        public async Task<IActionResult> History()
        {
            var address = await adressService.Find();

            return Ok(address);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GetAddressByZipCodeRequest request)
        {
            var address = await adressService.FindByZipCodeAndSave(request.ZipCode);

            if (address != null)
                return Ok(address);

            return BadRequest(new { Message = "CEP não encontrado" });
        }
    }
}
