using Microsoft.EntityFrameworkCore;
using Rift.Data;
using Rift.Data.Migrations;
using Rift.Models;

namespace Rift.Repositories.Implementations
{
    public class BusinessRepository : IBusiness
    {

        private readonly ApplicationDbContext _context;

        public BusinessRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        #region POST
        public async Task CreateBusiness(Business business)
        {
            await _context.AddAsync(business);
            await _context.SaveChangesAsync();
        }
        #endregion


        #region GET
        public async Task<List<Business>> GetAllBusiness()
        {
            return await _context.Business.ToListAsync();
        }

        public async Task<Business> GetBusinessById(int id)
        {
            return await _context.Business.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Business>> GetBusinessByCNPJ(long cnpj)
        {
            return await _context.Business.Where(e => e.Cnpj == cnpj).ToListAsync();
        }

        public async Task<List<Business>> GetBusinessByFantasyName(string fantasyName)
        {
            return await _context.Business.Where(e => e.FantasyName == fantasyName).ToListAsync();
        }

        public async Task<List<Business>> GetBusinessBySocialReason(string socialReason)
        {
            return await _context.Business.Where(e => e.SocialReason == socialReason).ToListAsync();
        }
        #endregion


        #region UPDATE

        public async Task UpdateBusiness(Business business)
        {
            _context.Update(business);
            await _context.SaveChangesAsync();
        }

        #endregion


        #region DELETE

        public async Task DeleteBusiness(Business business)
        {
            _context.Remove(business);
            await _context.SaveChangesAsync();
        }

        #endregion

    }
}
