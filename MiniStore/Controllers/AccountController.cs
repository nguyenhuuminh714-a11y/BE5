using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using MiniStore.Models;
using MiniStore.Models.ViewModel;

namespace MiniStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly StoreDb db = new StoreDb();
        private static string Hash(string value)
        {
            using (var sha = SHA256.Create())
                return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(value ?? "")));
        }

        [HttpGet] public ActionResult Register() { return View(); }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            if (db.Users.Any(u => u.Username == vm.Username)) { ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại."); return View(vm); }
            db.Users.Add(new User { Username = vm.Username, Password = Hash(vm.Password), UserRole = "C" });
            db.Customers.Add(new Customer { Username = vm.Username, CustomerName = vm.CustomerName, CustomerPhone = vm.CustomerPhone, CustomerEmail = vm.CustomerEmail, CustomerAddress = vm.CustomerAddress });
            db.SaveChanges();
            FormsAuthentication.SetAuthCookie(vm.Username, false);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet] public ActionResult Login() { return View(); }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var user = db.Users.FirstOrDefault(u => u.Username == vm.Username && u.Password == Hash(vm.Password));
            if (user == null) { ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng."); return View(vm); }
            Session["UserRole"] = user.UserRole;
            FormsAuthentication.SetAuthCookie(user.Username, vm.RememberMe);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult ProfileInfo()
        {
            var c = db.Customers.FirstOrDefault(x => x.Username == User.Identity.Name);
            if (c == null) return HttpNotFound();
            return View(c);
        }

        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(string CustomerPhone, string CustomerEmail, string CustomerAddress)
        {
            var c = db.Customers.FirstOrDefault(x => x.Username == User.Identity.Name);
            if (c == null) return HttpNotFound();
            c.CustomerPhone = CustomerPhone; c.CustomerEmail = CustomerEmail; c.CustomerAddress = CustomerAddress;
            db.SaveChanges();
            return RedirectToAction("ProfileInfo");
        }

        public ActionResult Logout() { FormsAuthentication.SignOut(); Session.Clear(); return RedirectToAction("Index", "Home"); }
    }
}
