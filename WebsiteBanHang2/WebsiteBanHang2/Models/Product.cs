using System.ComponentModel.DataAnnotations;

namespace WebsiteBanHang2.Models
{
	public class Product
	{
		public int Id { get; set; }
		[Required, StringLength(100)]
		public string? Name { get; set; }
		[Range(0.01, 10000.00)]
		public string? Description { get; set; }
		public decimal Price { get; set; }
		public string? ImageUrl { get; set; }
		public List<ProductImage>? Images { get; set; }
		public int CategoryId { get; set; }
		public Category? Category { get; set; }
	}
}
