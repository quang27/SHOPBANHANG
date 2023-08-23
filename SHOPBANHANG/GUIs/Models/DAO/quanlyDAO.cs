using GUIs.Models.EF;
using GUIs.Models.VIEW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GUIs.Models.DAO
{
    public class quanlyDAO
    {
        private dbContext context = new dbContext();
        public void InsertOrUpdate(QUANLY item)
        {
            if (item.ID == 0)
            {
                context.QUANLY.Add(item);
            }
            context.SaveChanges();
        }
        public QUANLY getItem(int id)
        {

            return context.QUANLY.Where(x => x.ID == id).FirstOrDefault();
        }
        public quanlyVIEW getItemView(int id)
        {

            var query = (from a in context.QUANLY
                         where a.ID == id
                         select new quanlyVIEW
                         {
                             ID = a.ID,
                             username = a.username,
                             password=a.password,
                             
                         }).FirstOrDefault();
            return query;
        }
        public void Detele(int id)
        {
            QUANLY x = getItem(id);
            context.QUANLY.Remove(x);
            context.SaveChanges();
        }
    }
}