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
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.Any, Duration = 60 * 30,
            VaryByParam = "id")]
        public ActionResult ProductDetail(int id)
        {
          var  Product = new ProductDAO().getProduct(id);
            ViewBag.Category= new ProductCategoryDAO().GetProductCategory(id);
            ViewBag.ProductsRelated = new ProductDAO().ProductsRelated(id);
            return View(Product);
        }
        [OutputCache(Duration = 60 * 30)]
        public ActionResult ListProduct(int page = 1,int pagesize=12)
        {
            ProductDAO productDAO = new ProductDAO();
            var listProduct = productDAO.SelectAll().ToPagedList(page, pagesize);
            return View(listProduct);
        }
        
        public ActionResult Category(int id,int pageIndex=1, int pageSize=2)
        {
           
            var model = new ProductDAO().GetProductsRelated(id).ToPagedList(pageIndex,pageSize);
            
            ViewBag.ProductCategory = new ProductCategoryDAO().GetProductCategory(id);
            return View(model);
        }
        //autocomplete keysearch
        [HttpPost]
        public JsonResult AutocompleteKeysearch(string Prefix)
        {
            //Note : you can bind same list from database  
            List<Product> ObjList = new List<Product>();
            ObjList = new OnlineShop().Products.ToList();
            //Searching records from list using LINQ query  
            var CityName = (from N in ObjList
                            where N.ProductName.ToUpper().StartsWith(Prefix.ToUpper())
                            select new { N.ProductName });
            return Json(CityName, JsonRequestBehavior.AllowGet);
        }
        //tìm kiếm
        public ActionResult Timkiem(string keysearch, int page = 1)
        {

            int pagesize = 10;
            ViewBag.Keysearch = keysearch;
            var ds = new OnlineShop().Products.Where(x => x.ProductName.ToUpper().Contains(keysearch.Trim().ToUpper()) || x.ProductID.ToString().Contains(keysearch.Trim())).OrderByDescending(x => x.CreateDate).ToList().ToPagedList(page, pagesize);

            return View(ds);
        }
        //sản phẩm hot
        [ChildActionOnly]
        public ActionResult sanphamhot()
        {
            TempData["listhotproduct"] = new ProductDAO().ListHotProduct(4);
            return View();
        }
      
        public ActionResult sanphambanchay(int page = 1, int pagesize = 12)
        {
           var ds= new ProductDAO().ListHotProduct().ToPagedList(page, pagesize);
            return View(ds);
        }
     
    }
}