using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebsiteBanHang2.Models;
using WebsiteBanHang2.Repository;

namespace WebsiteBanHang2.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductRepository _productRepository;
		private readonly ICategoryRepository _categoryRepository;
		public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
		}

		public async Task<IActionResult> Index()
		{
			var products = await _productRepository.GetAllAsync();
			return View(products);
		}
		public async Task<IActionResult> AddAsync()
		{
			var categories = await _categoryRepository.GetAllAsync();
			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Add(Product product)
		{
			if (ModelState.IsValid)
			{
				await _productRepository.AddAsync(product);
				return RedirectToAction(nameof(Index));
			}

			var categories = await _categoryRepository.GetAllAsync();
			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			return View(product);
		}
		public async Task<IActionResult> Display(int id)
		{
			var product = await _productRepository.GetByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}
		public async Task<IActionResult> Update(int id)
		{
			var product = await _productRepository.GetByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			var categories = await _categoryRepository.GetAllAsync();
			ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
			return View(product);
		}
		[HttpPost]
		public async Task<IActionResult> Update(int id, Product product)
		{
			if (id != product.Id)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				await _productRepository.UpdateAsync(product);
				return RedirectToAction(nameof(Index));
			}
			return View(product);
		}
		public async Task<IActionResult> Delete(int id)
		{
			var product = await _productRepository.GetByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _productRepository.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
