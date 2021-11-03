using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BLL.Product;
using ProductMananger.Data;
using BLL.Category;
using BLL.Repository;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using BLL;
using ProductMananger.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace ProductMananger.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
         private readonly Repository<Product> _context;
         private readonly Repository<Category> _contextCat;
         private readonly INotyfService _notyf;

        public ProductsController(Repository<Product> context, Repository<Category> contextCat,INotyfService _notyf)
        {
            _context = context;
            _context.requestUrl = "Products";

            _contextCat = contextCat;
            _contextCat.requestUrl = "Categories";

            this._notyf = _notyf;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var getProduts = await Task.Run(() => _context.FindAll());
            DependencyAssiging(getProduts);
            return View(getProduts);
        }

        private void DependencyAssiging(IQueryable<Product> getProduts)
        {
            foreach (var product in getProduts)
            {
                product.Category = _contextCat.Find(product.CategoryId);
            }
        }

        private void AssignDependency(Product x)
        {
            throw new NotImplementedException();
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await Task.Run(()=> _context.Find(id));
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ProductCode"] = DateTime.Now.ToString("yyyyMM") +"-"+ _context.FindAll().Count()+1;
            ViewData["CategoryId"] = new SelectList( _contextCat.FindAll(), "CategoryId", "CategoryCode");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductCode,Name,Description,CategoryName,Price,Image,CategoryId")] Product product)
        {
            AsingImange(product);
            if (Request.Form.Files.Count > 0)
            {
                await Task.Run(() => _context.Save(product));
                _notyf.Success("Saved Successfully", 3);
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Please choose an Image to upload");
            ViewData["CategoryId"] = new SelectList(_contextCat.FindAll(), "CategoryId", "CategoryCode", product.CategoryId);
            return View(product);
        }

        private void AsingImange(Product product)
        {
            foreach (var file in Request.Form.Files)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                product.Image = ms.ToArray();
            }
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

             var product = await Task.Run(()=> _context.Find(id));
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_contextCat.FindAll(), "CategoryId", "CategoryCode", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductCode,Name,Description,CategoryName,Price,Image,CategoryId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    AsingImange(product);
                    await Task.Run(() => _context.Update(product, id));
                    _notyf.Success("Saved Successfully", 3);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_contextCat.FindAll(), "CategoryId", "CategoryCode", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await Task.Run(() => _context.Find(id));
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //I created delete to follow the instruction, my way will be to disable the product.
            var product = await Task.Run(() => _context.Find(id));
            await Task.Run(() => _context.Remove(product.ProductId));
            _notyf.Success("Removed Successfully", 3);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
             return _context.FindAll().Any(e => e.CategoryId == id);
        }


        public IActionResult ImportExcel()
        {
            return View();
        }

        [HttpPost]
        public void ImportExcel(IList<IFormFile> files)
        {

            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);

            }
             var filePath = Path.GetTempFileName();
            string filePath1 = null;
            if (Request.Form.Files.Count == 0) return;
           var getProductData = new ExcelData().ProductExcelData(filePath);
        }



    }
}
