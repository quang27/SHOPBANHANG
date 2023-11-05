using GUIs.Helper;
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
                             total = a.total??0,//Nếu a.total ==null thì sẽ lấy giá trị 0
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
                             total = a.total??0,
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
                             total = a.total??0,
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
                             total = a.total??0,
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
        public int getTotal(int idhd)
        {
            var query=(from a in context.HOADON
                       join b in context.CHITIETHOADON on a.ID equals b.idhd
                       where a.ID== idhd
                       select new hoadonVIEW
                       {
                           ID = a.ID,
                           idnv = a.idnv,
                           idkh = a.idkh,
                           total =   (b.quatity ?? 0)* (b.price ?? 0),
                           status = a.status,
                           date = a.date,
                           name = a.name,
                           telephone = a.telephone,
                           address = a.address
                       }).ToList() ;
           return  query.Sum(x => x.total);      
        }
        public List<hoadonVIEW> History(int idkh, out int total, int index = 1, int size = 10)
        {
              var query=(from a in context.HOADON
                         join b in context.KHACHHANG on a.idkh equals b.ID
                        
                         where b.ID == idkh && a.status==2
                         select new hoadonVIEW
                         {
                             ID = a.ID,
                             idnv = a.idnv,
                             idkh = a.idkh,
                             total = a.total ?? 0,                          
                             date = a.date,
                             name=a.name,
                             telephone = a.telephone,
                             address = a.address
                         }).ToList();
            total = query.Count();
            var result = query.Skip((index - 1) * size).Take(size).ToList();
            return result;
        }
        public List<int> Doanhthu(int nam)
        {
            List<int> rs = new List<int>();

            for (int i = 1; i < 13; i++)
            {
                DateTime start = DateServices.GetFirstDayOfMonth(i,nam);
                DateTime end = DateServices.GetLastDayOfMonth(i,nam);

                // Lấy danh sách hóa đơn trong khoảng thời gian của tháng hiện tại
                List<hoadonVIEW> query = (from a in context.HOADON
                                          where a.date >= start && a.date <= end
                                          select new hoadonVIEW
                                          {
                                              ID = a.ID,
                                              idnv = a.idnv,
                                              idkh = a.idkh,
                                              total = a.total ?? 0,
                                              date = a.date,
                                              name = a.name,
                                              telephone = a.telephone,
                                              address = a.address
                                          }).ToList();

                // Tính tổng doanh thu của tháng và thêm vào danh sách rs
                int doanhThuThang = query.Sum(item => item.total);
                rs.Add(doanhThuThang);
            }

            return rs;
        }
      

    }
}