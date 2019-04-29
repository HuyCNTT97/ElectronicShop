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
    public class TagController : BaseController
    {
        // GET: Admin/Tag
        public ActionResult Index(string searchString="",int page=1,int pagesize=2)
        {
            ViewBag.searchString = searchString;
            TagDAO tagDAO = new TagDAO();
            if (searchString == "")
            {
                var listTag = tagDAO.SelectAll().ToPagedList(page, pagesize);
                return View(listTag);
            }
            else
            {
                var listTag = tagDAO.SelectWithCondition(searchString).ToPagedList(page, pagesize);
                return View(listTag);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                TagDAO tagDAO = new TagDAO();
                var check = tagDAO.Create(tag);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Create successfully");
                    return RedirectToAction("Create", "Tag");
                }
                else
                {
                    ModelState.AddModelError("", "Edit fail");
                }
            }
            return View();
        }
        [HttpDelete]
        public ActionResult Remove(int TagID)
        {
            TagDAO tagDAO = new TagDAO();
            tagDAO.Remove(TagID);
            return RedirectToAction("Index", "Tag");
        }
        private static int prioripage;
        [HttpGet]
        public ActionResult Edit(int id,int page)
        {
            prioripage = page;
            TagDAO tagDAO = new TagDAO();
            var listTag = tagDAO.GetTag(id);
            return View(listTag);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tag tag,int id)
        {

            tag.TagID = id;
            if (ModelState.IsValid)
            {
                TagDAO tagDAO = new TagDAO();
                var check = tagDAO.Edit(tag);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Edit successfully");
                    return RedirectToAction("Index", "Tag", new { @page = prioripage });
                }
                else
                {
                    ModelState.AddModelError("", "Edit fail");
                }
            }
            return View();
        }

    }
}