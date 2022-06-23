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
            /*Call MovieCurrent API*/
            List<JObject> listMovie = new List<JObject>(9999);
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
            /*Call GetLocationInfo API*/
            List<JObject> listLocation = new List<JObject>(9999);
            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri("http://localhost:8085/api/HomepageAPI/");
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
            /*Call GetListSlide API*/
            List<JObject> listSlide = new List<JObject>(9999);
            HttpClient client2 = new HttpClient();
            client2.BaseAddress = new Uri("http://localhost:8085/api/HomepageAPI/");
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            responseMessage = client2.GetAsync("GetListSlide");
            responseMessage.Wait();

            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listSlideJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listSlideJA.Children<JObject>())
                {
                    listSlide.Add(o);
                }
                ViewBag.slide = listSlide;
            }
            /*Call GetReview API*/
            List<JObject> listReview = new List<JObject>(9999);
            HttpClient client3 = new HttpClient();
            client3.BaseAddress = new Uri("http://localhost:8085/api/CinematicAPI/");
            client3.DefaultRequestHeaders.Accept.Clear();
            client3.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            responseMessage = client3.GetAsync("GetReview");
            responseMessage.Wait();

            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listReviewJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listReviewJA.Children<JObject>())
                {
                    listReview.Add(o);
                }
                ViewBag.blp = listReview;
            }
            /*Call GetBlog API*/
            List<JObject> listBlog = new List<JObject>(9999);
            HttpClient client4 = new HttpClient();
            client4.BaseAddress = new Uri("http://localhost:8085/api/CinematicAPI/");
            client4.DefaultRequestHeaders.Accept.Clear();
            client4.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            responseMessage = client4.GetAsync("GetBlog");
            responseMessage.Wait();

            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listBlogJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listBlogJA.Children<JObject>())
                {
                    listBlog.Add(o);
                }
                ViewBag.blog = listBlog;
            }
            /*Call GetSaleNew API*/
            List<JObject> listSale = new List<JObject>(9999);
            HttpClient client5 = new HttpClient();
            client5.BaseAddress = new Uri("http://localhost:8085/api/CinematicAPI/");
            client5.DefaultRequestHeaders.Accept.Clear();
            client5.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            responseMessage = client5.GetAsync("GetSaleNew");
            responseMessage.Wait();

            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listSaleJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listSaleJA.Children<JObject>())
                {
                    listSale.Add(o);
                }
                ViewBag.sale = listSale;
            }
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