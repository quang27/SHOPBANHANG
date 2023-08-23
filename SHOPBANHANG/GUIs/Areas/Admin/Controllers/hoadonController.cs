using Antlr.Runtime.Misc;
using GUIs.Models.DAO;
using GUIs.Models.VIEW;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace GUIs.Areas.Admin.Controllers
{
    public class hoadonController : Controller
    {
        // GET: Admin/hoadon
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
        public JsonResult ShowList( int status, int index = 1, int size = 10)
        {

            hoadonDAO hoadon = new hoadonDAO();
            int total = 0;

            var query = hoadon.Search(status, out total, index, size);
            string text = "";
            
            foreach (var item in query)
            {
                text += "<tr>";
                text += "<td>" + item.ID + "</td>";
              
                text += "<td>" + item.name + "</td>";
                text += "<td>" + item.total + "</td>";          
                text += "<td>" + item.date + "</td>";
                text += "<td>" + item.telephone + "</td>";
                text += "<td>" + item.address + "</td>";
                text += "<td>" + item.khachhang + "</td>";
                text += "<td>" + item.nhanvien + "</td>";
                text += "<td>" + item.status;
                
                text += "</td>";
                text += "<td>" +
                    "<a href='javacript:void(0)' onclick='hoadon.xemchitiet(" + item.ID + ")' data-toggle='modal' data-target='#xemchitiet' data-whatever='" + item.ID + "' ><i class='fas fa-eye'></i></a></td>";
                text += "<td> <a href='/Admin/hoadon/Edit/" + item.ID + "'><i class='fa fa-edit' aria-hidden='true'></i></a>";
                text += " <a href='/Admin/hoadon/Delete/" + item.ID + "'><i class='fa fa-trash' aria-hidden='true'></i> </a></td>";
                text += "</tr>";
            }
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = text, page = page }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int? id)
        {
            hoadonDAO hoadon = new hoadonDAO();
            if (id == null) return RedirectToAction("Index");
            return View(hoadon.getItemView(id.Value));
        }
        [HttpPost]
        public ActionResult Edit(hoadonVIEW model)
        {
            hoadonDAO hoadon = new hoadonDAO();
            var item = hoadon.getItem(model.ID);
            item.idnv = model.idnv;
            item.idkh = model.idkh;
            item.total = model.total;
            item.status = model.status;
            item.date = model.date;
            item.name = model.name;
            item.telephone = model.telephone;
            item.address = model.address;
            hoadon.InsertOrUpdate(item);
            return RedirectToAction("Index");
        }
    }
}