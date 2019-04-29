using _Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Models.DAO
{
    public class Order_Detail
    {
        OnlineShop dt;
        public Order_Detail()
        {
            dt = new OnlineShop();
        }
        public IEnumerable<OrderDetail> ListAll(int orderDetailID)
        {
            return dt.OrderDetails.Where(x => x.OrderID == orderDetailID).OrderBy(x => x.OrderID);
        }
        public bool ChangeStatusProcess(int orderID,int productID)
        {
            var orderDetail = dt.OrderDetails.SingleOrDefault(x => x.OrderID == orderID && x.ProductID == productID);
            orderDetail.Trangthaixuli = !orderDetail.Trangthaixuli;
            dt.SaveChanges();

            return orderDetail.Trangthaixuli;
        }
        public bool ChangeStatusShipped(int orderID, int productID)
        {
            var orderDetail = dt.OrderDetails.SingleOrDefault(x => x.OrderID == orderID && x.ProductID == productID);
            orderDetail.Trangthaigiao = !orderDetail.Trangthaigiao;
            dt.SaveChanges();

            return orderDetail.Trangthaigiao;
        }
        public bool ChangeStatusPaymented(int orderID, int productID)
        {
            var orderDetail = dt.OrderDetails.SingleOrDefault(x => x.OrderID == orderID && x.ProductID == productID);
            orderDetail.Trangthaithanhtoan = !orderDetail.Trangthaithanhtoan;
            dt.SaveChanges();
            return orderDetail.Trangthaithanhtoan;
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
