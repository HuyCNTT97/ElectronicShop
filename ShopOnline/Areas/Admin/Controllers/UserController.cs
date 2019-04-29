using _Models.EF;
using _Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Common;
using PagedList;
namespace ShopOnline.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [AllowAnonymous]
        public ActionResult Index(string SearchUser, int page=1,int pageSize=5)
        {
            UserDAO userDAO = new UserDAO();
            var PermissionID = userDAO.GetPermissionID(BaseController.NguoiDungHienTai.UserAccount);
            if (PermissionID == 1)
            {
                var danhSachAllUser = userDAO.ListAll(SearchUser).ToPagedList(page, pageSize);
                return View(danhSachAllUser);
            }
            else
            {
                TempData["warning"] = MessageBox.Show("Bạn không có quyền truy cập");
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            UserDAO userDAO = new UserDAO();
          
           var ketqua = userDAO.ChangeStatus(id);
            return Json(new
            {
                status = ketqua
            });

        }
        [HttpGet]
        public ActionResult Create()
        {
            UserDAO userDAO = new UserDAO();
            var PermissionID = userDAO.GetPermissionID(BaseController.NguoiDungHienTai.UserAccount);
            if (PermissionID == 1)
            {
                GetPermission();
                return View();
            }
            else
            {
                TempData["warning"] = MessageBox.Show("Bạn không có quyền truy cập");

                return RedirectToAction("Index", "Home");
            }
        }
        public void GetPermission(int id = 0)
        {
            PermissionDAO permissionDAO = new PermissionDAO();
            var listPermission = permissionDAO.ListAll();
            ViewBag.ListPermission = new SelectList(listPermission, "PermissionID", "PermissionName", id);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            GetPermission();
            UserDAO userDAO = new UserDAO();
            var PermissionID = userDAO.GetPermissionID(BaseController.NguoiDungHienTai.UserAccount);
            if (PermissionID == 1)
            {
                UserAccount nguoiDungCanThayDoi = userDAO.UserDetail(id);
                nguoiDungCanThayDoi.UserAccount1 = nguoiDungCanThayDoi.UserAccount1.Trim();
                return View(nguoiDungCanThayDoi);
            }
            else
            {
                TempData["warning"] = MessageBox.Show("Bạn không có quyền truy cập");

                return RedirectToAction("Index", "Home");
            }
            

        }
        [HttpPost]
        public ActionResult Create(UserAccount userAccount,string tempstatus)
        {
            if (ModelState.IsValid)
            {
                UserDAO dao = new UserDAO();
                if(tempstatus=="Kích Hoạt")
                {
                    userAccount.Status = true;
                }
                else
                {
                    userAccount.Status = false;
                }
                var mecryptMD5 = Encryptor.MD5Hash(userAccount.PassWord);
                userAccount.PassWord = mecryptMD5;
                long id = dao.Insert(userAccount);
                if (id > 0)
                {
                    TempData["msg"] = MessageBox.Show("Create Successful");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    

                    ModelState.AddModelError("","Không them user thanh cong");
                }
            }
            GetPermission();
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(UserAccount userAccount,int AccountID)
        {
            userAccount.UserID = AccountID;
            GetPermission();
            if (ModelState.IsValid)
            {
                UserDAO dao = new UserDAO();

                var mecryptMD5 = Encryptor.MD5Hash(userAccount.PassWord);
                userAccount.PassWord = mecryptMD5;
                bool id = dao.Update(userAccount);
                if (id)
                {
                    TempData["msg"] = MessageBox.Show("Edit successful");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    TempData["warning"] = MessageBox.Show("Bạn không có quyền truy cập");

                    ModelState.AddModelError("", "Không cập nhập user thanh cong");
                }
            }
            return View("Index");
        }
      
        [HttpDelete]
        public ActionResult Remove(int IDnguoiDungCanXoa)
        {
           
            UserDAO userDAO = new UserDAO();
            UserAccount nguoiDungCanXoa = userDAO.UserDetail(IDnguoiDungCanXoa);
            userDAO.RemoveAccount(nguoiDungCanXoa);
            return RedirectToAction("Index", "User");
        }
    }
}