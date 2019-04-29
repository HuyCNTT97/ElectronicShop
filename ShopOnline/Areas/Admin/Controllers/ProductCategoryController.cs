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
    public class CategoryItemProduct
    {
        private string categoryName;
        private int id;

       
public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string CategoryName
        {
            get
            {
                return categoryName;
            }

            set
            {
                categoryName = value;
            }
        }

        public CategoryItemProduct(ProductCategory productCategories)
        {
            CategoryName = productCategories.CategoryName;
            Id = productCategories.CategoryID;
        }
        public CategoryItemProduct() { }
        public CategoryItemProduct(string name,int tempid=-1)
        {
            CategoryName = name;
            Id = tempid;
        }
    }
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string searchString="",int page=1,int pagesize=2)
        {
            ViewBag.searchString = searchString;
            if (searchString == "")
            {
                ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
                var listProductCategory = productCategoryDAO.SelectAll().ToPagedList(page,pagesize);
                return View(listProductCategory);
            }
            else
            {
                ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
                var listProductCategory = productCategoryDAO.SelectWithCondition(searchString).ToPagedList(page,pagesize);
                return View(listProductCategory);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetCategoryParent();
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory,string tempstatus)
        {
            if (ModelState.IsValid)
            {
                ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
                productCategory.CreateBy = BaseController.NguoiDungHienTai.UserAccount;
                productCategory.CreateDate = DateTime.Now;
                if (tempstatus == "Kích Hoạt")
                {
                    productCategory.Status = true;
                }
                else
                {
                    productCategory.Status=false;
                }
                var check = productCategoryDAO.Create(productCategory);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Create successfully");
                    return RedirectToAction("Create", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Create fail");
                }
            }
            SetCategoryParent();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            SetCategoryParent();
            ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
            var productcategory = productCategoryDAO.GetProductCategory(id);
            return View(productcategory);
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory,string tempstatus)
        {
            if (ModelState.IsValid)
            {
                if (tempstatus == "Kích Hoạt")
                {
                    productCategory.Status = true;
                }
                else productCategory.Status = false;
                productCategory.ModifiedDate = DateTime.Now.ToString();
                ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
                var check = productCategoryDAO.Edit(productCategory);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Edit thành công");
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Edit không thành công");
                }
            }
            SetCategoryParent();
            return View("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(int productCategoryID)
        {
            ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
            var tempstatus=productCategoryDAO.ChangeStatus(productCategoryID);
            return Json(new
            {
                status = tempstatus
            });
        }
        [HttpDelete]
        public ActionResult Remove(int productCategoryID)
        {
            ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
            productCategoryDAO.Remove(productCategoryID);
            return RedirectToAction("Index");
        }
        public void SetCategoryParent(int itemselectedID=1)
        {
            ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
            var list = productCategoryDAO.SelectAll();
            List<CategoryItemProduct> listCategory = new List<CategoryItemProduct>();
            listCategory.Add(new CategoryItemProduct("Không Có", -1));
            if (list.Count() == 0||list==null)
            {
                ViewBag.Parent = new SelectList(listCategory, "id", "CategoryName", itemselectedID);
            }
            else
            {
                foreach(var item in list)
                {
                    listCategory.Add(new CategoryItemProduct(item));
                }
                ViewBag.Parent = new SelectList(listCategory, "ID", "CategoryName", itemselectedID);
            }
        }
    }
}