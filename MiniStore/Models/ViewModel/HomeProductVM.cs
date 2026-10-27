using System.Collections.Generic;
using MiniStore.Models;

namespace MiniStore.Models.ViewModel
{
    public class HomeProductVM
    {
        public List<Product> FeaturedProducts { get; set; } = new List<Product>();
        public List<Product> NewProducts { get; set; } = new List<Product>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public string SearchTerm { get; set; }
    }
}
