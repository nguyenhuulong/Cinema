using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Cinema.Controllers
{
    public class HomepageController : Controller
    {
        // GET: Homepage
        public ActionResult Homepage()
        {
            List<JObject> listMovie = new List<JObject>(9999);
            List<JObject> listLocation = new List<JObject>(9999);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Movie/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            var responseMessage = client.GetAsync("MovieCurrent");
            responseMessage.Wait();

            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach(JObject o in listMovieJA.Children<JObject>())
                {
                    listMovie.Add(o);
                }
                ViewBag.movie = listMovie;
            }

            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri("http://localhost:8085/api/PublicAPI/");
            client1.DefaultRequestHeaders.Accept.Clear();
            client1.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            responseMessage = client1.GetAsync("GetLocationInfo");
            responseMessage.Wait();

            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listLocationJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listLocationJA.Children<JObject>())
                {
                    listLocation.Add(o);
                }
                ViewBag.listlocation = listLocation;
            }

            ViewBag.blp = null;
            ViewBag.blog = null;
            ViewBag.sale = null;
            ViewBag.slide = null;

            ViewBag.checklocation = "none";
            ViewBag.checkcinema = "none";

            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            return View();
        }
    }
}