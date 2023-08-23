using GUIs.Models.EF;
using GUIs.Models.VIEW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GUIs.Models.DAO
{
    public class chitiethoadonDAO
    {
        private dbContext context = new dbContext();
        public void InsertOrUpdate(CHITIETHOADON item)
        {
            if (item.ID == 0)
            {
                context.CHITIETHOADON.Add(item);
            }
            context.SaveChanges();
        }
        public CHITIETHOADON getItem(int id)
        {

            return context.CHITIETHOADON.Where(x => x.ID == id).FirstOrDefault();
        }
        public chitiethoadonVIEW getItemView(int id)
        {

            var query = (from a in context.CHITIETHOADON
                         where a.ID == id
                         select new chitiethoadonVIEW
                         {
                             ID = a.ID,
                             idsp = a.idsp,
                             idhd = a.idhd,
                             price = a.price,
                             quatity = a.quatity,                           
                         }).FirstOrDefault();
            return query;
        }
        public List<chitiethoadonVIEW> getList()
        {
            var query = (from a in context.CHITIETHOADON
                         select new chitiethoadonVIEW
                         {
                             ID = a.ID,
                             idsp = a.idsp,
                             idhd = a.idhd,
                             price = a.price,
                             quatity = a.quatity,
                         }).ToList();
            return query;
        }
        public List<chitiethoadonVIEW> Search(int idhd)
        {
            var query = (from a in context.CHITIETHOADON
                         where (a.idhd==idhd )
                         select new chitiethoadonVIEW
                         {
                             ID = a.ID,
                             idsp = a.idsp,
                             idhd = a.idhd,
                             price = a.price,
                             quatity = a.quatity,
                         }).ToList();
            
           
            return query;
        }
        public void Detele(int id)
        {
            CHITIETHOADON x = getItem(id);
            context.CHITIETHOADON.Remove(x);
            context.SaveChanges();
        }
    }
}