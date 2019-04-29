using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class LoginCustomer
    {
        [Display(Name ="Email")]
        [Required(ErrorMessage ="Bạn phải nhập Email")]
        public string Email { set; get; }
        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Bạn phải nhập Mật Khẩu")]
        public string PassWord { set; get; }
    }
}