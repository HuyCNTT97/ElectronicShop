using _Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Models.DAO
{
   public class PermissionDAO
    {
        private OnlineShop dt = new OnlineShop();
        public PermissionDAO()
        {

        }
        public Permission GetPermission(int id)
        {
            return dt.Permissions.Find(id);
        }
        public IEnumerable<Permission> ListAll()
        {
            return dt.Permissions.OrderBy(x=>x.PermissionName);
        }
    }
}
