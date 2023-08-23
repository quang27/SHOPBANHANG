using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GUIs.Models.VIEW
{
    public class sanphamVIEW
    {
        public int ID { get; set; }

        public string name { get; set; }


        public string origin { get; set; }


        public string img { get; set; }


        public string color { get; set; }

        public int? quatity { get; set; }

        public int? price { get; set; }
    }
}