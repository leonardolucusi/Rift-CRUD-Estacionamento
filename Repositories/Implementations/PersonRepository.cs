using Microsoft.EntityFrameworkCore;
using Rift.Data;
using Rift.Data.Migrations;
using Rift.Models;

namespace Rift.Repositories.Implementations
{
    public class PersonRepository : IPerson
    {

        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region POST
        
        public async Task CreatePerson(Person person)
        {
            await _context.Person.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region GET

        public async Task<List<Person>> GetAllPersons()
        {
            return await _context.Person.ToListAsync();
        }

        public async Task<Person> GetPersonById(int id)
        {
            return await _context.Person.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Person>> GetPersonByCPF(long cpf)
        {
            return await _context.Person.Where(e => e.Cpf == cpf).ToListAsync();
        }

        public async Task<List<Person>> GetPersonByName(string name)
        {
            return await _context.Person.Where(e => e.Name == name).ToListAsync();
        }

        public async Task<List<Person>> GetPersonByRG(long rg)
        {
            return await _context.Person.Where(e => e.Rg == rg).ToListAsync();
        }

        #endregion

        #region UPDATE

        public async Task UpdatePerson(Person person)
        {
            _context.Update(person);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region DELETE

        public async Task DeletePerson(Person person)
        {
            _context.Remove(person);
            await _context.SaveChangesAsync();
        }

        #endregion

    }
}
