using GUIs.Areas.Admin.Controllers;
using GUIs.Models.DAO;
using GUIs.Models.EF;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace GUIs.Areas.Client.Controllers
{
    public class trangchuController : Controller
    {
        private const string KHACHHANG = "KHACHHANG";
        // GET: Client/trangchu
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
        [HttpPost]
        public JsonResult showlist(string name = "", int index = 1, int size = 8)
        {

            sanphamDAO sanpham = new sanphamDAO();
            string text = "";
            int total = 0;
            var query = sanpham.getListNew(name, out total, index, size);
            foreach (var item in query)
            {
                text += "<div class='col-md-3 p-1'>";
                text += "<div class='card'>";
                text += " <img src='" + item.img + "' class='card-img-top'/>";
                text += " <div class='card-body'>";
                text += "<h5 class='card-title'>" + item.name + "</h5>";
                text += " <p class='card-text'>" + item.color + "</p>";
                text += " <p class='card-text'>" + item.origin + "</p>";
                text += " <p class='card-text'>" + item.price + "</p>";
                text +=
                    "<a href='javacript:void(0)' data-toggle='modal' data-target='#xemchitiet' data-whatever='" + item.ID + "'><i class='fas fa-eye'></i>xem chi tiet</a>";
                text += "</div>";//en card body

                text += "</div>";            //end card
                text += "</div>";            //end col-md-3
            }//
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = text, page = page }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getSanpham(int id)
        {
            sanphamDAO sanpham = new sanphamDAO();

            var query = sanpham.getItemView(id);
            return Json(new { data = query }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id)
        {
            chitiethoadonDAO x = new chitiethoadonDAO();
            x.Detele(id);
            return RedirectToAction("Cart");
        }
        public ActionResult Login()
        {
            return View();
        }
        public JsonResult Dangnhap(string username, string password)
        {
            khachhangDAO khachhang = new khachhangDAO();
            if (khachhang.Login(username, password) == true) {
                Session[KHACHHANG] = khachhang.Login(username);
                return Json(new { mess = "Đăng nhập thành công" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { mess = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddCart(int idsp, int quatity, int price)
        {
            bool kiemtralogin = false;
            if (Session[KHACHHANG] != null)
            {
                kiemtralogin = true;
                hoadonDAO hoadon = new hoadonDAO();
                int idkh = Convert.ToInt16(Session[KHACHHANG]);
                if (hoadon.Kiemtragohang(idkh) == true)
                {
                    chitiethoadonDAO chitiethoadon = new chitiethoadonDAO();
                    int idhd = hoadon.getIDhoadon(idkh);
                    CHITIETHOADON item = new CHITIETHOADON();
                    int cthd = chitiethoadon.Kiemtra(idsp, idhd);
                    if (cthd == -1)
                    {
                        item.idsp = idsp;
                        item.quatity = quatity;
                        item.price = price;
                        item.idhd = idhd;

                    }
                    else
                    {
                        item = chitiethoadon.getItem(cthd);
                        item.quatity += quatity;
                    }
                    chitiethoadon.InsertOrUpdate(item);
                    return Json(new { mess = "Thêm vào giỏ hàng thành công", state = kiemtralogin }, JsonRequestBehavior.AllowGet);
                }
                else if (hoadon.Kiemtragohang(idkh) == false)
                {
                    HOADON hd = new HOADON();
                    hd.idkh = idkh;
                    hd.status = 3;
                    hoadon.InsertOrUpdate(hd);
                    chitiethoadonDAO chitiethoadon = new chitiethoadonDAO();
                    CHITIETHOADON item = new CHITIETHOADON();
                    item.idsp = idsp;
                    item.quatity = quatity;
                    item.price = price;
                    item.idhd = hd.ID;
                    chitiethoadon.InsertOrUpdate(item);
                    return Json(new { mess = "Thêm vào giỏ hàng thành công", state = kiemtralogin }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { mess = "Ko thanh cong ", state = kiemtralogin }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Cart()
        {
            return View();
        }
        public JsonResult MyCart()
        {

            chitiethoadonDAO chitiethoadon = new chitiethoadonDAO();
            int idkh = Convert.ToInt16(Session[KHACHHANG]);
            string text = "";
            var query = chitiethoadon.getCart(idkh);
            foreach (var item in query)
            {
                text += "<tr>";
                text += "<td" + item.ID + "</td>";
                text += "<td>";
                text += " <img src='" + item.img + "' style='width:40px;height:40px;'/>";
                text += "</td>";
                text += "<td>" + item.name + "</td>";
                text += "<td>" + item.color + "</td>";
                text += "<td>" + item.origin + "</td>";
                text += "<td>";
                text += "<input type='number' class='form - control' readonly='readonly' value='" + item.price + "'>";
                text += "</td>";
                text += "<td>";
                text += "<input type='number' class='form - control' id ='qua_" + item.ID + "'  value='" + item.quatity + "' onchange='cart.updateTotal(" + item.ID + ")'>";
                text += "</td>";
                text += "<td>";
                text += "<input type='number' class='form - control' id='total_" + item.ID + "' readonly='readonly' value='" + item.total + "'>";
                text += "</td>";
                text += " <td><a href='/Client/trangchu/Delete/" + item.ID + "'><i class='fa fa-trash' aria-hidden='true'></i> </a></td>";
                text += "</tr>";
            }
            return Json(new { data = text }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Mycart()
        {
            int tong = 0;
            chitiethoadonDAO chitiethoadon = new chitiethoadonDAO();
            int idkh = Convert.ToInt16(Session[KHACHHANG]);
            string text = "";
            var query = chitiethoadon.getCart(idkh);
            foreach (var item in query)
            {
                text += "<tr>";
                text += "<td><img src='" + item.img + "'/></td>";
                text += "<td>" + item.name + "</td>";
                text += "<td>" + item.color + "</td>";
                text += "<td>" + item.origin + "</td>";
                text += "<td><input type='number' class='form - control' readonly='readonly' value='" + item.price + "'> </td>";
                text += "<td>";
                text += "<input type='number' class='form - control' min='0' id ='qua_" + item.ID + "'  value='" + item.quatity + "' onchange='cart.updateTotal(" + item.ID + ")'>";
                text += "</td>";
                text += "<td>";
                text += "<input type='number' class='form - control' id='total_" + item.ID + "' readonly='readonly' value='" + item.total + "'>";
                text += "</td>";
                text += "<td><a href='/Client/trangchu/Delete/" + item.ID + "'><i class='uil uil-times'></i></a></td>";
                tong += item.total.Value;
            }
            return Json(new { data = text,tong=tong }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Order(string name, string sdt, string address)
        {
            hoadonDAO hoadonDAO = new hoadonDAO();
            int idkh = Convert.ToInt16(Session[KHACHHANG]);
            var item = hoadonDAO.getItemOrder(idkh);
            item.name = name;
            item.telephone = sdt;
            item.address = address;
            item.status = 0;
            item.date = DateTime.Now;
            hoadonDAO.InsertOrUpdate(item);
            return Json(new { mess = "Đặt hàng thành công " }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult updateTotal(int id, int soluong)
        {
            chitiethoadonDAO chitiethoadon = new chitiethoadonDAO();
            var item = chitiethoadon.getItem(id);
            item.quatity = soluong;
            var t = item.price * soluong;
            chitiethoadon.InsertOrUpdate(item);
            //Laays toongr tien cua hoa don
            var tt = 0;
            var idhd = item.idhd;
            hoadonDAO hoadon = new hoadonDAO();
            tt = hoadon.getTotal(idhd ?? 0);
            return Json(new { tongtien = t, total = tt }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Home()
        {
            List<Lopchung> pagesize = new List<Lopchung>();
            pagesize.Add(new Lopchung { ID = 8 });
            pagesize.Add(new Lopchung { ID = 12 });
            pagesize.Add(new Lopchung { ID = 16 });
            pagesize.Add(new Lopchung { ID = 20 });
            pagesize.Add(new Lopchung { ID = 24 });
            ViewBag.Pagesize = pagesize;
            return View();
        }
        [HttpPost]
        public JsonResult getHome(string name = "", int index = 1, int size = 8)
        {

            sanphamDAO sanpham = new sanphamDAO();
            string text = "";
            int total = 0;
            var query = sanpham.getListNew(name, out total, index, size);
            foreach (var item in query)
            {
                text += " <div class='product grid'> ";
                text += " <img src='" + item.img + "' alt=''/>";
                text += " <p class='fs-poppins bold-500'>" + item.name + "</p>";
                text += " <p class='fs-poppins bold-500'>" + item.price + "</p> ";

                text += "<div class='product-details grid bg-red'>";
                text += "<a href='javacript:void(0)' data-toggle='modal' data-target='#xemchitiet' data-whatever='" + item.ID + "'><i class='text-white uil uil-shopping-cart-alt'></i></a>";
                text += " <i class='text-white uil uil-heart-alt'> </i>";
                text += "</div>";
                text += " </div>";
                //text +="< a href = '/Client/trangchu/ProductDetail/'" + item.ID +"'>";
                //text +=
                //   "<a href='javacript:void(0)' data-toggle='modal' data-target='#xemchitiet' data-whatever='" + item.ID + "'><i class='fas fa-eye'></i>xem chi tiet</a>";

                //text += "</div>";            
                //text += "</div>";            
            }
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = text, page = page }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult History()
        {
            List<Lopchung> pagesize = new List<Lopchung>();
            pagesize.Add(new Lopchung { ID = 8 });
            pagesize.Add(new Lopchung { ID = 12 });
            pagesize.Add(new Lopchung { ID = 16 });
            pagesize.Add(new Lopchung { ID = 20 });
            pagesize.Add(new Lopchung { ID = 24 });
            ViewBag.Pagesize = pagesize;
            return View();
        }
        public JsonResult OderHistory(int index = 1, int size = 10)
        {

            hoadonDAO hoadon = new hoadonDAO();
            int total = 0;
            int idkh = Convert.ToInt16(Session[KHACHHANG]);
            var query = hoadon.History(idkh, out total, index, size);
            string text = "";

            foreach (var item in query)
            {
                text += "<tr>";
                text += "<td>" + item.ID + "</td>";
                text += "<td>" + item.name + "</td>";  
                text += "<td>" + item.date + "</td>";
                text += "<td>" + item.telephone + "</td>";
                text += "<td>" + item.address + "</td>";
                text += "<td>" + item.total + "</td>";
                text += "</td>";
                text += "<td>" +
                    "<a href='javacript:void(0)' data-toggle='modal' data-target='#xemchitiet' data-whatever='" + item.ID + "'> <i class='fas fa-eye'></i></a></td>";            
                text += "</tr>";
            }
            string page = Support.Support.InTrang(total, index, size);
            return Json(new { data = text, page = page }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult OrderDetail(int id)
        {

            chitiethoadonDAO chitiethoadon = new chitiethoadonDAO();


            var query = chitiethoadon.Search(id);
            string text = "";

            foreach (var item in query)
            {
                text += "<tr>";
                text += "<td>" + item.ID + "</td>";
                text += "<td>" + item.idhd + "</td>";
                text += "<td>" + item.name + "</td>";
                text += "<td>" + item.price + "</td>";
                text += "<td>" + item.quatity + "</td>";
                text += "<td>" + item.total + "</td>";

                text += "<td> <a href='/Admin/chitiethoadon/Edit/" + item.ID + "'><i class='fa fa-edit' aria-hidden='true'></i></a>";
                text += " <a href='/Admin/chitiethoadon/Delete/" + item.ID + "'><i class='fa fa-trash' aria-hidden='true'></i> </a></td>";
                text += "</tr>";
            }

            return Json(new { data = text }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckLogin()
        {
            string text = "";
            int idkh = Convert.ToInt16(Session[KHACHHANG]);
            //idkh == null
            if(Session[KHACHHANG] != null)
                {
                text = " <p class='fs-100 fs-montserrat bold-500'><a href='/Client/trangchu/Logout'>Logout</a></p>";
            }
            if(Session[KHACHHANG] == null)
            {
                text = " <p class='fs-100 fs-montserrat bold-500'><a href='/Client/trangchu/Login'>Login</a></p>";
            }
            return Json(new { data = text }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout() 
        {
            Session.Clear();
            return RedirectToAction("Home");
        }
       
    }
   
}