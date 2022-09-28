using Rift.Models;

namespace Rift.Repositories
{
    public interface IBusiness
    {
        // POST
        Task CreateBusiness(Business business);

        // GET
        Task<List<Business>> GetAllBusiness();
        Task<Business> GetBusinessById(int id);
        Task<List<Business>> GetBusinessByCNPJ(long cnpj);
        Task<List<Business>> GetBusinessBySocialReason(string socialReason);
        Task<List<Business>> GetBusinessByFantasyName(string fantasyName);

        // UPDATE
        Task UpdateBusiness(Business business);

        // DELETE
        Task DeleteBusiness(Business business);
    }
}
