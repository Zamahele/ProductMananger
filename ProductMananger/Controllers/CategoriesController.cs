using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BLL.Category;
using ProductMananger.Data;
using BLL.Repository;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace ProductMananger.Controllers
{
     [Authorize]
    public class CategoriesController : Controller
    {
        private readonly Repository<Category> _context;
        private readonly INotyfService _notyf;
        public CategoriesController(Repository<Category> context,INotyfService _notyf)
        {
            _context = context;
            _context.requestUrl = "Categories";
             this._notyf = _notyf;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(()=> _context.FindAll()));
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await Task.Run(()=> _context.Find(id));
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name,CategoryCode,IsActive")] Category category)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(()=> _context.Save(category));
                 _notyf.Success("Saved Successfully", 3);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult ValidateCategory(string categoryCode)
        {
            var result = CategoryExists(categoryCode);
            if (result)
                return Json(data: "Category Code in used,Please use 3 alphabet letters and three numeric characters e.g., ABC123");

            if (!ValidateFormat(categoryCode))
                return Json(data: "Invalid Format,Please use 3 alphabet letters and three numeric characters e.g., ABC123");


            return Json(data: true);
        }

        public bool ValidateFormat(string categoryCode)
        {
            var result1 = true;
            var result2 = true;
            var getFirstThreeLetters = categoryCode.Substring(0, 3);
            var getLastThreeNumbers = categoryCode.Substring(3);

            FormatValidationString(ref result1, ref result2, getFirstThreeLetters, getLastThreeNumbers);

            return result1 == result2;
        }

        private static void FormatValidationString(ref bool result1, ref bool result2, string getFirstThreeLetters, string getLastThreeNumbers)
        {
            foreach (var myChar in getFirstThreeLetters)
            {
                if (Char.IsDigit(Convert.ToChar(myChar)))
                    result1 = false;
            }
            if (result1)
                foreach (var myChar in getLastThreeNumbers)
                {
                    if (!Char.IsDigit(Convert.ToChar(myChar)))
                        result2 = false;
                }
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await Task.Run(()=> _context.Find(id));
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name,CategoryCode,IsActive")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await Task.Run(()=> _context.Update(category,id));
                     _notyf.Success("Saved Successfully", 3);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await Task.Run (()=> _context.Find(id));
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Parent child delete.
            var category = await Task.Run(() => _context.Find(id));
            await Task.Run(() => _context.Remove(category.CategoryId));
            _notyf.Success("Removed Successfully", 3);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.FindAll().Any(e => e.CategoryId == id);
        }

        private bool CategoryExists(string CategoryCode)
        {
            return _context.FindAll().Any(e => e.CategoryCode == CategoryCode);
        }
        
    }
}
