using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Repositories
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
        public IActionResult Add()
        {
            var categories = _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }
        // 2.3.5
		[HttpPost]
		public async Task<IActionResult> Add(Product product, IFormFile imageURL, List<IFormFile> imageURLs)
		{
			if (ModelState.IsValid)
			{
				if (imageURL != null)
				{
					product.ImageURL = await SaveImage(imageURL);
				}
				if (imageURLs != null)
				{
					product.ImageURLs = new List<string>();
					foreach (var file in imageURLs)
					{
						product.ImageURLs.Add(await SaveImage(file));
					}
				}
				_productRepository.Add(product);
				return RedirectToAction("Index");
			}
			return View(product);
		}
		public IActionResult Index()
        {
            var products = _productRepository.GetAll();
            return View(products);
        }
        public IActionResult Display(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        public IActionResult Update(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _productRepository.Delete(id); 
            return RedirectToAction("Index");
        }
		// 2.3.5
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
