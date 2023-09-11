using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GUIs.Models.VIEW
{
    public class chitiethoadonVIEW
    {
        public int ID { get; set; }

        public int? idsp { get; set; }
        public string name { get; set; }

        public int? idhd { get; set; }

        public int? price { get; set; }

        public int? quatity { get; set; }
        public int? total { get; set; }
        public string img { get; set; }
        public string color { get; set; }
        public string origin { get; set; }
    }
}