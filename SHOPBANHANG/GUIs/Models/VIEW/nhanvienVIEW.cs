﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GUIs.Models.VIEW
{
    public class nhanvienVIEW
    {
        public int ID { get; set; }

     
        public string name { get; set; }

        public int? age { get; set; }

     
        public string telephone { get; set; }

        public string address { get; set; }

        public DateTime? startdate { get; set; }

    
        public string username { get; set; }

    
        public string password { get; set; }

        public int? status { get; set; }

        public string img { get; set; }
    }
}