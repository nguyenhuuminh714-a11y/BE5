using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MiniStore.Models;
using MiniStore.Models.ViewModel;

namespace MiniStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        [HttpGet]
        public ActionResult Checkout()
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null || !cart.Any()) return RedirectToAction("Index", "Cart");
            using (var db = new StoreDb())
            {
                var customer = db.Customers.FirstOrDefault(c => c.Username == User.Identity.Name);
                if (customer == null) return RedirectToAction("Register", "Account");
                ViewBag.Customer = customer;
                return View(cart);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Checkout(string ShippingAddress, string DeliveryMethod, string PaymentMethod)
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null || !cart.Any()) return RedirectToAction("Index", "Cart");
            using (var db = new StoreDb())
            {
                var customer = db.Customers.FirstOrDefault(c => c.Username == User.Identity.Name);
                if (customer == null) return RedirectToAction("Register", "Account");
                if (string.IsNullOrWhiteSpace(ShippingAddress)) { ModelState.AddModelError("", "Vui lòng nhập địa chỉ giao hàng."); ViewBag.Customer = customer; return View(cart); }
                var order = new Order { CustomerID = customer.CustomerID, OrderDate = DateTime.Today, AddressDelivery = ShippingAddress, DeliveryMethod = DeliveryMethod ?? "Giao hàng tiêu chuẩn", PaymentMethod = PaymentMethod ?? "COD", PaymentStatus = "Pending", TotalAmount = cart.Sum(x => x.TotalPrice) };
                db.Orders.Add(order); db.SaveChanges();
                foreach (var item in cart)
                    db.OrderDetails.Add(new OrderDetail { OrderID = order.OrderID, ProductID = item.ProductID, Quantity = item.Quantity, UnitPrice = item.UnitPrice });
                db.SaveChanges();
                Session["Cart"] = new List<CartItem>();
                return RedirectToAction("OrderSuccess", new { id = order.OrderID });
            }
        }

        public ActionResult OrderSuccess(int id)
        {
            using (var db = new StoreDb())
            {
                var order = db.Orders.Include("OrderDetails").FirstOrDefault(o => o.OrderID == id);
                if (order == null) return HttpNotFound();
                var customer = db.Customers.FirstOrDefault(c => c.CustomerID == order.CustomerID);
                if (customer == null || customer.Username != User.Identity.Name) return new HttpUnauthorizedResult();
                ViewBag.CustomerName = customer.CustomerName;
                return View(order);
            }
        }

        public ActionResult MyOrder()
        {
            using (var db = new StoreDb())
            {
                var customer = db.Customers.FirstOrDefault(c => c.Username == User.Identity.Name);
                if (customer == null) return RedirectToAction("Register", "Account");
                return View(db.Orders.Where(o => o.CustomerID == customer.CustomerID).OrderByDescending(o => o.OrderID).ToList());
            }
        }
    }
}
