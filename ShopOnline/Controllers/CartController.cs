using _Models.DAO;
using _Models.EF;
using Antlr.Runtime;
using Common;
using ShopOnline.Common;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Net;
using System.Net.Mail;
namespace ShopOnline.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private const string CartSession = "CartSession";

        

        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list =(List<CartItem>) cart;
            }
            return View(list);
        }
        [ChildActionOnly]
        public ActionResult HeaderCart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);  
        }
        [HttpPost]
        public ActionResult Payment(string txtShipName,string txtPhone,string txtAdress,string txtEmail,string lblTong)
        {
            var order = new Order();
            order.CreateDay = DateTime.Now;
            order.ShipAdress = txtAdress;
            order.ShipName = txtShipName;
            order.ShipPhone = int.Parse(txtPhone);
            order.ShipEmail = txtEmail;
            var session = Session[Common.CommonConstants.Customer_SESSION] as CustomerLogin;
            order.CustomerID = new CustomerAccountDAO().getCustomerAccount(session.Email).ID;
            var orderDAO = new OrderDAO();
            var id = orderDAO.AddOrder(order);
            if (id > 0)
            {
                var cart = (List<CartItem>)Session[CartSession];
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                foreach (var item in cart.ToList())
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderID = id;
                    orderDetail.ProductID = item.Product.ProductID;
                    
                    if(item.Product.PromotionPrice!=0&& item.Product.PromotionPrice != null)
                    {
                        
                        orderDetail.PriceWhenBuyAtTime =item.Product.Price - item.Product.Price * double.Parse(item.Product.PromotionPrice.ToString()) / 100;
                    }
                    else
                    {
                        orderDetail.PriceWhenBuyAtTime = item.Product.Price;
                    }
                    orderDetail.Quantity = item.Quantity;
                    orderDetails.Add(orderDetail);   
                }
                orderDAO.CreateOrderDetail(orderDetails);
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Asset/Client/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", txtShipName);
                content = content.Replace("{{Phone}}", txtPhone);
                content = content.Replace("{{Email}}", txtEmail);
                content = content.Replace("{{Address}}", txtAdress);
                content = content.Replace("{{Total}}", string.Format("{0:#,###,### VND}",double.Parse(lblTong)));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                new MailHelper().SendMail(txtEmail, "Đơn hàng mới từ OnlineShop", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
            }   
            else
            {
                TempData["msg"] = MessageBox.Show("Xác Nhận Đơn Thất Bại");
                return Redirect("thanh-toan-gio-hang");
            }
            return Redirect("/hoan-thanh");
        }
        public ActionResult Complete()
        {
            //TempData["msg"] = MessageBox.Show("Đặt Thành Công");
            //return View();
            Session[CartSession] = null;
            TempData["thanhcong"] = "<script>alert('mua hàng thành công, mời kiểm tra gmail!');</script>";
          
            return RedirectToAction("", "Home");
        }
        public JsonResult RemoveCart(int productID)
        {
            var cart = (List<CartItem>)Session[CartSession];
          
            foreach (var item in cart.ToList())
            {
                if (item.Product.ProductID == productID) { cart.Remove(item);
                }
            }
            Session[CartSession] = cart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult UpdateQuantity(string CartModel)
        {
            var jscart = new JavaScriptSerializer().Deserialize<List<CartItem>>(CartModel);
            var sessioncart = (List<CartItem>)Session[CartSession];
            foreach (var item in sessioncart)
            {
                var jsItem = jscart.SingleOrDefault(x => x.Product.ProductID == item.Product.ProductID);
                if (jsItem != null)
                {
                    item.Quantity = jsItem.Quantity;
                }
            }
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddItem(int productID,int quantity)
        {
            var proDuct = new ProductDAO().getProduct(productID);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                bool tontai = false;
                foreach (var item in list)
                {
                    if (item.Product.ProductID == productID) {
                        item.Quantity = item.Quantity + quantity;
                        tontai = true;
                    }
                    
                }
                if (tontai == false)
                {
                    var item = new CartItem();
                    item.Product = proDuct;
                    item.Quantity = quantity;
                    list.Add(item);
                }
            }
            else
            {
                var item = new CartItem();
                item.Product = proDuct;
                item.Quantity = quantity;
                var list= new List<CartItem>();
                list.Add(item);
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
    }
}

