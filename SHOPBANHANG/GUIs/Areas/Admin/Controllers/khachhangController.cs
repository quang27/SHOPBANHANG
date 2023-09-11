using GUIs.Models.DAO;
using GUIs.Models.EF;
using GUIs.Models.VIEW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GUIs.Areas.Admin.Controllers
{
    public class khachhangController : Controller
    {
        // GET: Admin/khachhang
        public ActionResult Index()
        {
            List<Lopchung> pagesize = new List<Lopchung>();
            pagesize.Add(new Lopchung { ID = 10 });
            pagesize.Add(new Lopchung { ID = 20 });
            pagesize.Add(new Lopchung { ID = 30 });
            pagesize.Add(new Lopchung { ID = 40 });
            pagesize.Add(new Lopchung { ID = 50 });
            ViewBag.Pagesize = pagesize;

            return View();
        }
        public JsonResult Create(string name, int age, string address, string sdt, string img,string username,string password)
        {

            khachhangDAO khachhang = new khachhangDAO();
            KHACHHANG item = new KHACHHANG();
            item.name = name;
            item.age = age;
            item.address = address;
            item.telephone = sdt;
            item.username = username;
            item.password = password;
            item.img = img;
            khachhang.InsertOrUpdate(item);
            return Json(new { mess = "Them khach hang thanh cong" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(int id, string name, int age, string address, string sdt,  string img, string username, string password)
        {

            khachhangDAO khachhang = new khachhangDAO();
            var item = khachhang.getItem(id);
            item.name = name;
            item.age = age;
            item.address = address;
            item.telephone = sdt;
            item.username=username;      
            item.password = password;   
            item.img = img;
            khachhang.InsertOrUpdate(item);
            return Json(new { mess = "Chinh sua khach hang thanh cong" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ShowList(string name = "", int index = 1, int size = 10)
        {

           khachhangDAO khachhang = new khachhangDAO();
            int total = 0;

            var query = khachhang.Search(name, out total, index, size);
            string text = "";

            foreach (var item in query)
            {
                text += "<tr>";
                text += "<td>" + item.ID + "</td>";
                text += "<td>" + item.name + "</td>";
                text += "<td>" + item.age + "</td>";
                text += "<td>" + item.address + "</td>";
                text += "<td>" + item.telephone + "</td>";            
                text += "<td> <img src='" + item.img + "' style='width:40px;height:40px;' class='img-profile rounded-circle'/></td>";
                text += "<td>" +
                    "<a href='javacript:void(0)' data-toggle='modal' data-target='#update' data-whatever='" + item.ID + "'><i class='fa fa-edit'></i></a>" + "</td>";
              
                text += "<td> <a href='/Admin/khachhang/Delete/" + item.ID + "'><i class='fa fa-trash' aria-hidden='true'></i> </a></td>";
                text += "</tr>";
            }
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = text, page = page }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int? id)
        {
            khachhangDAO khachhang = new khachhangDAO();
            if (id == null) return RedirectToAction("Index");
            return View(khachhang.getItemView(id.Value));
        }
        [HttpPost]
        public ActionResult Edit(khachhangVIEW model)
        {
            khachhangDAO lop = new khachhangDAO();
            var item = lop.getItem(model.ID);
            item.name = model.name;
            item.age = model.age;
            item.telephone = model.telephone;
            item.address = model.address;
            item.username = model.username;
            item.password = model.password;
        
            item.img = model.img;
            lop.InsertOrUpdate(item);
            return RedirectToAction("Index");
        }
        public JsonResult getKhachhang(int id)
        {
            khachhangDAO khachhang = new khachhangDAO();

            var query = khachhang.getItemView(id);
            return Json(new { data = query }, JsonRequestBehavior.AllowGet);
        }
    }
}