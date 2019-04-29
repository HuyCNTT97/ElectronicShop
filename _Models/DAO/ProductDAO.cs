using _Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _Models.DAO
{
    public class ProductDAO
    {
         OnlineShop dt=null;
        public ProductDAO()
        {
            dt = new OnlineShop();
        }
        public List<Product> GetProductsRelated(int ProductCategoryID)
        {
            return dt.Products.Where(x => x.CategoryID == ProductCategoryID).OrderByDescending(x => x.Price).ToList();
        }
        public List<Product> ProductsRelated(int id)
        {
            var proDuct = dt.Products.Find(id);
            return dt.Products.Where(x => x.ProductID != id && x.CategoryID == proDuct.CategoryID).Take(4).ToList();
        }
        public Product getProduct(int id)
        {
            return dt.Products.Find(id);
        }
        public int SearchProductCategory(string searchString)
        {
            var ProductCategoryID = dt.ProductCategories.Where(x => x.CategoryName.ToLower() == searchString.ToLower()).FirstOrDefault().CategoryID;
            return ProductCategoryID;
        }
        public int Create(Product product)
        {
            try
            {
                product.MetaTitle = convertToUnSign3(product.ProductName).Replace(" ", "-").ToLower();

                dt.Products.Add(product);
                dt.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public void HamLinhDong()
        {
            var list = dt.Products.ToList();
            foreach (var item in list)
            {
                var product = dt.Products.Find(item.ProductID);
                product.Description = "Đây là một trong những mấu tốt nhất trên thị trường, chắc chắn sẽ làm bạn hài lòng";
                dt.SaveChanges();
            }
        }
        public List<Product> ListNewProduct(int top)
        {
            return dt.Products.OrderByDescending(x => x.CreateBy).Take(top).ToList();
        }
        public List<Product> ListHotProduct(int top)
        {
            return dt.Products.Where(x=>x.Tophot!=null).OrderByDescending(x => x.CreateBy).Take(top).ToList();
        }
        public List<Product> ListHotProduct()
        {
            return dt.Products.Where(x => x.Tophot != null).OrderByDescending(x => x.CreateBy).ToList();
        }
        public int Edit(Product product)
        {
            try
            {
                var editProduct = dt.Products.Find(product.ProductID);
                editProduct.hinhanh = product.hinhanh;
                editProduct.Description = product.Description;
                editProduct.Detail = product.Detail;
                editProduct.Image = product.Image;
                editProduct.IncludeVAT = product.IncludeVAT;
                editProduct.MetaDescriptions = product.MetaDescriptions;
                editProduct.MetaKeywords = product.MetaKeywords;
                editProduct.MetaTitle = product.MetaTitle;
                editProduct.ModifiedBy = product.ModifiedBy;
                editProduct.ModifiedDate = product.ModifiedDate;
                editProduct.MoreImages = product.MoreImages;
                editProduct.Price = product.Price;
                editProduct.ProductName = product.ProductName;
                editProduct.PromotionPrice = product.PromotionPrice;
                editProduct.Quantity = product.Quantity;
                editProduct.Status = product.Status;
                editProduct.Tophot = product.Tophot;
                editProduct.Viewcount = product.Viewcount;
                editProduct.Warranty = product.Warranty;
                dt.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public static string convertToUnSign3(string s)
        {
            System.Text.RegularExpressions.Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public void ChuyenDoi(List<Product> products)
        {
            foreach (var item in products)
            {
                var product = dt.Products.Find(item.ProductID);
                product.MetaTitle = convertToUnSign3(item.ProductName).Replace(" ","-").ToLower();
                dt.SaveChanges();
            }
        }
        public void ChuyenDoi(Product products)
        {
            var product = dt.Products.Find(products.ProductID);
            product.MetaTitle = convertToUnSign3(product.ProductName).Replace(" ", "-").ToLower();
        }
        public void Remove(int id)
        {
            var productDeleted = dt.Products.Find(id);
            dt.Products.Remove(productDeleted);
            dt.SaveChanges();
        }
        public IEnumerable<Product> SelectAll()
        {
            return dt.Products.OrderBy(x => x.ProductID);
        }
        public IEnumerable<Product> SelectWithCondition(string searchString)
        {
            ProductDAO productDAO = new ProductDAO();
            var CategoryProductID = productDAO.SearchProductCategory(searchString);
            return dt.Products.Where(x => x.ProductName.Contains(searchString) ||
            x.ProductID.ToString()==searchString||x.CategoryID==CategoryProductID).OrderBy(x=>x.ProductID);
        }
        public bool ChangeStatus(int id)
        {
            var product = dt.Products.Find(id);
            product.Status = !product.Status;
            dt.SaveChanges();
            return product.Status;
        }
    }
}
