using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET Company/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET Company/Upsert
        public IActionResult Upsert(int? id)
        {
            Company company = new();
            // Create Company
            if (id == null || id == 0)
            {
                return View(company);
            }

            // Update Company
            company = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == id);
            return View(company);
        }

        // POST Company/Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (!ModelState.IsValid) return View(company);

            if(company.Id == 0)
            {
                // Create
                _unitOfWork.Company.Add(company);
                TempData["success"] = "Company created successfully";
            }
            else
            {
                // update
                _unitOfWork.Company.Update(company);
                TempData["success"] = "Company updated successfully";
            }
            
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Company> companies = _unitOfWork.Company.GetAll();
            return Json(new { data = companies });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Company company = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == id);
            if (company == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Company.Remove(company);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
