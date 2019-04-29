using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Common
{
    public class MessageBox
    {
        public MessageBox() { }
        public static string Show(string Message)
        {
            string chuoi = "<script>alert(\"" + Message + "\")</script>";
            return chuoi;
        }
    }
}