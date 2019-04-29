using _Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Models.DAO
{
    public class MenuTypeDAO
    {
        private OnlineShop dt;
        public MenuTypeDAO()
        {
            dt = new OnlineShop();
        }
                public MenuType GetMenuType(int id)
        {
            return dt.MenuTypes.Find(id);
        }
        public int Create(MenuType menuType)
        {
            try
            {
                dt.MenuTypes.Add(menuType);
                dt.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
           
        }
        public IEnumerable<MenuType> SelectAll()
        {
            return dt.MenuTypes.OrderBy(x => x.Name);
        }
        public IEnumerable<MenuType> SelectWithCondition(string searchString)
        {
            return dt.MenuTypes.Where(x => x.Name.Contains(searchString) ||
            x.MenuTypeID.ToString().Contains(searchString)).OrderBy(x => x.Name);
        }
        public void Remove(int id)
        {
            var MenuTypeDeleted = dt.MenuTypes.Find(id);
            dt.MenuTypes.Remove(MenuTypeDeleted);
            dt.SaveChanges();
        }
        public int Edit(MenuType menuType)
        {
            try
            {
                var MenuTypeEdited = dt.MenuTypes.Find(menuType.MenuTypeID);
                MenuTypeEdited.Name = menuType.Name;
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
