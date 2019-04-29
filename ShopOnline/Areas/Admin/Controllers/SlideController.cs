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
    public class SlideController : BaseController
    {
        // GET: Admin/Slide
        public ActionResult Index(string searchString="",int page=1,int pagesize=2)
        {
            ViewBag.searchString = searchString;
            SlideDAO slideDAO = new SlideDAO();
            if (searchString == "")
            {
                var listSlide = slideDAO.SelectAll().ToPagedList(page, pagesize);
                return View(listSlide);
            }
            else
            {
                var listSlide = slideDAO.SelectWithConditon(searchString).ToPagedList(page, pagesize);
                return View(listSlide);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Slide slide,string tempstatus)
        {
           
            if (ModelState.IsValid)
            {
                if (tempstatus == "Kích Hoạt")
                    slide.Status = true;
                else slide.Status = false;
               
                SlideDAO slideDAO = new SlideDAO();
                var check = slideDAO.Create(slide);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Create successfully");
                    return RedirectToAction("Create","Slide");
                }
                else
                {
                    ModelState.AddModelError("", "Create fail");
                }
            }
            return View();
        }
        [HttpDelete]
        public ActionResult Remove(int SlideID)
        {
            SlideDAO slideDAO = new SlideDAO();
            slideDAO.Remove(SlideID);
            return RedirectToAction("Index", "Slide");
        }
        private int tempSlideID;
        [HttpGet]
        public ActionResult Edit(int id)
        {
            tempSlideID = id;
            SlideDAO slideDAO = new SlideDAO();
            var Slide = slideDAO.GetSlide(id);
            return View(Slide);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slide slide,string tempstatus,int id)
        {
            slide.SlideID = id;
            SlideDAO slideDAO = new SlideDAO();
            if (ModelState.IsValid)
            {
                if (tempstatus == "Kích Hoạt")
                {
                    slide.Status = true;
                }
                else slide.Status = false;
               
                var check = slideDAO.Edit(slide);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Edit successfully");
                    return RedirectToAction("Index", "Slide");
                }
                else
                {
                    ModelState.AddModelError("", "Edit fail");
                }
            }
            return View(slideDAO.GetSlide(tempSlideID));
        }
        [HttpPost]
        public JsonResult ChangeStatus(int SlideID)
        {
            SlideDAO slideDAO = new SlideDAO();
            var tempstatus = slideDAO.ChangeStatus(SlideID);
            return Json(new {
                status = tempstatus
            });
        }
    }
}