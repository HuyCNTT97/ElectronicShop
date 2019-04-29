using _Models.DAO;
using _Models.EF;
using PagedList;
using ShopOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class MenuTypeController : BaseController
    {
        // GET: Admin/MenuType
        public ActionResult Index(string searchString="",int page=1,int pagesize=2)
        {
            ViewBag.searchString = searchString;
            MenuTypeDAO menuTypeDAO = new MenuTypeDAO();
            
            if (searchString == "")
            {
                var list = menuTypeDAO.SelectAll().ToPagedList(page, pagesize);
                return View(list);
            }
            else
            {
                var list = menuTypeDAO.SelectWithCondition(searchString).ToPagedList(page, pagesize);
                return View(list);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(MenuType menuType)
        {
            if (ModelState.IsValid)
            {
                MenuTypeDAO menuTypeDAO = new MenuTypeDAO();
                var check=menuTypeDAO.Create(menuType);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Thêm Thành Công");
                    return RedirectToAction("Create", "MenuType");
                }
                else
                {
                    ModelState.AddModelError("", "");
                }
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            MenuTypeDAO menuTypeDAO = new MenuTypeDAO();
            var menuType = menuTypeDAO.GetMenuType(id);
            return View(menuType);
        }
        [HttpPost]
        public ActionResult Edit(MenuType menuType)
        {
            if (ModelState.IsValid)
            {
                MenuTypeDAO menuTypeDAO = new MenuTypeDAO();
                var check = menuTypeDAO.Edit(menuType);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Edit Thành Công");
                    return RedirectToAction("Index", "MenuType");
                }
                else
                {
                    ModelState.AddModelError("","Edit Thành Công");
                }
            }
            return View();
        }
        [HttpDelete]
        public ActionResult Remove(int menutypeID)
        {
            MenuTypeDAO menuTypeDAO = new MenuTypeDAO();
            menuTypeDAO.Remove(menutypeID);
            return RedirectToAction("Index");
        }
    }
}