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
        [ValidateAntiForgeryToken] //NET platformunun dış saldırılara karşı aldığı,
        //bilgi isteyen kişi gerçekten sen misin diye kontrol eden önlemidir.

        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "DisplayOrder, Ad ile aynı olamaz.");
            }
            if (ModelState.IsValid) // modelde hata varmı?
            {
                _contex.Categories.Add(obj); //Kullanıcıdan gelen verileri db'e ekle
                _contex.SaveChanges(); //Veritabanına gönder
                TempData["success"] = "Kategori başarıyla oluşturuldu.";
                return RedirectToAction("Index"); // Index 'e yönlendir
            }
            return View(obj);//hata varsa modeldeki hatayı gönder

        }
        //Get
        public IActionResult Edit(int? id)
        {
            if ( id == null || id == 0 ) // id null yada 0 'a eşit mi?
            {
                return NotFound();
            } //değilse
            var categoryFromDb = _contex.Categories.Find(id); // db içerisinden id deki category 'i bul
            //var categoryFromDbFİrst = _contex.Categories.FirstOrDefault(u => u.Id == id); // bunu al bana ilkini ver
            //var categoryFromDbSingle = _contex.Categories.SingleOrDefault(u => u.Id == id); // bunu al bana ilkini ver
            
            if (categoryFromDb == null) // Nesneyi aldığımızda db'den kategori olup olmadığını kontrol edelim.
            {
                return NotFound();
            }
                
            return View(categoryFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post
        public IActionResult Edit(Category obj)
        {
            if( obj.Name == obj.DisplayOrder.ToString() )
            {
                ModelState.AddModelError("Name", "DisplayOrder, Ad ile aynı olamaz.");
            }
            if (ModelState.IsValid) // modelde hata yoksa güncelleme işlemlerini yap.
            {
                _contex.Categories.Update(obj);
                _contex.SaveChanges();
                TempData["success"] = "Kategori başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(obj);//hata varsa modeldeki hatayı gönder
        }

        [HttpGet]
        //Get
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0 )
            {
                NotFound();
            }

            var categoryFromDb = _contex.Categories.Find(id);

            if (categoryFromDb == null)
            {
                NotFound();
            }
            return View(categoryFromDb);
        }
        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _contex.Categories.Find(id);   

            if(obj == null)
            {
                return NotFound();
            }
            _contex.Categories.Remove(obj);
                 _contex.SaveChanges();
            TempData["success"] = "Kategori başarıyla silindi.";

            return RedirectToAction("Index");
        }
    }
}
