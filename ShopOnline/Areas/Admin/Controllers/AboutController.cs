using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _Models.EF;
using _Models.DAO;
using ShopOnline.Common;
using PagedList;
namespace ShopOnline.Areas.Admin.Controllers
{
    public class AboutController : BaseController
    {
        // GET: Admin/About
        public ActionResult Index(string SearchAbout,int page=1,int pagesize=2)
        {
            if (SearchAbout == null)
            {
                SearchAbout = "";
            }
            if (SearchAbout != null)
            {
                ViewBag.SearchAbout = SearchAbout;
            }
           
            AboutDAO aboutDAO = new AboutDAO();
            if (SearchAbout == "")
            {
              
                IPagedList<About> listAbout = aboutDAO.SelectAll().ToPagedList(page, pagesize);
                return View(listAbout);
            }
            else
            {
                IPagedList<About> listAbout = aboutDAO.SelectWithCondition(SearchAbout).ToPagedList(page, pagesize);
                return View(listAbout);
            }
        }
        [HttpPost]
        public JsonResult Changestatus(int aboutid)
        {
            AboutDAO aboutDAO = new AboutDAO();
            var tempstatus = aboutDAO.ChangeStatus(aboutid);
            return Json(new{
                status= tempstatus
            });
        }
        [HttpGet]
        public ActionResult Edit(int aboutid)
        {
            AboutDAO aboutDAO = new AboutDAO();
            var about = aboutDAO.GetAbout(aboutid);
            return View(about);
        }
        [HttpPost]
        public ActionResult Edit(About about,string tempstatus,int id)
        {
            about.AboutID = id;
            if (ModelState.IsValid)
            {
                if (tempstatus == "Khóa")
                {
                    about.Status = false;
                }
                else about.Status = true;
                about.ModifiedBy = BaseController.NguoiDungHienTai.UserAccount;
                about.ModifiedDate = DateTime.Now.ToString();
                AboutDAO aboutDAO = new AboutDAO();

                var check = aboutDAO.Edit(about);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Thành Công");
                    return RedirectToAction("Index", "About");
                }
                else
                {
                    ModelState.AddModelError("", "Không Edit Thành Công");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpDelete]
        public ActionResult Remove(int AboutID)
        {
            AboutDAO aboutDAO = new AboutDAO();
            aboutDAO.Remove(AboutID);
            return RedirectToAction("Index","About");
        }
        [HttpPost,ValidateInput(false)]
        public ActionResult Create(About about,string tempstatus)
        {
            if (ModelState.IsValid)
            {
                AboutDAO aboutDAO = new AboutDAO();
                if (tempstatus == "Khóa")
                {
                    about.Status = false;
                }
                else about.Status = true;
                about.CreateBy = BaseController.NguoiDungHienTai.UserAccount;
                var success = aboutDAO.CreateAbout(about);
                if (success > 0)
                {
                    TempData["msg"] = MessageBox.Show("Thêm About Thành Công");
                    return RedirectToAction("Create", "About");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Không Thành Công");
                }
            }
            return View("Create");
        }
    }
}