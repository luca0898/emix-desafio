using eMix.ConsultaCEP.Models;

namespace eMix.ConsultaCEP.Contracts.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> Find();
        Task<Address?> FindByZipCodeAndSave(string zipCode);
    }
}
