using GUIs.Models.EF;
using GUIs.Models.VIEW;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GUIs.Models.DAO
{
    public class hoadonDAO
    {
        private dbContext context = new dbContext();
        public void InsertOrUpdate(HOADON item)
        {
            if (item.ID == 0)
            {
                context.HOADON.Add(item);
            }
            context.SaveChanges();
        }
        public HOADON getItem(int id)
        {

            return context.HOADON.Where(x => x.ID == id).FirstOrDefault();
        }
        public HOADON getItemOrder(int idkh)
        {

            return context.HOADON.Where(x => x.idkh == idkh&&x.status==3).FirstOrDefault();
        }
        public hoadonVIEW getItemView(int id)
        {

            var query = (from a in context.HOADON
                         join b in context.KHACHHANG on a.idkh equals b.ID
                         join c in context.NHANVIEN on a.idnv equals c.ID
                         where a.ID == id
                         select new hoadonVIEW
                         {
                             ID = a.ID,
                             idnv = a.idnv,
                             idkh = a.idkh,
                             total = a.total,
                             status = a.status,
                             date = a.date,
                             name = a.name,
                             telephone = a.telephone,
                             address = a.address,
                             khachhang = b.name,
                             nhanvien = c.name
                         }).FirstOrDefault();
            return query;
        }

        public List<hoadonVIEW> getList()
        {
            var query = (from a in context.HOADON
                         join b in context.KHACHHANG on a.idkh equals b.ID
                         join c in context.NHANVIEN on a.idnv equals c.ID
                         select new hoadonVIEW
                         {
                             ID = a.ID,
                             idnv = a.idnv,
                             idkh = a.idkh,
                             total = a.total,
                             status = a.status,
                             date = a.date,
                             name = a.name,
                             telephone = a.telephone,
                             address = a.address,
                             khachhang = b.name,
                             nhanvien = c.name
                         }).ToList();
            return query;
        }
        public List<hoadonVIEW> Search(int status, out int total, int index = 1, int size = 10)
        {
            var query = (from a in context.HOADON
                         join b in context.KHACHHANG on a.idkh equals b.ID
                         join c in context.NHANVIEN on a.idnv equals c.ID
                         where  (a.status == status)
                         select new hoadonVIEW
                         {
                             ID = a.ID,
                             idnv = a.idnv,
                             idkh = a.idkh,
                             total = a.total,
                             status = a.status,
                             date = a.date,
                             name = a.name,
                             telephone = a.telephone,
                             address = a.address,
                             khachhang=b.name,
                             nhanvien=c.name
                         }).ToList();
            total = query.Count();
            var result = query.Skip((index - 1) * size).Take(size).ToList();
            return result;
        }
        public void Detele(int id)
        {
            HOADON x = getItem(id);
            context.HOADON.Remove(x);
            context.SaveChanges();
        }
        public Boolean Kiemtragohang(int idkh)
        {
            var query = (from a in context.HOADON                     
                         where (a.idkh == idkh &&a.status==3)
                         select new hoadonVIEW
                         {
                             ID = a.ID,
                             idnv = a.idnv,
                             idkh = a.idkh,
                             total = a.total,
                             status = a.status,
                             date = a.date,
                             name = a.name,
                             telephone = a.telephone,
                             address = a.address                         
                         }).FirstOrDefault();
            if (query != null)
                return true;
            return false;
        }
        public int getIDhoadon(int idkh)
        {
            var query = (from a in context.HOADON
                         where (a.idkh == idkh && a.status == 3)
                         select new hoadonVIEW
                         {
                             ID = a.ID,                           
                         }).FirstOrDefault();
           
            return query.ID;
        }
        
    }
}