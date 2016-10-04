using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReportService.Helpers;

namespace ReportService.Controllers
{
    public class HonorsController : ApiController
    {
        // POST api/Honors/
        public byte[] Post([FromBody]Honors honors)
        {
            var parameters = new Dictionary<string, string>();
            var reportName = "/Commencement/Honors";

            // set the shared parameters
            parameters.Add("term", honors.TermCode);
            parameters.Add("honors_4590", honors.Honors4590.ToString(CultureInfo.InvariantCulture));
            parameters.Add("honors_90135", honors.Honors90135.ToString(CultureInfo.InvariantCulture));
            parameters.Add("honors_135", honors.Honors135.ToString(CultureInfo.InvariantCulture));

            if (string.Equals(honors.CollegeId, "LS", StringComparison.OrdinalIgnoreCase))
            {
                reportName = "/Commencement/HonorsLS";
            }
            else
            {
                parameters.Add("coll", honors.CollegeId);

                parameters.Add("highhonors_4590", honors.HighHonors4590.ToString());
                parameters.Add("highhonors_90135", honors.HighHonors90135.ToString());
                parameters.Add("highhonors_135", honors.HighHonors135.ToString());

                parameters.Add("highesthonors_4590", honors.HighestHonors4590.ToString());
                parameters.Add("highesthonors_90135", honors.HighestHonors90135.ToString());
                parameters.Add("highesthonors_135", honors.HighestHonors135.ToString());
            }

            return ReportHelper.Get(reportName, parameters);
        }
    }

    public class Honors
    {
        public string CollegeId { get; set; }
        public string TermCode { get; set; }
        public decimal Honors4590 { get; set; }
        public decimal? HighHonors4590 { get; set; }
        public decimal? HighestHonors4590 { get; set; }

        public decimal Honors90135 { get; set; }
        public decimal? HighHonors90135 { get; set; }
        public decimal? HighestHonors90135 { get; set; }

        public decimal Honors135 { get; set; }
        public decimal? HighHonors135 { get; set; }
        public decimal? HighestHonors135 { get; set; }
    }
}