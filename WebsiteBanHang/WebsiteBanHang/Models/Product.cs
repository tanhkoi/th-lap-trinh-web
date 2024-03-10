using System.ComponentModel.DataAnnotations;

namespace WebsiteBanHang.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, StringLength(128)]
        public string Name { get; set; }
        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        // 2.3.5
        public string? ImageURL { get; set; }
        public List<string>? ImageURLs { get; set; }
    }
}
