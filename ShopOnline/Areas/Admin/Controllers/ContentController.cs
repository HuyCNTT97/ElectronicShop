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
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string SearchContent, int page = 1, int pagesize = 2)
        {
            ContentDAO contentDAO = new ContentDAO();
            IPagedList<Content> danhsach;
            if (SearchContent == "" || SearchContent == null)
            {
                danhsach = contentDAO.listAll().ToPagedList(page, pagesize);
            }
            else
            {
                danhsach = contentDAO.ListAllWithCondition(SearchContent).ToPagedList(page, pagesize);
            }
            @ViewBag.Search = SearchContent;
            return View(danhsach);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Content content, string trangthai)
        {
            SetViewBag();
            if (ModelState.IsValid == true)
            {
                if (trangthai == "Mở")
                {
                    content.Status = true;
                }
                else
                {
                    content.Status = false;
                }
                content.CreateDate = DateTime.Now;
                ContentDAO contentDAO = new ContentDAO();
                var id = contentDAO.ThemContent(content);
                if (id > 0)
                {

                    TempData["msg"] = MessageBox.Show("Thêm Thành Công");
                    return RedirectToAction("Create", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Không Thêm User Thành Công");
                }
            }
            return View("Create");

        }
        public void SetViewBag(int? selectedID = null)
        {
            NewsDAO newsDAO = new NewsDAO();

            ViewBag.NewsID = new SelectList(newsDAO.ListAll(), "NewsID", "NewsName", selectedID);
        }
        [HttpGet]
        public ActionResult Edit(int idContentEdit)
        {
            SetViewBagEditNews();
            ContentDAO contentDAO = new ContentDAO();
            var ContentChange = contentDAO.TimContent(idContentEdit);
            return View(ContentChange);
        }
        public void SetViewBagEditNews(int id = 1)
        {
            NewsDAO newsDAO = new NewsDAO();
            ViewBag.TenNews = new SelectList(newsDAO.ListAll(), "NewsID", "NewsName", id);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Content contentChange, string trangthai)
        {
            SetViewBagEditNews();
            if (ModelState.IsValid)
            {
                if (trangthai == "Mở")
                {
                    contentChange.Status = true;
                }
                else
                {
                    contentChange.Status = false;
                }
                contentChange.ModifiedBy = BaseController.NguoiDungHienTai.UserAccount;
                contentChange.ModifiedDate = DateTime.Now.ToString();
                ContentDAO contentDAO = new ContentDAO();
                var id = contentDAO.Sua(contentChange);
                if (id == 1)
                {
                    TempData["msg"] = MessageBox.Show("Edit Thành Công");
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Edit không thành công");
                }
            }
            return View("Edit");
        }
        [HttpDelete]
        public ActionResult Remove(int id)
        {
            ContentDAO contentDAO = new ContentDAO();
            contentDAO.Remove(id);
            return RedirectToAction("Index", "Content");
        }
        [HttpPost]
        public JsonResult ChangeStatus(int ContentID)
        {
            ContentDAO contentDAO = new ContentDAO();
            var tempstatus = contentDAO.ChangeStatus(ContentID);
            return Json(new
            {
                status = tempstatus
            });
        }
    }
}