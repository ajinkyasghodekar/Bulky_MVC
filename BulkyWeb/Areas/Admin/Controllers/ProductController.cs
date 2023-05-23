
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            
            return View(objProductList);
        }

        // Create Methods View
        public IActionResult Create()
        {
            // Projection to get a Name and Id from category table
            // ViewBag
            //ViewBag.CategoryList = CategoryList;

            // ViewData
            //ViewData["CategoryList"] = CategoryList;

            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            return View(productVM);
        }

        // Create Methods Create a record
        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product Created Successfully !!!";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
            
        }

        // Edit Methods View
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // ProductFromDb1 and ProductFromDb2 are also used to get a id from db there are below 3 ways to get id. 
            //Product? ProductFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Product? ProductFromDb2 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();

            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        // Edit Methods Edit a record
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product Edited Successfully !!!";
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
            // ProductFromDb1 and ProductFromDb2 are also used to get a id from db there are below 3 ways to get id. 
            //Product? ProductFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Product? ProductFromDb2 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();

            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        // Delete Methods Delete a record
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            Product obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (id == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted Successfully !!!";
            return RedirectToAction("Index");
        }

    }
}
