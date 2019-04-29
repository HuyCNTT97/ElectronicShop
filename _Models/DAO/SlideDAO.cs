using _Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Models.DAO
{
    public class SlideDAO
    {
        private OnlineShop dt;
        public SlideDAO()
        {
            dt = new OnlineShop();
        }
        public Slide GetSlide(int id)
        {
            return dt.Slides.Find(id);
        }
        public IEnumerable<Slide> SelectAll()
        {
            return dt.Slides.OrderByDescending(x => x.CreateDate).Where(x=>x.Status==true);
        }
        public IEnumerable<Slide> SelectWithConditon(string searchString)
        {
            return dt.Slides.Where(x => x.Description.Contains(searchString)).OrderByDescending(x => x.CreateDate);
        }
        public int Create(Slide slide)
        {
            try
            {
                slide.CreateDate = DateTime.Now;
                dt.Slides.Add(slide);
                dt.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public int Edit(Slide slide)
        {
            try
            {
                var SlideEdit = dt.Slides.Find(slide.SlideID);
                SlideEdit.Description = slide.Description;
                SlideEdit.DisplayOrder = slide.DisplayOrder;
                SlideEdit.Image = slide.Image;
                SlideEdit.Link = slide.Link;
                SlideEdit.ModifiedBy = slide.ModifiedBy;
                SlideEdit.ModifiedDate = DateTime.Now.ToString();
                SlideEdit.Status = slide.Status;
                dt.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }

        }
        public void Remove(int id)
        {
            var slide = dt.Slides.Find(id);
            dt.Slides.Remove(slide);
            dt.SaveChanges();
        }
        public bool ChangeStatus(int id)
        {
            var slide = dt.Slides.Find(id);
            slide.Status = !slide.Status;
            dt.SaveChanges();
            return slide.Status;
        }
    }
}
