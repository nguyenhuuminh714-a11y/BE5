using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MiniStore.Models;
using MiniStore.Models.ViewModel;

namespace MiniStore.Controllers
{
    public class CartController : Controller
    {
        private List<CartItem> GetCart() {
            if (Session["Cart"] == null) Session["Cart"] = new List<CartItem>();
            return (List<CartItem>)Session["Cart"];
        }

        public ActionResult Index() { return View(GetCart()); }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddItem(int id, int quantity = 1)
        {
            using (var db = new StoreDb())
            {
                var p = db.Products.Find(id);
                if (p == null) return HttpNotFound();
                var cart = GetCart();
                var item = cart.FirstOrDefault(x => x.ProductID == id);
                if (item == null) cart.Add(new CartItem { ProductID = p.ProductID, ProductName = p.ProductName, ProductImage = p.ProductImage, UnitPrice = p.ProductPrice, Quantity = System.Math.Max(1, quantity) });
                else item.Quantity += System.Math.Max(1, quantity);
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var item = GetCart().FirstOrDefault(x => x.ProductID == id);
            if (item != null) { if (quantity <= 0) GetCart().Remove(item); else item.Quantity = quantity; }
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult RemoveItem(int id) { var item = GetCart().FirstOrDefault(x => x.ProductID == id); if (item != null) GetCart().Remove(item); return RedirectToAction("Index"); }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ClearCart() { Session["Cart"] = new List<CartItem>(); return RedirectToAction("Index"); }
    }
}
