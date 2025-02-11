using AutoMapper;
using eMix.ConsultaCEP.Contracts.Repositories;
using eMix.ConsultaCEP.Contracts.Services;
using eMix.ConsultaCEP.Models;

namespace eMix.ConsultaCEP.Services
{
    public class AddressService(
        IAddressRepository repository,
        IMapper mapper,
        IViaCepHttpService viaCepHttpService
        ) : IAddressService
    {
        public async Task<IEnumerable<Address>> Find()
        {
            return await repository.Find();
        }

        public async Task<Address?> FindByZipCodeAndSave(string zipCode)
        {
            var existingAddress = await repository.Find(zipCode);

            if (existingAddress != null)
                return existingAddress;

            var viaCepResult = await viaCepHttpService.getAddressByZipCode(zipCode);

            if (viaCepResult.erro == "true")
                return null;

            var address = mapper.Map<Address>(viaCepResult);

            await repository.Save(address);

            return address;
        }
    }
}
