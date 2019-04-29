using _Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace _Models.DAO
{
    public class MenuDAO
    {
        private OnlineShop dt;
        public MenuDAO()
        {
            dt = new OnlineShop();
        }
        
        public Menu GetMenu(int id)
        {
            return dt.Menus.Find(id);
        }
        public List<Menu> ListMenu(int MenuTypeID)
        {
            return dt.Menus.Where(x => x.MenuTypeID == MenuTypeID).ToList();
        }
        public IEnumerable<Menu> SelectAll()
        {
            return dt.Menus.OrderBy(x => x.MenuID);
        }
        public IEnumerable<Menu> SelectWithCondition(string searchString)
        {
            return dt.Menus.Where(x => x.MenuID.ToString().Contains(searchString) ||
            x.Text.Contains(searchString) || x.Link.Contains(searchString)).OrderBy(x=>x.MenuID);
        }
        public int Create(Menu menu)
        {
            try
            {
                dt.Menus.Add(menu);
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
            var menudeleted = dt.Menus.Find(id);
            dt.Menus.Remove(menudeleted);
            dt.SaveChanges();
        }
        public int Edit(Menu menu)
        {
            try
            {
                var menuEdited = dt.Menus.Find(menu.MenuID);
                menuEdited.Link = menu.Link;
                menuEdited.DisplayOrder = menu.DisplayOrder;
                menuEdited.MenuTypeID = menu.MenuTypeID;
                menuEdited.Status = menu.Status;
                menuEdited.Target = menu.Target;
                menuEdited.Text = menu.Text;
                dt.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public bool ChangeStatus(int id)
        {
            var menu = dt.Menus.Find(id);
            menu.Status = !menu.Status;
            dt.SaveChanges();
            return menu.Status;
        }
    }

}
