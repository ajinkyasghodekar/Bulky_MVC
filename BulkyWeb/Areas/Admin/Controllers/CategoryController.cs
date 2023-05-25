
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        // Create Methods View
        public IActionResult Create()
        {
            return View();
        }

        // Create Methods Create a record
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // Custom validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Name and Order cannot be same...");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully !!!";
                return RedirectToAction("Index");
            }
            return View();
        }

        // Edit Methods View
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // categoryFromDb1 and categoryFromDb2 are also used to get a id from db there are below 3 ways to get id. 
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();

            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        // Edit Methods Edit a record
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Edited Successfully !!!";
                return RedirectToAction("Index");
            }
            return View();
        }

        // Delete Methods View
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // categoryFromDb1 and categoryFromDb2 are also used to get a id from db there are below 3 ways to get id. 
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();

            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        // Delete Methods Delete a record
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            Category obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (id == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully !!!";
            return RedirectToAction("Index");
        }

    }
}
