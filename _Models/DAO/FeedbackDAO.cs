using _Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Models.DAO
{
    public class FeedbackDAO
    {
        private OnlineShop dt;
        public FeedbackDAO()
        {
            dt = new OnlineShop();
        }
        public void Remove(int id)
        {
            var feedbackdeleted = dt.Feedbacks.Find(id);
            dt.Feedbacks.Remove(feedbackdeleted);
            dt.SaveChanges();
        }
        public Feedback GetFeedback(int id)
        {
            return dt.Feedbacks.Find(id);
        }
        public bool ChangeStatus(int id)
        {
            var feedback = dt.Feedbacks.Find(id);
            feedback.Status = !feedback.Status;
            dt.SaveChanges();
            return feedback.Status;
        }
        public int Create(Feedback feedback)
        {
            try
            {
                dt.Feedbacks.Add(feedback);
                dt.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
            
        }
        public IEnumerable<Feedback> SelectAll()
        {
            return dt.Feedbacks.OrderBy(x => x.FeedbackName);
        }
        public IEnumerable<Feedback> SelectCondition(string searchString)
        {
            return dt.Feedbacks.Where(x => x.FeedbackName.Contains(searchString)
            || x.FeedbackID.ToString().Contains(searchString)).OrderBy(x=>x.FeedbackName);
        }
        public int Edit(Feedback feedback)
        {
            try
            {
                var feedbackeditted = dt.Feedbacks.Find(feedback.FeedbackID);
                feedbackeditted.Address = feedback.Address;
                feedbackeditted.Content = feedback.Content;
                feedbackeditted.Email = feedback.Email;
                feedbackeditted.FeedbackName = feedback.FeedbackName;
                feedbackeditted.Phone = feedback.Phone;
                feedbackeditted.Status = feedback.Status;
                dt.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
