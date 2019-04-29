using _Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Models.DAO
{
    public class AboutDAO
    {
        private OnlineShop dt;
        public AboutDAO()
        {
            dt = new OnlineShop();
        }
        public bool ChangeStatus(int id)
        {
            var aboutchange = dt.Abouts.Find(id);
            aboutchange.Status = !aboutchange.Status;
            dt.SaveChanges();
            return aboutchange.Status;
        }
        public IEnumerable<About> SelectWithCondition(string condition)
        {
            var list = dt.Abouts.Where(x => x.AboutName.Contains(condition) || x.AboutID.ToString().Contains(condition)).OrderBy(x => x.AboutName);
            return list;
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
        public About GetAbout(int id)
        {
            var About = dt.Abouts.Find(id);
            return About;
        }
        public void Remove(int id)
        {
            dt.Abouts.Remove(dt.Abouts.Find(id));
            dt.SaveChanges();
        }
        public int CreateAbout(About about)
        {
            try
            {
                about.CreateDate = DateTime.Now;
                dt.Abouts.Add(about);
                dt.SaveChanges();
                return 1;
            }
            catch 
            {
                return 0;
            }
         
        }
        public IEnumerable<About> SelectAll()
        {
            var listAllAbout = dt.Abouts.OrderBy(x => x.AboutName);
            return listAllAbout;
        }
    }
}
