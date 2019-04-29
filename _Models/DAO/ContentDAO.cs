using _Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace _Models.DAO
{
    public class ContentDAO
    {
        private OnlineShop dt;
        public ContentDAO()
        {
            dt = new OnlineShop();
        }
        public Content GetContent(int id)
        {
            return dt.Contents.Find(id);
        }
        public static string convertToUnSign3(string s)
        {
            System.Text.RegularExpressions.Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public void ChuyenDoi(List<Content> contents)
        {
            foreach (var item in contents)
            {
                var content = dt.Contents.Find(item.ContentID);
                content.MetaTitle = convertToUnSign3(item.ContentName).Replace(" ", "-").ToLower();
                dt.SaveChanges();
            }
        }
        public void Remove(int Id)
        {
            var Content = dt.Contents.Find(Id);
            dt.Contents.Remove(Content);
            dt.SaveChanges();
        }
        public bool ChangeStatus(int id)
        {
            var content = dt.Contents.Find(id);
            content.Status = !content.Status;
            dt.SaveChanges();
            return content.Status;
        }
        public int Sua(Content contentEdit)
        {
            
                var contentEditted = dt.Contents.Find(contentEdit.ContentID);
            contentEditted.ContentName = contentEdit.ContentName;
            contentEditted.Description = contentEdit.Description;
            contentEditted.Detail = contentEdit.Detail;
            contentEditted.Image = contentEdit.Image;
            contentEditted.MetaDescriptions = contentEdit.MetaDescriptions;
            contentEditted.MetaKeywords = contentEdit.MetaKeywords;
            contentEditted.MetaTitle = contentEdit.MetaTitle;
            contentEditted.ModifiedDate = DateTime.Now.ToString() ;
            contentEditted.NewsID = contentEdit.NewsID;
            contentEditted.Status = contentEdit.Status;
            contentEditted.Tags = contentEdit.Tags;
            contentEditted.Tophot = contentEdit.Tophot;
            contentEditted.Viewcount = contentEdit.Viewcount;
            contentEditted.Warranty = contentEdit.Warranty;
                dt.SaveChanges();
                return 1;
           
        }
        public int ThemContent(Content content)
        {
                content.MetaTitle = convertToUnSign3(content.ContentName).Replace(" ", "-").ToLower();
            dt.Contents.Add(content);
            dt.SaveChanges();
            return content.ContentID;
        }
        public Content TimContent(int id)
        {
            var ContentCanTim = dt.Contents.Find(id);
            return ContentCanTim;
        }
        public IEnumerable<Content> listAll()
        {
            var DanhSach = dt.Contents.OrderBy(x=>x.ContentID);
            return DanhSach;

        }
        public IEnumerable<Content> ListAllWithCondition(string SearchContent)
        {
            var DanhSach = dt.Contents.Where(x => x.Status == true&&x.ContentName.Contains(SearchContent)||
             x.Status == true && x.Description.Contains(SearchContent)).OrderBy(x => x.ContentID);
            return DanhSach;

        }
    }
}
