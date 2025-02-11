using eMix.ConsultaCEP.Models;

namespace eMix.ConsultaCEP.Contracts.Repositories
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> Find();
        Task<Address?> Find(string zipCode);
        Task Save(Address model);
    }
}
