using _Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Models.DAO
{
    public class TagDAO
    {
        private OnlineShop dt;
        public TagDAO()
        {
            dt = new OnlineShop();
        }
        public Tag GetTag(int id)
        {
            return dt.Tags.Find(id);
        }
        public IEnumerable<Tag> SelectAll()
        {
            return dt.Tags.OrderBy(x => x.NameTag);
        }
        public IEnumerable<Tag> SelectWithCondition(string searchString)
        {
            return dt.Tags.Where(x => x.NameTag.Contains(searchString)).OrderBy(x => x.NameTag);
        }
        public int Create(Tag tag)
        {
            try
            {
                dt.Tags.Add(tag);
                dt.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public int Edit(Tag tag)
        {
            try
            {
                var TagEditting = dt.Tags.Find(tag.TagID);
                TagEditting.NameTag = tag.NameTag;
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
            var TagRemoved = dt.Tags.Find(id);
            dt.Tags.Remove(TagRemoved);
            dt.SaveChanges();
        }

    }
}
