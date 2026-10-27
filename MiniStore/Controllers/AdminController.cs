using System.Linq;
using System.Web.Mvc;
using MiniStore.Models;

namespace MiniStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly StoreDb db = new StoreDb();
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            if (Session["UserRole"] == null || (string)Session["UserRole"] != "A")
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Login" }));
                return;
            }
            base.OnActionExecuting(filterContext);
        }

        public ActionResult Index() { ViewBag.CategoryCount = db.Categories.Count(); ViewBag.ProductCount = db.Products.Count(); ViewBag.OrderCount = db.Orders.Count(); return View(); }

        public ActionResult Categories() { return View(db.Categories.OrderBy(c => c.CategoryID).ToList()); }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateCategory(string CategoryName)
        {
            if (!string.IsNullOrWhiteSpace(CategoryName)) { db.Categories.Add(new Category { CategoryName = CategoryName.Trim() }); db.SaveChanges(); }
            return RedirectToAction("Categories");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int id)
        {
            var c = db.Categories.Find(id);
            if (c != null && !db.Products.Any(p => p.CategoryID == id)) { db.Categories.Remove(c); db.SaveChanges(); }
            return RedirectToAction("Categories");
        }

        public ActionResult Products(string searchTerm)
        {
            var q = db.Products.Include("Category").AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm)) q = q.Where(p => p.ProductName.Contains(searchTerm));
            ViewBag.SearchTerm = searchTerm;
            ViewBag.Categories = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View(q.OrderBy(p => p.ProductID).ToList());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateProduct(int CategoryID, string ProductName, string ProductDescription, decimal ProductPrice, string ProductImage)
        {
            if (ProductPrice >= 0 && !string.IsNullOrWhiteSpace(ProductName) && db.Categories.Any(c => c.CategoryID == CategoryID))
            {
                db.Products.Add(new Product { CategoryID = CategoryID, ProductName = ProductName.Trim(), ProductDescription = ProductDescription ?? "", ProductPrice = ProductPrice, ProductImage = ProductImage });
                db.SaveChanges();
            }
            return RedirectToAction("Products");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id)
        {
            var p = db.Products.Find(id);
            if (p != null && !db.OrderDetails.Any(d => d.ProductID == id)) { db.Products.Remove(p); db.SaveChanges(); }
            return RedirectToAction("Products");
        }
    }
}
