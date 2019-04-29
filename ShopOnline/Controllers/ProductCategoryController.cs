using _Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: ProductCategory
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult ListProductCategory()
        {
            ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
            var listProduct = productCategoryDAO.SelectAll().ToList();
            return PartialView(listProduct);
        }
    }
}