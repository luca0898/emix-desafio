using System.ComponentModel.DataAnnotations;

namespace eMix.ConsultaCEP.Models
{
    public class GetAddressByZipCodeRequest
    {
        [Required, RegularExpression("^[0-9]{8}$", ErrorMessage = "O CEP deve conter exatamente oito dígitos numéricos")]
        public required string ZipCode { get; set; }
    }
}
