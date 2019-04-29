using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using _Models.DAO;
using _Models.EF;
namespace ShopOnline.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        public ActionResult Index(string SearchOrder="", int page=1,int pagesize=2)
        {
            OrderDAO OrderDAO = new OrderDAO();
            ViewBag.Search = SearchOrder;
            if (SearchOrder == "")
            {
                
                var list = OrderDAO.ListAll().ToPagedList(page, pagesize);
                return View(list);
            }
            else
            {
                var list = OrderDAO.ListAllWithCondition(SearchOrder).ToPagedList(page, pagesize);
                return View(list);
            }
            
        }
        [HttpPost]
        public JsonResult ChangeStatus(int orderID)
        {
            OrderDAO orderDAO = new OrderDAO();
            var tempstatus = orderDAO.ChangeStatus(orderID);
            return Json(new
            {
                status = tempstatus
            });
        }
    }
}