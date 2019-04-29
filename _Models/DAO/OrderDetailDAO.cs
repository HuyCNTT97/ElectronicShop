using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _Models.EF;
namespace ShopOnline.Areas.Admin.Controllers
{
    public class OrderDetailDAO
    {
        OnlineShop dt;
        public OrderDetailDAO()
        {
            dt = new OnlineShop();
        }
        public IEnumerable<OrderDetail> ListAll(int orderDetailID)
        {
            return dt.OrderDetails.Where(x => x.OrderID == orderDetailID).OrderBy(x => x.OrderID);
        }
        public double TongTien(int orderDetailID)
        {
            var list = dt.OrderDetails.Where(x => x.OrderID == orderDetailID).OrderBy(x => x.OrderID).ToList();
            double tong = 0;
            foreach (var item in list)
            {
                tong = tong + item.PriceWhenBuyAtTime;
            }
            return tong;
        }
    }
}