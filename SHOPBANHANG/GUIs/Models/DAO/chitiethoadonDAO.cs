using GUIs.Helper;
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
                         join b in context.SANPHAM on a.idsp equals b.ID
                         where a.ID == id
                         select new chitiethoadonVIEW
                         {
                             ID = a.ID,
                             idsp = a.idsp,
                             name= b.name,
                             idhd = a.idhd,
                             price = a.price,
                             quatity = a.quatity,   
                             total=a.price*a.quatity
                         }).FirstOrDefault();
            return query;
        }
        public chitiethoadonVIEW getTongTien(int id,int soluong)
        {

            var query = (from a in context.CHITIETHOADON
                         join b in context.SANPHAM on a.idsp equals b.ID
                         where a.ID == id
                         select new chitiethoadonVIEW
                         {
                             ID = a.ID,
                             idsp = a.idsp,
                             name = b.name,
                             idhd = a.idhd,
                             price = a.price,
                             
                             total = a.quatity*a.price
                         }).FirstOrDefault();
            return query;
        }
        public List<chitiethoadonVIEW> getList()
        {
            var query = (from a in context.CHITIETHOADON
                         join b in context.SANPHAM on a.idsp equals b.ID
                         select new chitiethoadonVIEW
                         {
                             ID = a.ID,
                             idsp = a.idsp,
                             name = b.name,
                             idhd = a.idhd,
                             price = a.price,
                             quatity = a.quatity,
                             total = a.price * a.quatity
                         }).ToList();
            return query;
        }
        public List<chitiethoadonVIEW> Search(int idhd)
        {
            var query = (from a in context.CHITIETHOADON
                         join b in context.SANPHAM on a.idsp equals b.ID
                         where (a.idhd==idhd )
                         select new chitiethoadonVIEW
                         {
                             ID = a.ID,
                             idsp = a.idsp,
                             name = b.name,
                             idhd = a.idhd,
                             price = a.price,
                             quatity = a.quatity,
                             total = a.price * a.quatity
                         }).ToList();
            
           
            return query;
        }
    
        public void Detele(int id)
        {
            CHITIETHOADON x = getItem(id);
            context.CHITIETHOADON.Remove(x);
            context.SaveChanges();
        }
        public int Kiemtra(int idsp,int idhd)
        {
            int id = -1;
            var query = (from a in context.CHITIETHOADON
                       join b in context.HOADON on a.idhd equals b.ID
                         where (a.idhd == idhd &&a.idsp==idsp&&b.status==3)
                         select new chitiethoadonVIEW
                         {
                             ID = a.ID,
                             idsp = a.idsp,
                            
                             idhd = a.idhd,
                             price = a.price,
                             quatity = a.quatity,
                             total = a.price * a.quatity
                         }).FirstOrDefault();
            if (query != null)
                id=query.ID;
            return id;
        }
        public List<chitiethoadonVIEW> getCart(int idkh)
        {
            var query = (from a in context.CHITIETHOADON
                         join b in context.SANPHAM on a.idsp equals b.ID
                         join c in context.HOADON on a.idhd equals c.ID
                         where (c.idkh ==idkh&&c.status==3)
                         select new chitiethoadonVIEW
                         {
                             ID = a.ID,
                             idsp = a.idsp,
                             name = b.name,
                             idhd = a.idhd,
                             price = a.price,
                             quatity = a.quatity,
                             total = a.price * a.quatity,
                             img=b.img,
                             color=b.color,
                             origin=b.origin,
                         }).ToList();
            return query;
        }
        public List<chitiethoadonVIEW> Soluongban(int month, int year)
        {
            DateTime start = DateServices.GetFirstDayOfMonth(month, year);
            DateTime end = DateServices.GetLastDayOfMonth(month, year);

            var query = (from a in context.CHITIETHOADON
                         join b in context.SANPHAM on a.idsp equals b.ID
                         join c in context.HOADON on a.idhd equals c.ID
                         where c.date >= start && c.date <= end
                         group new { a, b } by new { a.idsp, b.name, a.idhd, a.price, b.img, b.color, b.origin } into grouped
                         select new chitiethoadonVIEW
                         {
                             ID = grouped.FirstOrDefault().a.ID,
                             idsp = grouped.Key.idsp,
                             name = grouped.Key.name,
                             idhd = grouped.Key.idhd,
                             price = grouped.Key.price,
                             quatity = grouped.Sum(item => item.a.quatity),
                             total = grouped.Key.price * grouped.Sum(item => item.a.quatity),
                             img = grouped.Key.img,
                             color = grouped.Key.color,
                             origin = grouped.Key.origin,
                         }).ToList();
            return query;
        }

    }
}