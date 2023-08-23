using GUIs.Models.EF;
using GUIs.Models.VIEW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GUIs.Models.DAO
{
    public class sanphamDAO
    {
        private dbContext context = new dbContext();
        public void InsertOrUpdate(SANPHAM item)
        {
            if (item.ID == 0)
            {
                context.SANPHAM.Add(item);
            }
            context.SaveChanges();
        }
        public SANPHAM getItem(int id)
        {

            return context.SANPHAM.Where(x => x.ID == id).FirstOrDefault();
        }
        public sanphamVIEW getItemView(int id)
        {

            var query = (from a in context.SANPHAM
                         where a.ID == id
                         select new sanphamVIEW
                         {
                             ID = a.ID,
                             name = a.name,
                             origin = a.origin,
                             img = a.img,
                             color = a.color,
                             quatity = a.price,
                             price = a.price,
                            
                         }).FirstOrDefault();
            return query;
        }
        public List<sanphamVIEW> getListNew()
        {
            var query = (from a in context.SANPHAM orderby a.ID descending
                         select new sanphamVIEW
                         {
                             ID = a.ID,
                             name = a.name,
                             origin = a.origin,
                             img = a.img,
                             color = a.color,
                             quatity = a.price,
                             price = a.price,
                         }).ToList();
            return query;
        }
        public List<sanphamVIEW> Search(String name, out int total, int index = 1, int size = 10)
        {
            var query = (from a in context.SANPHAM
                         where (a.name.Contains(name) )
                         select new sanphamVIEW
                         {
                             ID = a.ID,
                             name = a.name,
                             origin = a.origin,
                             img = a.img,
                             color = a.color,
                             quatity = a.quatity,
                             price = a.price,
                           
                         }).ToList();
            total = query.Count();
            var result = query.Skip((index - 1) * size).Take(size).ToList();
            return result;
        }
        public void Detele(int id)
        {
            SANPHAM x = getItem(id);
            context.SANPHAM.Remove(x);
            context.SaveChanges();
        }
    }
}