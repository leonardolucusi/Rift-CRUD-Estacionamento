using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rift.Models;
using Rift.Repositories;


namespace Rift.Controllers
{
    public class BusinessesController : Controller
    {
        private readonly IBusiness _businessRepository;

        public BusinessesController(IBusiness businessRepository)
        {
            _businessRepository = businessRepository;
        }


        #region GET

        /// <summary>
        /// GET: Businesses/ShowSearchCNPJ
        /// </summary>
        public IActionResult ShowSearchCNPJ()
        {
            return View();
        }

        /// <summary>
        /// GET: Businesses/ShowSearchCNPJResults
        /// </summary>
        public async Task<IActionResult> ShowSearchCNPJResults(long searchCnpj)
        {
            List<Business> businesses = await _businessRepository.GetBusinessByCNPJ(searchCnpj);
            return View(nameof(DisplayResults), businesses);
        }

        /// <summary>
        /// GET: Businesses/ShowSearchSocialReasonResults
        /// </summary>
        public async Task<IActionResult> ShowSearchSocialReasonResults(string searchSocialReason)
        {
            List<Business> businesses = await _businessRepository.GetBusinessBySocialReason(searchSocialReason);
            return View(nameof(DisplayResults), businesses);
        }

        /// <summary>
        /// GET: Businesses/ShowSearchFantasyNameResults
        /// </summary>
        public async Task<IActionResult> ShowSearchFantasyNameResults(string searchFantasyName)
        {
            List<Business> businesses = await _businessRepository.GetBusinessByFantasyName(searchFantasyName);
            return View(nameof(DisplayResults), businesses);
        }

        /// <summary>
        /// GET: Businesses/DisplayResults
        /// </summary>
        /// <param name="businesses"></param>
        public IActionResult DisplayResults(List<Business> businesses)
        {
            return View("DisplayResults", businesses);
        }

        /// <summary>
        /// GET: Businesses/Details
        /// </summary>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return NotFound(); }

            Business business = await _businessRepository.GetBusinessById((int)id);
            if (business == null) { return NotFound(); }

            return View(business);
        }

        /// <summary>
        /// GET: Businesses/
        /// </summary>
        public async Task<IActionResult> Index()
        {
            return View(await _businessRepository.GetAllBusiness());
        }

        #endregion


        #region POST

        /// <summary>
        /// POST: Businesses/Create
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: Businesses/Create
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cnpj,SocialReason,FantasyName,Address,Phone,Email")] Business business)
        {
            if (ModelState.IsValid)
            {
                //if cnpj already exist 
                if (await _businessRepository.GetBusinessByCNPJ(business.Cnpj) == null)
                {
                    // keep the same view with the same data if this business cnpj exists.
                    View(business);
                }

                await _businessRepository.CreateBusiness(business);
                return RedirectToAction(nameof(Index));
            }
            return View(business);
        }

        #endregion


        #region UPDATE

        /// <summary>
        /// UPDATE: Businesses/Edit/{id}
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            Business business = await _businessRepository.GetBusinessById((int)id);
            if (business == null) { return NotFound(); }

            return View(business);
        }

        /// <summary>
        /// UPDATE: Businesses/Edit/{id}
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cnpj,SocialReason,FantasyName,Address,Phone,Email")] Business business)
        {
            if (id != business.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                try
                {
                    await _businessRepository.UpdateBusiness(business);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _businessRepository.GetBusinessById(id) == null) { return NotFound(); }
                    else { throw; }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(business);
        }

        #endregion


        #region DELETE

        /// <summary>
        /// GET: Businesses/Delete/{id}
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            Business business = await _businessRepository.GetBusinessById((int)id);
            if (business == null) { return NotFound(); }

            return View(business);
        }

        /// <summary>
        /// POST: Businesses/Delete/{id}
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Business business = await _businessRepository.GetBusinessById(id);
            if (business != null)
            {
                await _businessRepository.DeleteBusiness(business);
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion


        #region SERVER-SIDE FIELD VALIDATION
        /// <summary>
        /// Remote action function to verify if BusinessModel 'cnpj' exists.
        /// </summary>
        public async Task<IActionResult> VerifyCNPJExists(long cnpj)
        {
            List<Business> businesses = await _businessRepository.GetBusinessByCNPJ(cnpj);
            Business business = businesses.FirstOrDefault(e => e.Cnpj == cnpj);
            if (business != null)
            {
                return Json($"O CNPJ '{cnpj}' já existe.");
            }
            return Json(true);
        }
        #endregion
    }
}
