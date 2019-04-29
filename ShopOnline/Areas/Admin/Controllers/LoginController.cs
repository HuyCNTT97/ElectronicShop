using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _Models.DAO;
using ShopOnline.Common;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                int result = dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                if (result==1)
                {
                    var user = dao.GetID(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserID = user.UserID;
                    userSession.UserAccount = user.UserAccount1;
                    Session.Add(Common.CommonConstants.User_SESSION,userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản hiện đang bị khóa");
                }
                else if(result==-2)
                {
                    ModelState.AddModelError("", "Bạn đã điền sai password");
                }
            }
            return View("Index");
        }
    }
}