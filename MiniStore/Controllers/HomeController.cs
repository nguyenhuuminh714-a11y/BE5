using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using MiniStore.Models;
using MiniStore.Models.ViewModel;

namespace MiniStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly StoreDb db = new StoreDb();

        public ActionResult Index(string searchTerm)
        {
            var query = db.Products.Include(p => p.Category).AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(p => p.ProductName.Contains(searchTerm) ||
                    p.ProductDescription.Contains(searchTerm) || p.Category.CategoryName.Contains(searchTerm));
            var all = query.OrderBy(p => p.ProductName).ToList();
            var vm = new HomeProductVM {
                SearchTerm = searchTerm,
                Categories = db.Categories.OrderBy(c => c.CategoryName).ToList(),
                FeaturedProducts = all.Take(4).ToList(),
                NewProducts = all.OrderByDescending(p => p.ProductID).Take(20).ToList()
            };
            return View(vm);
        }

        public ActionResult ProductList(int? categoryId, string searchTerm, string sortOrder, int page = 1)
        {
            const int pageSize = 6;
            var query = db.Products.Include(p => p.Category).AsQueryable();
            if (categoryId.HasValue) query = query.Where(p => p.CategoryID == categoryId.Value);
            if (!string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(p => p.ProductName.Contains(searchTerm) ||
                    p.ProductDescription.Contains(searchTerm) || p.Category.CategoryName.Contains(searchTerm));
            switch (sortOrder)
            {
                case "name_desc": query = query.OrderByDescending(p => p.ProductName); break;
                case "price": query = query.OrderBy(p => p.ProductPrice); break;
                case "price_desc": query = query.OrderByDescending(p => p.ProductPrice); break;
                default: query = query.OrderBy(p => p.ProductName); break;
            }
            var count = query.Count();
            var vm = new ProductSearchVM {
                SearchTerm = searchTerm, SortOrder = sortOrder, PageNumber = page < 1 ? 1 : page,
                PageSize = pageSize, TotalPages = (int)System.Math.Ceiling(count / (double)pageSize),
                Products = query.Skip(((page < 1 ? 1 : page) - 1) * pageSize).Take(pageSize).ToList(),
                Categories = db.Categories.ToList()
            };
            return View(vm);
        }

        public ActionResult ProductDetail(int id)
        {
            var product = db.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductID == id);
            if (product == null) return HttpNotFound();
            ViewBag.RelatedProducts = db.Products.Where(p => p.CategoryID == product.CategoryID && p.ProductID != id).Take(4).ToList();
            return View(product);
        }
    }
}
