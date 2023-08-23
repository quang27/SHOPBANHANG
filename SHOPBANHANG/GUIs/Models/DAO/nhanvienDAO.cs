using GUIs.Models.EF;
using GUIs.Models.VIEW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GUIs.Models.DAO
{
    public class nhanvienDAO
    {
        private dbContext context = new dbContext();
        public void InsertOrUpdate(NHANVIEN item)
        {
            if (item.ID == 0)
            {
                context.NHANVIEN.Add(item);
            }
            context.SaveChanges();
        }
        public NHANVIEN getItem(int id)
        {

            return context.NHANVIEN.Where(x => x.ID == id).FirstOrDefault();
        }
        public nhanvienVIEW getItemView(int id)
        {

            var query = (from a in context.NHANVIEN
                         where a.ID == id
                         select new nhanvienVIEW
                         {
                             ID = a.ID,
                             name = a.name,
                             age = a.age,
                             address = a.address,
                             telephone = a.telephone,
                             startdate = a.startdate,
                             status = a.status,
                             img = a.img
                         }).FirstOrDefault();
            return query;
        }

        public List<nhanvienVIEW> getList()
        {
            var query = (from a in context.NHANVIEN
                         select new nhanvienVIEW
                         {
                             ID = a.ID,
                             name = a.name,
                             age = a.age,
                             address = a.address,
                             telephone = a.telephone,
                             startdate = a.startdate,
                             status = a.status,
                             img = a.img
                         }).ToList();
            return query;
        }
        public List<nhanvienVIEW> Search(String name, int status, out int total, int index = 1, int size = 10)
        {
            var query = (from a in context.NHANVIEN
                         where (a.name.Contains(name) && (a.status == status))
                         select new nhanvienVIEW
                         {
                             ID = a.ID,
                             name = a.name,
                             age = a.age,
                             address = a.address,
                             telephone = a.telephone,
                             startdate = a.startdate,
                             status = a.status,
                             img = a.img
                         }).ToList();
            total = query.Count();
            var result = query.Skip((index - 1) * size).Take(size).ToList();
            return result;
        }
        public void Detele(int id)
        {
            NHANVIEN x = getItem(id);
            context.NHANVIEN.Remove(x);
            context.SaveChanges();
        }
    }
}