using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ReportService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public async Task<ActionResult> Excel()
        {
            const string token = "Pkx2MPUc6QtJjqbk";
            const string data = "{\"CollegeId\":\"AE\",\"TermCode\":\"201601\",\"Honors4590\":3.725,\"HighHonors4590\":3.838,\"HighestHonors4590\":3.894,\"Honors90135\":3.634,\"HighHonors90135\":3.774,\"HighestHonors90135\":3.872,\"Honors135\":3.546,\"HighHonors135\":3.725,\"HighestHonors135\":3.838}\"";

            var client = new HttpClient();
            var result = await client.PostAsync("https://test.caes.ucdavis.edu/ReportService/api/honors?token=" + token, new StringContent(data, Encoding.UTF8, "application/json"));

            var contents = await result.Content.ReadAsByteArrayAsync();

            return File(contents, "application/excel", "Honors.xls");
        }
    }
}
