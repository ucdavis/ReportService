using System;
using System.Collections.Generic;
using Microsoft.Azure;
using Microsoft.Reporting.WebForms;

namespace ReportService.Helpers
{
    public class ReportHelper
    {
        /// <summary>
        /// Calls the sql report server and gets the byte array
        /// </summary>
        /// <param name="reportName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static byte[] Get(string reportName, Dictionary<string, string> parameters)
        {
            string reportServer = CloudConfigurationManager.GetSetting("ReportServer");

            var rview = new ReportViewer();
            rview.ServerReport.ReportServerUrl = new Uri(reportServer);
            rview.ServerReport.ReportPath = reportName;

            var paramList = new List<ReportParameter>();

            if (parameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> kvp in parameters)
                {
                    paramList.Add(new ReportParameter(kvp.Key, kvp.Value));
                }
            }

            rview.ServerReport.SetParameters(paramList);

            string mimeType, encoding, extension, deviceInfo;
            string[] streamids;
            Warning[] warnings;

            var format = "EXCEL";

            deviceInfo =
                "<DeviceInfo>" +
                "<SimplePageHeaders>True</SimplePageHeaders>" +
                "<HumanReadablePDF>True</HumanReadablePDF>" +   // this line disables the compression done by SSRS 2008 so that it can be merged.
                "</DeviceInfo>";

            byte[] bytes = rview.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);

            return bytes;
        }

    }
}