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
        // hien thi danh sach sp
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }
		// hien thi form them sp moi
		public async Task<IActionResult> Add()
		{
			var categories = await _categoryRepository.GetAllAsync();
			foreach (var category in categories)
			{
				Console.WriteLine($"Category Id: {category.Id}, Name: {category.Name}");
			}
			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			return View();
		}

		// xu li them sp moi
		[HttpPost]
        public async Task<IActionResult> Add(Product product, IFormFile imageURL, List<ProductImage> imageURLs)
        {
            if (ModelState.IsValid)
            {
                if (imageURL != null)
                {
                    product.ImageUrl = await SaveImage(imageURL);
                }
                await _productRepository.AddAsync(product);
                return RedirectToAction("Index");
            }
            // neu modelstate khong hop le, hien thi form voi du lieu da nhap
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }
        // hien thi thong tin chi tiet sp
        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // hien thi form cap nhat sp
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
        // xu li cap nhat sp
		[HttpPost]
		public async Task<IActionResult> Update(Product product, IFormFile imageURL, List<ProductImage> imageURLs)
		{
			if (ModelState.IsValid)
			{
				if (imageURL != null)
				{
					product.ImageUrl = await SaveImage(imageURL);
				}
				await _productRepository.UpdateAsync(product);
				return RedirectToAction(nameof(Index));
			}
			return View(product);
		}
		// hien thi form xac nhan xoa sp
		public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // xu ly xoa sp
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName;
        }
    }
}