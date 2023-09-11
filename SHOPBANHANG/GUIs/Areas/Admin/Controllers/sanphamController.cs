using GUIs.Models.DAO;
using GUIs.Models.EF;
using GUIs.Models.VIEW;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GUIs.Areas.Admin.Controllers
{
    public class sanphamController : Controller
    {
        // GET: Admin/sanpham
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
        public JsonResult Create(string name, string origin, string img, string color, int quatity,int price)
        {

            sanphamDAO sanpham = new sanphamDAO();
            SANPHAM item = new SANPHAM();
            item.name = name;
            item.origin = origin;
            item.img = img;
            item.color = color;
            item.quatity = quatity;
            item.price = price;          
            sanpham.InsertOrUpdate(item);
            return Json(new { mess = "Them san pham thanh cong" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(int id, string name,string origin,string img,string color,int quatity,int price)
        {

            sanphamDAO sanpham = new sanphamDAO();
            var item = sanpham.getItem(id);
            item.name = name;
            item.origin = origin;
            item.img = img;
            item.color = color;
            item.quatity = quatity;
            item.price = price;
            sanpham.InsertOrUpdate(item);
            return Json(new { mess = "Chinh sua san pham thanh cong" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShowList(string name = "", int index = 1, int size = 10)
        {

            sanphamDAO sanpham = new sanphamDAO();
            int total = 0;

            var query = sanpham.Search(name, out total, index, size);
            string text = "";

            foreach (var item in query)
            {
                text += "<tr>";
                text += "<td>" + item.ID + "</td>";
                text += "<td>" + item.name + "</td>";
                text += "<td>" + item.origin + "</td>";
                text += "<td>" + item.color + "</td>";
                text += "<td>" + item.quatity + "</td>";
                text += "<td>" + item.price + "</td>";
                text += "<td> <img src='" + item.img + "' style='width:40px;height:40px;' class='img-profile rounded-circle'/></td>";
                text += "<td>" +
                    "<a href='javacript:void(0)'  data-toggle='modal' data-target='#update' data-whatever='" + item.ID + "'><i class='fa fa-edit'></i></a>" + "</td>";
               
                text += " <td><a href='/Admin/sanpham/Delete/" + item.ID + "'><i class='fa fa-trash' aria-hidden='true'></i> </a></td>";
                text += "</tr>";
            }
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = text, page = page }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getSanpham(int id)
        {
            sanphamDAO sanpham = new sanphamDAO();

            var query = sanpham.getItemView(id);
            return Json(new { data = query }, JsonRequestBehavior.AllowGet);
        }
    }
}