using _Models.DAO;
using _Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace ShopOnline.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index(int id=0,int page=1,int pagesize=5)
        {
            if (id == 5) id = 0;
            NewsDAO newsDAO = new NewsDAO();
            ViewBag.NewsName = newsDAO.getNews(id).NewsName;
            var list = newsDAO.getContens(id).ToPagedList(page,pagesize);
            return View(list);
        }

        [OutputCache(Duration = 60 * 30)]
        [ChildActionOnly]
        public ActionResult ListNews()
        {
            NewsDAO newsDAO = new NewsDAO();
            var list = newsDAO.ListAll();
            newsDAO.ChuyenDoi(list);
            return PartialView(list);
        }
        public ActionResult DetailContent(int id)
        {
            ContentDAO contentDAO = new ContentDAO();
            var content = contentDAO.GetContent(id);

            return View(content);
        }
    }
}