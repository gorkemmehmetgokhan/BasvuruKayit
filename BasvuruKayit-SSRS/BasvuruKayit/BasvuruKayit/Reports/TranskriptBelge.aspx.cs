using Microsoft.Reporting.WebForms;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BasvuruKayit.Reports
{
    public partial class TranskriptBelge : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReport();
            }
        }

        private void LoadReport()
        {
            var reportParam = (dynamic)HttpContext.Current.Session["ReportParam"];
            if (reportParam != null && !string.IsNullOrEmpty(reportParam.RptFileName))
            {
                var dt = new DataTable();
                dt = reportParam.DataSource;
                if (dt.Rows.Count > 0)
                {
                    GenerateReportDocument(reportParam, dt);
                }
                else
                {
                    ShowErrorMessage();
                }
            }
        }

        private void GenerateReportDocument(dynamic reportParam, DataTable data)
        {
            //ReportParameter[] rptParameters = new ReportParameter[0];
            //if (reportParam.IsHasParams)
            //{
            //    rptParameters = ReportParameters(reportParam,rptParameters);
            //}
            string dsName = reportParam.DataSetName;
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource(dsName, data));
            rptViewer.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//" + reportParam.RptFileName);
            rptViewer.LocalReport.EnableExternalImages = true;
            //if (reportParam.IsHasParams)
            //{
            //    rptViewer.LocalReport.SetParameters(rptParameters);
            //}
            rptViewer.DataBind();
            rptViewer.LocalReport.Refresh();
        }

        //private static ReportParameter[] ReportParameters(dynamic reportParam, ReportParameter[] rptParameters)
        //{
        //    rptParameters = new ReportParameter[1]; //parametre sayısı
        //    rptParameters[0] = new ReportParameter("OgrenciNo", reportParam.OgrenciNo);         
        //    return rptParameters;

        //}
        public void ShowErrorMessage()
        {
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("", new DataTable()));
            rptViewer.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//blank.rdlc");
            rptViewer.DataBind();
            rptViewer.LocalReport.Refresh();

        }
    }
}