using _Models.DAO;
using _Models.EF;
using ShopOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace ShopOnline.Areas.Admin.Controllers
{
    public class FeedBackController : BaseController
    {
        // GET: Admin/FeedBack
        public ActionResult Index(string searchFeedBack="",int page=1,int pagesize=2)
        {
            ViewBag.searchFeedBack = searchFeedBack;
            if (searchFeedBack == "")
            {
                FeedbackDAO feedbackDAO = new FeedbackDAO();
                var feedBack = feedbackDAO.SelectAll().ToPagedList(page, pagesize);
                return View(feedBack);
            }
            else
            {
                FeedbackDAO feedbackDAO = new FeedbackDAO();
                var feedBack = feedbackDAO.SelectCondition(searchFeedBack).ToPagedList(page, pagesize);
                return View(feedBack);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        public JsonResult ChangeStatus(int feedbackID)
        {
            FeedbackDAO feedbackDAO = new FeedbackDAO();
            bool tempstatus = feedbackDAO.ChangeStatus(feedbackID);
            return Json(new
            {
                status = tempstatus
            });
        }
        [HttpPost]
        public ActionResult Create(Feedback feedback,string tempstatus)
        {
            if (ModelState.IsValid)
            {
                if (tempstatus == "Kích Hoạt")
                {
                    feedback.Status = true;
                }
                else feedback.Status = false;
                feedback.CreateDate = DateTime.Now;
                FeedbackDAO feedbackDAO = new FeedbackDAO();
                var check = feedbackDAO.Create(feedback);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Create Success");
                    return RedirectToAction("Create", "FeedBack");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Không Thành Công");
                }
            }
            return View();
        }
        [HttpDelete]
        public ActionResult Remove(int feedbackid)
        {
            FeedbackDAO feedbackDAO = new FeedbackDAO();
            feedbackDAO.Remove(feedbackid);
            return RedirectToAction("Index");
        }
        private static Feedback feedbackeditting;
        [HttpGet]
        public ActionResult Edit(int id)
        {
            FeedbackDAO feedbackDAO = new FeedbackDAO();
            var feedback = feedbackDAO.GetFeedback(id);
            feedbackeditting = feedback;
            return View(feedback);
        }
        [HttpPost]
        public ActionResult Edit(Feedback feedback,string tempstatus)
        {
            if (ModelState.IsValid)
            {
                if (tempstatus == "Kích Hoạt")
                {
                    feedback.Status = true;
                }
                else feedback.Status = false;
                FeedbackDAO feedbackDAO = new FeedbackDAO();
                int check = feedbackDAO.Edit(feedback);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Edit Thành Công");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Edit Không Thành Công");
                }
            }
            return View(feedbackeditting);
        }
    }

}