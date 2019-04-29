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
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index(string searchString="",int page=1,int pagesize = 2)
        {
            ViewBag.searchString = searchString;
            MenuDAO menuDAO = new MenuDAO();
            if (searchString == "")
            {
                var list = menuDAO.SelectAll().ToPagedList(page, pagesize);
                return View(list);
            }
            else
            {
                var list = menuDAO.SelectWithCondition(searchString).ToPagedList(page, pagesize);
                return View(list);
            }
        }
        [HttpGet]
       public ActionResult Create()
        {
            SetMenuType();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Menu menu,string tempstatus)
        {
            
            if (ModelState.IsValid)
            {
                if (tempstatus == "Kích Hoạt")
                {
                    menu.Status = true;
                }
                else menu.Status = false;
                MenuDAO menuDAO = new MenuDAO();
                var check = menuDAO.Create(menu);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Create Success");
                    return RedirectToAction("Create", "Menu");
                }
                else
                {
                    ModelState.AddModelError("", "Create fail");
                }
            }
            SetMenuType();
            return View();
        }
        private static int priorpage;
        [HttpGet]
        public ActionResult Edit(int MenuID,int pageprior)
        {
            priorpage = pageprior;
            MenuDAO menuDAO = new MenuDAO();
            var menu = menuDAO.GetMenu(MenuID);
            SetMenuType();
            return View(menu);
        }
        [HttpPost]
        public ActionResult Edit(Menu menu,int id,string tempstatus)
        {
            SetMenuType();
            menu.MenuID = id;
            if (ModelState.IsValid)
            {
                if (tempstatus == "Kích Hoạt")
                {
                    menu.Status = true;
                }
                else menu.Status = false;
                MenuDAO menuDAO = new MenuDAO();
                var check = menuDAO.Edit(menu);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Edit successful");
                    return RedirectToAction("Index", "Menu",new {@page=priorpage });
                }
                else
                {

                }
            }
            return View(new {@menu=menu,@id=id });
        }
        public void SetMenuType(int itemselectedID=1)
        {
            MenuTypeDAO menuTypeDAO = new MenuTypeDAO();
            var ListMenuType = menuTypeDAO.SelectAll();
            ViewBag.ListMenuType = new SelectList(ListMenuType, "MenuTypeID", "Name", itemselectedID);
        }
        [HttpDelete]
        public ActionResult Remove(int menuID)
        {
            MenuDAO menuDAO = new MenuDAO();
            menuDAO.Remove(menuID);
            return RedirectToAction("Index","Menu");
        }
        public JsonResult ChangeStatus(int menuID)
        {
            MenuDAO menuDAO = new MenuDAO();
            var tempstatus=menuDAO.ChangeStatus(menuID);
            return Json(new
            {
                status = tempstatus
            });
        }
    }
}