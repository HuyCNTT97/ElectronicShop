using ShopOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class BaseController:Controller
    {
        private static UserLogin nguoiDungHienTai;

        public static UserLogin NguoiDungHienTai
        {
            get
            {
                return nguoiDungHienTai;
            }

            set
            {
                nguoiDungHienTai = value;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
             NguoiDungHienTai = (UserLogin)Session[CommonConstants.User_SESSION];
            if (NguoiDungHienTai == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                    new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}