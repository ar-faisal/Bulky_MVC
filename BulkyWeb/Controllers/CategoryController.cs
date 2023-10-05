﻿
using Bulky.DataAccess.Data;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {   List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        { return View(); }

        [HttpPost]
        public IActionResult Create(Category obj)
        { 
        //   if(obj.Name == obj.DisplayOrder.ToString())
        //    {
        //        ModelState.AddModelError("name", "the display order cannot match with the name");
        //    }
        //    if (obj.Name.ToLower() == "test")
        //    {
        //        ModelState.AddModelError("", "the name should not be test");
        //    }
            if (ModelState.IsValid) //it will check the validations at the server side done in model class/BOL
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
                
                //Or return RedirectToAction("Index","Category");
            }

            return View();
        }


        public IActionResult Edit(int? id)
        {   if(id==null || id==0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id); //Find only works on Id(primary key)
            //Category? categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb = _db.Categories.Where(u=>u.Id==id).FirstOrFefault();

            if (categoryFromDb ==null)
            {
                return NotFound();
            }
            
            return View(categoryFromDb); 
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            
            if (ModelState.IsValid) //it will check the validations at the server side done in model class/BOL
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated  successfully";
                return RedirectToAction("Index");
                //Or return RedirectToAction("Index","Category");
            }

            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id); //Find only works on Id(primary key)
            //Category? categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb = _db.Categories.Where(u=>u.Id==id).FirstOrFefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
            
        }
    }
}