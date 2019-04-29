using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class LoginModel
    {
        private string userName;
        private string passWord;
        public bool Remember { set; get; }
        [Required(ErrorMessage = "Mời bạn nhập user name")]

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }
        [Required(ErrorMessage = "Mời bạn nhập user password")]

        public string PassWord
        {
            get
            {
                return passWord;
            }

            set
            {
                passWord = value;
            }
        }
    }
}