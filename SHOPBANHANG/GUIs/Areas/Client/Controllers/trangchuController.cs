using GUIs.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace GUIs.Areas.Client.Controllers
{
    public class trangchuController : Controller
    {
        // GET: Client/trangchu
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ShowList()
        {

            sanphamDAO sanpham = new sanphamDAO();
            string text = "";
            var query = sanpham.getListNew();
            foreach (var item in query)
            {
                text += "<div class='single-product'>";
                text += "<div class='product-f-image'>";
                text += " <img src='" + item.img + "' >";
                text += " <div class='product-hover'>";
                text += "<a href='#' class='add-to-cart-link'><i class='fa fa-shopping-cart'></i> Add to cart</a>";
                text += " <a href='single-product.html' class='view-details-link'><i class='fa fa-link'></i> See details</a>";
                text += "</div>";
                text += "</div>";
                text += "<h2><a href='single-product.html'>" + item.name + "</a></h2>";
                text += " <div class='product-carousel-price'>";
                text += " <ins>" + item.price + "</ins> <del>$100.00</del>";
                text += " </div>";
                text += "</div>";
               
            }
            return Json(new { data = text }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Cart()
        {
            return View();
        }
    }
}