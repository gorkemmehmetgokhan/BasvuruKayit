using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasvuruKayit.Models
{
    public class ReportParams
    {
        public string RptFileName { get; set; }  
        public DataTable DataSource { get; set; }
        public string DataSetName { get; internal set; }

    }
}