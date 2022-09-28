using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rift.Models;
using Rift.Repositories;

namespace Rift.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IPerson _personRepository;

        public PeopleController(IPerson personRepository)
        {
            _personRepository = personRepository;
        }


        #region GET

        /// <summary>
        /// GET: Person/ShowSearchCNPF
        /// </summary>
        public IActionResult ShowSearchCPF()
        {
            return View();
        }

        /// <summary>
        /// GET: Person/ShowSearchCPFResults/
        /// </summary>
        public async Task<IActionResult> ShowSearchCPFResults(long searchCPF)
        {
            List<Person> peoples = await _personRepository.GetPersonByCPF(searchCPF);
            return View(nameof(DisplayResults), peoples);
        }

        /// <summary>
        /// GET: Person/ShowSearchNameResult/
        /// </summary>
        public async Task<IActionResult> ShowSearchNameResult(string searchName)
        {
            List<Person> peoples = await _personRepository.GetPersonByName(searchName);
            return View(nameof(DisplayResults), peoples);
        }

        /// <summary>
        /// GET: Person/ShowSearchRGResult/
        /// </summary>
        public async Task<IActionResult> ShowSearchRGResult(string searchRg)
        {
            List<Person> peoples = await _personRepository.GetPersonByName(searchRg);
            return View(nameof(DisplayResults), peoples);
        }

        /// <summary>
        /// GET: People/Details/{id}
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return NotFound(); }

            Person person = await _personRepository.GetPersonById((int)id);
            if (person == null) { return NotFound(); }

            return View(person);
        }

        /// <summary>
        /// GET: Persons/DisplayResults
        /// </summary>
        public IActionResult DisplayResults(List<Person> persons)
        {
            return View("DisplayResults", persons);
        }

        /// <summary>
        /// GET: People
        /// </summary>
        public async Task<IActionResult> Index()
        {
            return View(await _personRepository.GetAllPersons());
        }

        #endregion


        #region POST

        /// <summary>
        /// POST: People/Create
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: People/Create
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cpf,Name,Gender,BirthDate,Rg,Address,Phone,Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                // if cpf already exist
                if (await _personRepository.GetPersonByCPF(person.Cpf) == null)
                {
                    // keep the same view with the same data if this person cpf exists.
                    View(person);
                }

                await _personRepository.CreatePerson(person);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        #endregion


        #region UPDATE

        /// <summary>
        /// UPDATE: People/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            Person person = await _personRepository.GetPersonById((int)id);
            if (person == null) { return NotFound(); }

            return View(person);
        }

        /// <summary>
        /// UPDATE: People/Edit/{id}
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cpf,Name,Gender,BirthDate,Rg,Address,Phone,Email")] Person person)
        {
            if (id != person.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                try
                {
                    await _personRepository.UpdatePerson(person);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _personRepository.GetPersonById(id) == null) { return NotFound(); }
                    else { throw; }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        #endregion


        #region DELETE

        /// <summary>
        /// DELETE: People/Delete/{id}
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            Person person = await _personRepository.GetPersonById((int)id);
            if (person == null) { return NotFound(); }

            return View(person);
        }

        /// <summary>
        /// DELETE: People/Delete/{id}
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Person person = await _personRepository.GetPersonById(id);
            if (person != null)
            {
                await _personRepository.DeletePerson(person);
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion


        #region SERVER-SIDE FIELDS VALIDATION

        /// <summary>
        /// Remote action function to verify if PersonModel 'cpf' exists.
        /// </summary>
        public async Task<IActionResult> VerifyCPFExists(long cpf)
        {
            List<Person> persons = await _personRepository.GetPersonByCPF(cpf);
            Person person = persons.FirstOrDefault(e => e.Cpf == cpf);
            if (person != null)
            {
                return Json($"O CPF '{cpf}' já existe.");
            }
            return Json(true);
        }

        #endregion

    }

}
