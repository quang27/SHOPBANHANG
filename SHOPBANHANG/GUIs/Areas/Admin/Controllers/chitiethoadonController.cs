using GUIs.Models.DAO;
using GUIs.Models.EF;
using GUIs.Models.VIEW;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GUIs.Areas.Admin.Controllers
{
    public class chitiethoadonController : Controller
    {
        // GET: Admin/chitiethoadon
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Create(int idsp, int idhd, int price, int quatity)
        {

            chitiethoadonDAO chitiethoadon = new chitiethoadonDAO();
            CHITIETHOADON item = new CHITIETHOADON();
            item.idsp = idsp;
            item.idhd = idhd;
            item.price = price;
            item.quatity = quatity;
            chitiethoadon.InsertOrUpdate(item);
            return Json(new { mess = "Them sản phẩm thành công" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ShowList(int id)
        {

            chitiethoadonDAO chitiethoadon = new chitiethoadonDAO();
         

            var query = chitiethoadon.Search(id);
            string text = "";

            foreach (var item in query)
            {
                text += "<tr>";
                text += "<td>" + item.ID + "</td>";
                text += "<td>" + item.idsp + "</td>";
                text += "<td>" + item.price + "</td>";
                text += "<td>" + item.quatity + "</td>";
                text += "<td>" + item.idhd + "</td>";

                text += "<td> <a href='/Admin/chitiethoadon/Edit/" + item.ID + "'><i class='fa fa-edit' aria-hidden='true'></i></a>";
                text += " <a href='/Admin/chitiethoadon/Delete/" + item.ID + "'><i class='fa fa-trash' aria-hidden='true'></i> </a></td>";
                text += "</tr>";
            }
            
            return Json(new { data = text}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id)
        {
            chitiethoadonDAO x = new chitiethoadonDAO();
            x.Detele(id);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            chitiethoadonDAO x = new chitiethoadonDAO();
            if (id == null) return RedirectToAction("Index");
            return View(x.getItemView(id.Value));
        }
        [HttpPost]
        public ActionResult Edit(chitiethoadonVIEW model)
        {
            chitiethoadonDAO x = new chitiethoadonDAO();
            var item = x.getItem(model.ID);
            item.idsp =model.idsp;
            item.idhd = model.idhd;
            item.price = model.price;
            item.quatity = model.quatity;
            x.InsertOrUpdate(item);
            return RedirectToAction("Index");
        }
    }
}