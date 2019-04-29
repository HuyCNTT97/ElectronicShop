using _Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Models.DAO
{
    public class ContactDAO
    {
        private OnlineShop dt;
        public ContactDAO()
        {
            dt = new OnlineShop();
        }
        public bool InsertFeedback(Feedback feedback)
        {
            try
            {
                dt.Feedbacks.Add(feedback);
                dt.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ChangeStatus(int id)
        {
            var contact = dt.Contacts.Find(id);
            contact.Status = !contact.Status;
            dt.SaveChanges();
            return contact.Status;

        }
        public int Edit(Contact contact)
        {
            try
            {
                var contacted = dt.Contacts.Find(contact.ContactID);
                contacted.Content = contact.Content;
                contacted.Status = contact.Status;
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
            var contact = dt.Contacts.Find(id);
            dt.Contacts.Remove(contact);
            dt.SaveChanges();
        }
        public IEnumerable<Contact> ListAll()
        {
            return dt.Contacts.OrderBy(x => x.ContactID);
        }
        public Contact GetContact(int id)
        {
            var contact = dt.Contacts.Find(id);
            return contact;
        }
        public int Create(Contact contact)
        {
           
                dt.Contacts.Add(contact);
                dt.SaveChanges();
                return 1;
            
           
        }
    }
}
