using _Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace _Models.DAO
{
    public class UserDAO
    {
        OnlineShop dt = null;

         

        public UserDAO()
        {
            dt = new OnlineShop();
        }
        public long Insert(UserAccount entity) {
            try
            {
                dt.UserAccounts.Add(entity);
                dt.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public int GetPermissionID(string UserAccount)
        {
            return dt.UserAccounts.SingleOrDefault(x => x.UserAccount1==UserAccount).PermissionID;
        }
        public UserAccount GetID(string UserName)
        {
            return dt.UserAccounts.SingleOrDefault(x => x.UserAccount1 == UserName);
        }
        public UserAccount UserDetail(int id)
        {
            var user = dt.UserAccounts.SingleOrDefault(x => x.UserID == id);
            return user;
        }
        public bool ChangeStatus(int id)
        {
            var user = dt.UserAccounts.SingleOrDefault(x => x.UserID == id);
            user.Status = !user.Status;
            dt.SaveChanges();
            return user.Status;
        }
        public bool Update(UserAccount entity)
        {
         
                UserAccount NguoiDungCanThayDoi = dt.UserAccounts.SingleOrDefault(x => x.UserID == entity.UserID);
            NguoiDungCanThayDoi.PassWord = entity.PassWord;
                NguoiDungCanThayDoi.Name = entity.Name;
                NguoiDungCanThayDoi.Phone = entity.Phone;
                NguoiDungCanThayDoi.Address = entity.Address;
                NguoiDungCanThayDoi.ModifiedBy = entity.ModifiedBy;
                NguoiDungCanThayDoi.ModifiedDate = DateTime.Now;
            NguoiDungCanThayDoi.PermissionID = entity.PermissionID;
                dt.SaveChanges();
                return true;
          
          
        }
        public void RemoveAccount(UserAccount entity)
        {
            var user = dt.UserAccounts.SingleOrDefault(x => x.UserID == entity.UserID);

            dt.UserAccounts.Remove(user);
            dt.SaveChanges();
        }
        public IEnumerable<UserAccount> ListAll(string userAccountSearchString="")
        {
            IQueryable<UserAccount> model = dt.UserAccounts;
            if (!string.IsNullOrEmpty(userAccountSearchString))
            {
                model = model.Where(x => x.UserAccount1.Contains(userAccountSearchString)||x.Name.Contains(userAccountSearchString));
            }
            return model.OrderByDescending(x=>x.UserAccount1);
        }
        public int Login(string userName,string passWord)
        {
            var result = dt.UserAccounts.SingleOrDefault(x => x.UserAccount1 == userName);
            if (result == null)
            {
                return 0;
            }
            else {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.PassWord == passWord)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            };
        }
    }
}
