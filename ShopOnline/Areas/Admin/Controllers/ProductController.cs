using _Models.DAO;
using _Models.EF;
using PagedList;
using ShopOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString="",int page=1,int pagesize=2)

        {
            ViewBag.searchString = searchString;
            ProductDAO productDAO = new ProductDAO();
           

            if (searchString == "")
            {
                var listProduct = productDAO.SelectAll().ToPagedList(page, pagesize);
                return View(listProduct);
            }
            else
            {
                var listProduct = productDAO.SelectWithCondition(searchString).ToPagedList(page, pagesize);
                return View(listProduct);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            GetProductCategoryName();
            return View();
        }
        [HttpPost,ValidateInput(false)]
        public ActionResult Create(Product product,string tempstatus)
        {
            if (ModelState.IsValid)
            {
                if (tempstatus == "Kích Hoạt")
                {
                    product.Status = true;
                }
                else product.Status = false;
                product.CreateDate = DateTime.Now;
                product.CreateBy = BaseController.NguoiDungHienTai.UserAccount;
                ProductDAO productDAO = new ProductDAO();
                
                var check = productDAO.Create(product);
                productDAO.ChuyenDoi(product);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Create successfully");
                    return RedirectToAction("Create", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Create fail");
                }
            }
            GetProductCategoryName();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id,int page)
        {
            ViewBag.priorlypage = page;
            GetProductCategoryName();
            ProductDAO productDAO = new ProductDAO();
            var product = productDAO.getProduct(id);
            return View(product);
        }

   
        [HttpPost,ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product,string tempstatus,string priorlypage)
        {
            ProductDAO productDAO = new ProductDAO();
            
            if (ModelState.IsValid)
            {
                
                if (tempstatus == "Kích Hoạt")
                {
                    product.Status = true;
                }
                else product.Status = false;
                product.ModifiedBy = BaseController.NguoiDungHienTai.UserAccount;
               
                var check = productDAO.Edit(product);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Edit successfully");
                    return RedirectToAction("Index", "Product",new {@page= int.Parse(priorlypage) });
                }
                else
                {
                    ModelState.AddModelError("", "Edit fail");
                }
            }
            GetProductCategoryName();
            return View(product.ProductID);
        }
        public void GetProductCategoryName(int idselected=1)
        {
            ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
            var listCategory = productCategoryDAO.SelectAll();
            ViewBag.listCategory = new SelectList(listCategory, "CategoryID", "CategoryName", idselected);
        }
        [HttpDelete]
        public ActionResult Remove(int ProductID)
        {
            ProductDAO productDAO = new ProductDAO();
            productDAO.Remove(ProductID);
            return RedirectToAction("Index", "Product");
        }
        [HttpPost]
        public JsonResult ChangeStatus(int productID)
        {
            ProductDAO productDAO = new ProductDAO();
            var tempstatus = productDAO.ChangeStatus(productID);
            return Json(new
            {
                status = tempstatus
            });
        }
    }
}