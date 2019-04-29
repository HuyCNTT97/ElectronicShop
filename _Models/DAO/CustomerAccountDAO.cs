using _Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Models.DAO
{
    public class CustomerAccountDAO
    {
        OnlineShop dt = null;
        public CustomerAccountDAO()
        {
            dt = new OnlineShop();
        }
        public int Edit(CustomerAccount customer)
        {
            var customerAccout = dt.CustomerAccounts.SingleOrDefault(x => x.Email == customer.Email);
            if (customerAccout == null) return 0;
            customerAccout.PassWord = customer.PassWord;
            customerAccout.ConfirmPassWord = customer.ConfirmPassWord;
            dt.SaveChanges();
            return 1;
        }
        public int CheckPass(CustomerAccount customerAccount)
        {
            var customer = dt.CustomerAccounts.SingleOrDefault(x => x.Email == customerAccount.Email);
            if (customerAccount.PassWord == customer.PassWord) return 1;
            else return 0;
        }
        public int CheckLogin(string email,string password)
        {
            var list = dt.CustomerAccounts.Where(x => x.Email.ToUpper() == email.ToUpper()).ToList();
            if (list.Count == 0) return 0;
            { list= list.Where(x => x.PassWord == password).ToList(); }
            if (list.Count==0) return -1;
            return list.Count();
        }
        public int CreateAccount(CustomerAccount customerAccount)
        {
            try
            {
                dt.CustomerAccounts.Add(customerAccount);
                dt.SaveChanges();
                return 1;
            }

            catch
            {
                return 0;
            }
        }
        public int CreateFacebook(CustomerAccount customerAccount)
        {
            var customer = dt.CustomerAccounts.SingleOrDefault(x => x.Email == customerAccount.Email);
            if (customer == null)
            {
                dt.CustomerAccounts.Add(customerAccount);
                dt.SaveChanges();
                return customerAccount.ID;
            }
            else
            {
                return customer.ID;
            }
        }
        public CustomerAccount getCustomerAccount(string email)
        {
            return dt.CustomerAccounts.Where(x => x.Email == email).Single();
        }
    }
}
