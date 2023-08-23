using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GUIs.Models.VIEW
{
    public class hoadonVIEW
    {
        public int ID { get; set; }

        public int? idnv { get; set; }

        public int? idkh { get; set; }

        public int? total { get; set; }

        public int? status { get; set; }

        public DateTime? date { get; set; }

  
        public string name { get; set; }


        public string telephone { get; set; }

    
        public string address { get; set; }
        public string khachhang { get; set; }
        public string nhanvien { get; set; }
    }
}