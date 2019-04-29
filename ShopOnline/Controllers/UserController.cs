using _Models.DAO;
using _Models.EF;
using BotDetect.Web.Mvc;
using Facebook;
using ShopOnline.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class UserController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
       [ChildActionOnly]
        public ActionResult Create()
        {
            CustomerAccount userAccount = new CustomerAccount();
            return PartialView(userAccount);
        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppID"],
                client_secret = ConfigurationManager.AppSettings["KhoaBiMat"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code,
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken= accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;
                CustomerAccount customerAccount = new CustomerAccount();
                customerAccount.Email = email;
                customerAccount.Name = firstname + " " + middlename + " " + lastname;
                customerAccount.PassWord = Encryptor.MD5Hash("Huypro123");
                customerAccount.TelephoneNumber = 0;
                customerAccount.ConfirmPassWord= Encryptor.MD5Hash("Huypro123");
                customerAccount.GioiTinh = "Nam";
                CustomerAccountDAO customerAccountDAO = new CustomerAccountDAO();
                var check = customerAccountDAO.CreateFacebook(customerAccount);
                if (check > 0)
                {
                    var customersession = new CustomerLogin();
                    customersession.Email = customerAccount.Email;
                    customersession.Name = customerAccount.Name;
                    Session.Add(Common.CommonConstants.Customer_SESSION, customersession);
                }

            }
                    return Redirect("/");
        }
        [HttpGet]
        public ActionResult DangXuat()
        {
            Session[Common.CommonConstants.Customer_SESSION] = null;
            return Redirect("/");
        }
        [HttpPost]
        public ActionResult LoginCustomer(string txtEmail,string txtPassWord)
        {
            CustomerAccountDAO customerAccountDAO = new CustomerAccountDAO();
            var result = customerAccountDAO.CheckLogin(txtEmail, Encryptor.MD5Hash(txtPassWord));
            if (result == 1)
            {
                var customer = customerAccountDAO.getCustomerAccount(txtEmail);
                var customersession = new CustomerLogin();
                customersession.Email = customer.Email;
                customersession.PassWord = customer.PassWord;
                customersession.Name= customer.Name;
                Session.Add(Common.CommonConstants.Customer_SESSION, customersession);
                return Redirect("/");
            }
            else if (result == 0)
            {
                TempData["msg"] = "<script>alert('tên đăng nhập không tồn tại!')</script>";

            }
            else if (result == -1)
            {
                //ModelState.AddModelError("", "Bạn đã điền sai password");

                TempData["msg"] = "<script>alert('bạn đã điền sai mật khẩu!')</script>";

            }
            return Redirect("/");

        }
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppID"],
                client_secret = ConfigurationManager.AppSettings["KhoaBiMat"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }
        [HttpPost]
        [AllowAnonymous]
        [SimpleCaptchaValidation("CaptchaCode", "registerCapcha", "Mã xác nhận không đúng!")]
        public ActionResult Register(CustomerAccount customerAccount,string gioiTinh)
        {
            if (ModelState.IsValid == true)
            {
                customerAccount.GioiTinh = gioiTinh;
                customerAccount.PassWord = Encryptor.MD5Hash(customerAccount.PassWord);
                customerAccount.ConfirmPassWord = Encryptor.MD5Hash(customerAccount.ConfirmPassWord);
                CustomerAccountDAO customerAccountDAO = new CustomerAccountDAO();
                var check = customerAccountDAO.CreateAccount(customerAccount);
                if (check == 1)
                {
                    TempData["msg"] = MessageBox.Show("Đăng kí thành công");
                    return RedirectToAction("Register", "User");
                }
                else
                {
                    TempData["msg"] = MessageBox.Show("Email này đã có người đăng kí");
                }
            }
            return View("Register");
        }
    }
}