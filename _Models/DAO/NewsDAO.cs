using _Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _Models.DAO
{
    public class NewsDAO
    {
        public static string msgerror = "";
         OnlineShop dt = null;
        public NewsDAO()
        {
            dt = new OnlineShop();
        }
        public IEnumerable<Content> getContens(int NewsID)
        {
            if (NewsID == 0) return dt.Contents.OrderByDescending(x => x.CreateDate);
            return dt.Contents.Where(x => x.NewsID == NewsID).OrderByDescending(x => x.CreateDate);
        }
        public bool ChangeStatus(int newsid)
        {
            var news = dt.News.Find(newsid);
            news.Status = !news.Status;
            dt.SaveChanges();
            return news.Status;
        }
        public int Edit(About about)
        {
            try
            {
                var aboutEditted = dt.Abouts.Find(about.AboutID);
                aboutEditted.AboutName = about.AboutName;
                aboutEditted.Description = about.Description;
                aboutEditted.Detail = about.Detail;
                aboutEditted.MetaDescriptions = about.MetaDescriptions;
                aboutEditted.MetaKeywords = about.MetaKeywords;
                aboutEditted.MetaTitle = about.MetaTitle;
                aboutEditted.ModifiedBy = about.ModifiedBy;
                aboutEditted.ModifiedDate = about.ModifiedDate;
                about.Status = about.Status;
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
        public void ChuyenDoi(List<News> listnews)
        {
            foreach (var item in listnews)
            {
                var news = dt.News.Find(item.NewsID);
                news.MetaTitle = convertToUnSign3(item.NewsName).Replace(" ", "-").ToLower();
                dt.SaveChanges();
            }
        }
        public News getNews(int id)
        {
            if (id == 0)
            {
                News news = new News();
                news.NewsName = "Tất Cả Tin Tức";
                return news;
            }
            return dt.News.Find(id);
        }
        public int EditNews(News newsEdit)
        {
            var editted = dt.News.Find(newsEdit.NewsID);
            if (editted != null)
            {
                editted.NewsName = newsEdit.NewsName;
                editted.DisplayOrder = newsEdit.DisplayOrder;
                editted.MetaDescriptions = newsEdit.MetaDescriptions;
                editted.MetaKeywords = newsEdit.MetaKeywords;
                editted.MetaTitle = newsEdit.MetaTitle;
                editted.ParentNewsID = newsEdit.ParentNewsID;
                editted.SeoTittle = newsEdit.SeoTittle;
                editted.ShowOnHome = newsEdit.ShowOnHome;
                editted.Status = newsEdit.Status;
                editted.ModifiedDate = DateTime.Now.ToString();
                editted.ModifiedBy = newsEdit.ModifiedBy;
                dt.SaveChanges();
                return 1;
            }
            else { return 0; }
        }
        public string Xoa(int IDNewsCanXoa)
        {
            var NewsDeleting = dt.News.Find(IDNewsCanXoa);
            string NewsNameSelected = NewsDeleting.NewsName;
            dt.News.Remove(NewsDeleting);
            dt.SaveChanges();
            return NewsNameSelected;
        }
        public int AddNews(News news)
        {
            try
            {
                news.MetaTitle = convertToUnSign3(news.NewsName).Replace(" ", "-").ToLower();

                dt.News.Add(news);
                dt.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                msgerror = e.Message;
                return 0;
            }
        }
        public List<News> ListAll()
        {
            var list = dt.News.ToList();
            return dt.News.ToList();
        }
        public string TenNews(int? ParentNewsID)
        {
            if (ParentNewsID == null||dt.News.SingleOrDefault(x=>x.NewsID==ParentNewsID)==null) return "Không nằm trong loại nào cả";
            var ParentNews = dt.News.Find(ParentNewsID);
            return ParentNews.NewsName;
        }

    }
}
