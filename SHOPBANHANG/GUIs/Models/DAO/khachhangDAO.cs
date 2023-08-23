using GUIs.Models.EF;
using GUIs.Models.VIEW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GUIs.Models.DAO
{
    public class khachhangDAO
    {
        private dbContext context = new dbContext();
        public void InsertOrUpdate(KHACHHANG item)
        {
            if (item.ID == 0)
            {
                context.KHACHHANG.Add(item);
            }
            context.SaveChanges();
        }
        public KHACHHANG getItem(int id)
        {

            return context.KHACHHANG.Where(x => x.ID == id).FirstOrDefault();
        }
        public khachhangVIEW getItemView(int id)
        {

            var query = (from a in context.KHACHHANG
                         where a.ID == id
                         select new khachhangVIEW
                         {
                             ID = a.ID,
                             name = a.name,
                             age = a.age,
                             address = a.address,
                             telephone = a.telephone,
                             username=a.username,
                             password=a.password,   
                             img = a.img
                         }).FirstOrDefault();
            return query;
        }

        public List<khachhangVIEW> getList()
        {
            var query = (from a in context.KHACHHANG
                         select new khachhangVIEW
                         {

                             ID = a.ID,
                             name = a.name,
                             age = a.age,
                             address = a.address,
                             telephone = a.telephone,
                             username = a.username,
                             password = a.password,
                             img = a.img
                         }).ToList();
            return query;
        }
        public List<khachhangVIEW> Search(String name, out int total, int index = 1, int size = 10)
        {
            var query = (from a in context.KHACHHANG
                         where (a.name.Contains(name))
                         select new khachhangVIEW
                         {

                             ID = a.ID,
                             name = a.name,
                             age = a.age,
                             address = a.address,
                             telephone = a.telephone,
                             username = a.username,
                             password = a.password,
                             img = a.img
                         }).ToList();
            total = query.Count();
            var result = query.Skip((index - 1) * size).Take(size).ToList();
            return result;
        }
        public void Detele(int id)
        {
            KHACHHANG x = getItem(id);
            context.KHACHHANG.Remove(x);
            context.SaveChanges();
        }
    }
}