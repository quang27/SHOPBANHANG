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
    public class Lopchung
    {
        public int ID { set; get; }
        public string Name { set; get; }
    }
    public class nhanvienController : Controller
    {
        // GET: Admin/nhanvien
        public const string idnhanvien = "ID_NHANVIEN";
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
        public JsonResult Create(string name, int age, string address, string sdt, string img)
        {

            nhanvienDAO nhanvien = new nhanvienDAO();
            NHANVIEN item = new NHANVIEN();
            item.name = name;
            item.age = age;
            item.address = address;
            item.telephone = sdt;
            item.startdate = DateTime.Now;
            item.status = 1;
            item.img = img;
            nhanvien.InsertOrUpdate(item);
            return Json(new { mess = "Them nhan vien thanh cong" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(string name, int age, string address, string sdt, int status, string img)
        {

            nhanvienDAO nhanvien = new nhanvienDAO();
            var item = nhanvien.getItem((int)Session[idnhanvien]);
            item.name = name;
            item.age = age;
            item.address = address;
            item.telephone = sdt;
            item.status = status;
            item.img = img;
            nhanvien.InsertOrUpdate(item);
            return Json(new { mess = "Chinh sua nhan vien thanh cong" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShowList(string name = "", int status = 1, int index = 1, int size = 10)
        {

            nhanvienDAO nhanvien = new nhanvienDAO();
            int total = 0;

            var query = nhanvien.Search(name, status, out total, index, size);
            string text = "";

            foreach (var item in query)
            {
                text += "<tr>";
                text += "<td>" + item.ID + "</td>";
                text += "<td>" + item.name + "</td>";
                text += "<td>" + item.age + "</td>";
                text += "<td>" + item.address + "</td>";
                text += "<td>" + item.telephone + "</td>";
                text += "<td>" + item.startdate + "</td>";
                text += "<td> <img src='" + item.img + "' style='width:40px;height:40px;' class='img-profile rounded-circle'/></td>";
                text += "<td>" +
                    "<a href='javacript:void(0)' data-toggle='modal' data-target='#update' data-whatever='" + item.ID + "'><i class='fa fa-edit'></i></a>" + "</td>";
                text += "<td> <a href='/Admin/nhanvien/Edit/" + item.ID + "'><i class='fa fa-edit' aria-hidden='true'></i></a>";
                text += " <a href='/Admin/nhanvien/Delete/" + item.ID + "'><i class='fa fa-trash' aria-hidden='true'></i> </a></td>";
                text += "</tr>";
            }
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = text, page = page }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id)
        {
            nhanvienDAO x = new nhanvienDAO();
            x.Detele(id);
            return RedirectToAction("Index");
        }
        public JsonResult getNhanvien(int id)
        {
            nhanvienDAO nhanvien = new nhanvienDAO();

            var query = nhanvien.getItemView(id);
            return Json(new { data = query }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int? id)
        {
            nhanvienDAO nhanvien = new nhanvienDAO();
            if (id == null) return RedirectToAction("Index");
            return View(nhanvien.getItemView(id.Value));
        }
        [HttpPost]
        public ActionResult Edit(nhanvienVIEW model)
        {
            nhanvienDAO lop = new nhanvienDAO();
            var item = lop.getItem(model.ID);
            item.name = model.name;
            item.age= model.age;
            item.telephone = model.telephone;
            item.address = model.address;
            item.username = model.username;
            item.password = model.password;
            item.status = model.status;
            item.img = model.img;
            lop.InsertOrUpdate(item);
            return RedirectToAction("Index");
        }
    }
}