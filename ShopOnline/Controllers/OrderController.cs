using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using _Models.EF;
using _Models.DAO;
using ShopOnline.Common;

namespace ShopOnline.Controllers
{
    public class OrderController : Controller
    {
        
        public class Item
        {
           
            private string text;
            private int value;
            public string Text
            {
                get
                {
                    return text;
                }

                set
                {
                    text = value;
                }
            }

            public int Value
            {
                get
                {
                    return value;
                }

                set
                {
                    this.value = value;
                }
            }


        }
        public ActionResult Order_Detail(int id=1)
        {
            var session = Session[Common.CommonConstants.Customer_SESSION] as Common.CustomerLogin;
            if (session == null) return Redirect("/");
            ViewBag.customer = new CustomerAccountDAO().getCustomerAccount(session.Email);
            ViewBag.order = new OrderDAO().GetOrder(id);
            Order_Detail orderDetailDAO = new Order_Detail();
            var listOrder_Detail = orderDetailDAO.ListAll(id).ToList();
            ViewBag.tong =string.Format("{0:N0} VND", orderDetailDAO.TongTien(id)) ;
            return View(listOrder_Detail);
        }
        // GET: Order
        public ActionResult ManagerOrder(int count = 0)
        {
            var session = Session[CommonConstants.Customer_SESSION] as CustomerLogin;
            if (session == null) return Redirect("/");
            var customer = new CustomerAccountDAO().getCustomerAccount(session.Email);
            
            OrderDAO orderDAO = new OrderDAO();
            var listOrder = orderDAO.ListOrder(count,customer.ID);

            List<SelectListItem> listQuantityOrder = new List<SelectListItem>()
               {
                new SelectListItem()
                {
                    Text="5 Hóa đơn gần nhất",Value=5.ToString()
                    
                    
                },
                 new SelectListItem()
                {
                    Text="15 Hóa đơn gần nhất",Value=15.ToString()
                },
                  new SelectListItem()
                {
                    Text="30 Hóa đơn gần nhất",Value=30.ToString(),Selected=true
                },
                   new SelectListItem()
                {
                    Text="Tất cả hóa đơn",Value=0.ToString()
                }
            };
            
            foreach (var order in listQuantityOrder)
            {
                if (order.Value == count.ToString())
                {
                    order.Selected = true;
                  
                }
                else order.Selected = false;
            }
            ViewBag.list = listQuantityOrder;
            return View(listOrder);

        }
    }
}