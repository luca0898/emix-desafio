using eMix.ConsultaCEP.Configurations;
using eMix.ConsultaCEP.Contracts.Repositories;
using eMix.ConsultaCEP.Models;
using Microsoft.EntityFrameworkCore;

namespace eMix.ConsultaCEP.Repositories
{
    public class AddressRepository(AppDbContext dbContext) : IAddressRepository
    {
        public async Task<IEnumerable<Address>> Find()
        {
            return await dbContext.Addresses.ToArrayAsync();
        }

        public async Task<Address?> Find(string zipCode)
        {
            return await dbContext.Addresses.FirstOrDefaultAsync(entity => entity.Cep == zipCode);
        }

        public async Task Save(Address model)
        {
            await dbContext.Addresses.AddAsync(model);

            await dbContext.SaveChangesAsync();
        }
    }
}
