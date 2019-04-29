using _Models.DAO;
using _Models.EF;
using ShopOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
namespace ShopOnline.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Index()
        {
            TempData["slides"] = new SlideDAO().SelectAll().ToList();
            TempData["listnewproduct"] = new ProductDAO().ListNewProduct(4);
            TempData["listhotproduct"] = new ProductDAO().ListHotProduct(4);
            return View();
        }
        [ChildActionOnly]
        [OutputCache(Duration = 60 * 30)]
        public ActionResult MainMenu()
        {
            MenuDAO menuDAO = new MenuDAO();
            var menus = menuDAO.ListMenu(24);
            return PartialView(menus);
        }
        [ChildActionOnly]
        public ActionResult OptionAccount()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            MenuDAO menuDAO = new MenuDAO();
            var menus = menuDAO.ListMenu(23);
            return PartialView(menus);  
        }
        public ActionResult Information()
        {
            var session = (Common.CustomerLogin)Session[Common.CommonConstants.Customer_SESSION];
            if (session == null)
            {
                return Redirect("/");
            }
            CustomerAccountDAO customerAccountDAO = new CustomerAccountDAO();
            var customer=customerAccountDAO.getCustomerAccount(session.Email);
            return View(customer);
        }
        [HttpPost]
        public ActionResult Information(CustomerAccount customerAccount,string newPassword)
        {
            
            CustomerAccountDAO customerAccountDAO = new CustomerAccountDAO();
            customerAccount.PassWord = Common.Encryptor.MD5Hash(customerAccount.PassWord);
            customerAccount.ConfirmPassWord = Common.Encryptor.MD5Hash(customerAccount.ConfirmPassWord);
            if (customerAccountDAO.CheckPass(customerAccount) != 1)
            {
                TempData["msg"] = MessageBox.Show("Bạn đã nhập sai mật khẩu");
                return Redirect("/quan-li-tai-khoan");
            }
            customerAccount.PassWord = Common.Encryptor.MD5Hash(newPassword);

            int check = customerAccountDAO.Edit(customerAccount);
            if (check > 0)
            {
                Session[Common.CommonConstants.Customer_SESSION] = null;
                TempData["msg"] = MessageBox.Show("Cập Nhập Thành Công Mời Bạn Đăng Nhập Lại");
                return Redirect("/");
            }
            else
            {
                TempData["msg"] = MessageBox.Show("Cập Nhập Thất Bại, Bạn đã điền sai mật khẩu");
                return Redirect("/quan-li-tai-khoan");
            }
        }
        [OutputCache(Duration = 60 * 30)]
        public ActionResult huongdanmuahang()
        {
            return View();
        }
        [OutputCache(Duration = 60 * 30)]
        public ActionResult giaohang_doitra()
        {
            return View();
        }
      
    }

}