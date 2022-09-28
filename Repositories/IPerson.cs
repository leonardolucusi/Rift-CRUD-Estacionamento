using Rift.Models;

namespace Rift.Repositories
{
    public interface IPerson
    {
        // POST
        Task CreatePerson(Person person);

        // GET
        Task<List<Person>> GetAllPersons();
        Task<Person> GetPersonById(int id);
        Task<List<Person>> GetPersonByCPF(long cpf);
        Task<List<Person>> GetPersonByName(string name);
        Task<List<Person>> GetPersonByRG(long cpf);

        // UPDATE
        Task UpdatePerson(Person person);

        // DELETE
        Task DeletePerson(Person person); 
    }
}
