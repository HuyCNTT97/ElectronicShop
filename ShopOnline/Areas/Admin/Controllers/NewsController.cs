using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _Models.DAO;
using _Models.EF;
using ShopOnline.Common;
using PagedList;
namespace ShopOnline.Areas.Admin.Controllers
{
    public class MySelectListItem
    {
        public MySelectListItem() { }
        public int? NewsID { get; set; }
        public string NewsName { get; set; }
    }
    public class NewsController : BaseController
    {
        // GET: Admin/News
        public ActionResult Index(int page=1,int pagesize=2)
        {
            TempData["page"] = page;
            NewsDAO newsDAO = new NewsDAO();
            var list = newsDAO.ListAll().ToPagedList(page, pagesize);
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetParentNewsName();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int NewsEditID)
        {
            NewsDAO newsDAO = new NewsDAO();
            var newsEditting = newsDAO.getNews(NewsEditID);
            SetParentNewsName();
            return View(newsEditting);
        }
        [HttpPost]
        public ActionResult Edit(News NewsEdit, string trangthai,int id)
        {
            NewsEdit.NewsID = id;
            if (ModelState.IsValid)
            {
                NewsDAO newsDAO = new NewsDAO();
                if(trangthai== "Khóa")
                {
                    NewsEdit.Status = false;
                }
                else
                {
                    NewsEdit.Status = true;
                }
                NewsEdit.ModifiedBy = BaseController.NguoiDungHienTai.UserAccount;
                int checksuccess = newsDAO.EditNews(NewsEdit);
                if (checksuccess > 0)
                {
                    TempData["msg"] = MessageBox.Show("Edit Thành Công");
                    return RedirectToAction("Index", "News");
                }
                else
                {
                    ModelState.AddModelError("", "Edit Không Thành Công");
                }
            }
            return View("Edit", "News",new { @NewsEditID=NewsEdit.NewsID });
        }
        [HttpPost]
        public ActionResult Create(string trangthai,string showOnTop,News news)
        {
            if (ModelState.IsValid)
            {
                if (trangthai == "Kích Hoạt")
                {
                    news.Status = true;
                }
                else news.Status = false;
                if (showOnTop == "Có")
                {
                    news.ShowOnHome = true;
                }
                else news.ShowOnHome = false;
                news.CreateDate = DateTime.Now;
                news.CreateBy = BaseController.NguoiDungHienTai.UserAccount;
                NewsDAO newsDAO = new NewsDAO();
                int thanhcong = newsDAO.AddNews(news);
                if (thanhcong == 1)
                {
                    TempData["msg"] = MessageBox.Show("Thêm News Thành Công");
                    return RedirectToAction("Create", "News");
                }
                else
                {
                    ModelState.AddModelError("", NewsDAO.msgerror);
                }
               
            }
            return RedirectToAction("Create", "News");
        }
        [HttpPost]
        public JsonResult ChangeStatus(int newsid)
        {
            NewsDAO newsDAO = new NewsDAO();
            var trangthai=newsDAO.ChangeStatus(newsid);
            return Json(new
            {
                status = trangthai
            });
        }
        private void SetParentNewsName(int optionSelected=0)
        {
            NewsDAO newsDAO = new NewsDAO();
            List<MySelectListItem> listitem = new List<MySelectListItem>();
            listitem.Add(new MySelectListItem() {NewsName="Không Có",NewsID=null });
            foreach (News item in newsDAO.ListAll())
            {
                listitem.Add(new MySelectListItem() { NewsName = item.NewsName, NewsID = item.NewsID });
            }
            ViewBag.ParentNewsName = new SelectList(listitem, "NewsID", "NewsName", optionSelected);
            
        }
        #region Xóa
        [HttpDelete]
        public ActionResult Remove(int IDNewsCanXoa)
        {
            NewsDAO newsDAO = new NewsDAO();
            string newsNameSelected = newsDAO.Xoa(IDNewsCanXoa);  
            return RedirectToAction("Index","News");
        }
        #endregion
    }
}