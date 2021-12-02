using BulkyBookWebCore.Data;
using BulkyBookWebCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BulkyBookWebCore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _contex;
        public CategoryController(ApplicationDbContext db)
        {
            _contex = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _contex.Categories;
            return View(objCategoryList);
        }
        //Get
        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            _contex.Categories.Add(obj);
            _contex.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
