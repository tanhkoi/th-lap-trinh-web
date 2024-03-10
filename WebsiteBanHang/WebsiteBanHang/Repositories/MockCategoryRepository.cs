﻿using WebsiteBanHang.Models;

namespace WebsiteBanHang.Repositories
{
    public class MockCategoryRepository : ICategoryRepository
    {
        private List<Category> _categories;
        public MockCategoryRepository() 
        { 
            _categories = new List<Category> 
            {
                new Category { Id = 1, Name = "Laptop"},
                new Category { Id = 2, Name = "Desktop"}
            };
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _categories;
        }
    }
}
