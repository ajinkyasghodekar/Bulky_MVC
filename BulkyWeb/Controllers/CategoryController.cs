
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
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
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Name and Order cannot be same...");
            }
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category Created Successfully !!!";
                return RedirectToAction("Index");
            }
            return View();            
        }

        // Edit Methods View
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            // categoryFromDb1 and categoryFromDb2 are also used to get a id from db there are below 3 ways to get id. 
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();

            Category? categoryFromDb = _categoryRepo.Get(u=>u.Id == id);

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
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
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

            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);

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
            Category obj = _categoryRepo.Get(u => u.Id == id);
            if (id == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(obj);
            _categoryRepo.Save();
            TempData["success"] = "Category Deleted Successfully !!!";
            return RedirectToAction("Index");
        }

    }
}
