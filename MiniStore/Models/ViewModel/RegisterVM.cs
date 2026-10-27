using System.ComponentModel.DataAnnotations;

namespace MiniStore.Models.ViewModel
{
    public class RegisterVM
    {
        [Required, StringLength(255, MinimumLength = 3)]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }
        [Required, StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password), Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Required, Compare("Password"), DataType(DataType.Password), Display(Name = "Nhập lại mật khẩu")]
        public string ConfirmPassword { get; set; }
        [Required, Display(Name = "Họ tên")]
        public string CustomerName { get; set; }
        [Required, Display(Name = "Số điện thoại")]
        public string CustomerPhone { get; set; }
        [EmailAddress, Display(Name = "Email")]
        public string CustomerEmail { get; set; }
        [Display(Name = "Địa chỉ")]
        public string CustomerAddress { get; set; }
    }
}
