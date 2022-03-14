using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypesList = _unitOfWork.CoverType.GetAll();
            return View(coverTypesList);
        }

        // GET CoverType/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST CoverType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (!ModelState.IsValid) return View();

            _unitOfWork.CoverType.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType created successfully";
            return RedirectToAction("Index");
        }

        // GET CoverType/Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();

            CoverType coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(coverType => coverType.Id == id);

            if (coverTypeFromDb == null) return NotFound();

            return View(coverTypeFromDb);
        }
        // POST CoverType/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverType)
        {
            if (!ModelState.IsValid) return View();

            _unitOfWork.CoverType.Update(coverType);
            _unitOfWork.Save();
            TempData["success"] = "CoverType Uptated successfully";
            return RedirectToAction("Index");
        }

        // GET CoverType/Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id ==0) return NotFound();

            CoverType coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(coverType => coverType.Id == id);

            if (coverTypeFromDb == null) return NotFound();

            return View(coverTypeFromDb);
        }

        // POST CoverType/Delete
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            CoverType coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(coverType => coverType.Id == id);

            if (coverTypeFromDb == null) return NotFound();

            _unitOfWork.CoverType.Remove(coverTypeFromDb);
            _unitOfWork.Save();
            TempData["success"] = "CoverType deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
