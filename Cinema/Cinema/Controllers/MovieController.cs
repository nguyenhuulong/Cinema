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
    public class MovieController : Controller
    {
        // GET: Movies
       
        public ActionResult Details(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Movie/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
         
            var responseMessage = client.GetAsync("Details?id=" + id);
            responseMessage.Wait();

            var result = responseMessage.Result;
            if(result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();           
                ViewBag.detail = JObject.Parse(readTask.Result);
            }
            /*Call CurMovie API*/
            List<JObject> listCurMovie = new List<JObject>(9999);
            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri("http://localhost:8085/api/Cinematic/");
            client1.DefaultRequestHeaders.Accept.Clear();
            client1.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            responseMessage = client1.GetAsync("CurMovie");
            responseMessage.Wait();

            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listCurMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listCurMovieJA.Children<JObject>())
                {
                    listCurMovie.Add(o);
                }
                ViewBag.curMovie = listCurMovie;
            }
            return View();
        }
        public ActionResult MovieCurrent()
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            /*Call CurMovie API*/
            List<JObject> listCurMovie = new List<JObject>(9999);
            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri("http://localhost:8085/api/MovieAPI/");
            client1.DefaultRequestHeaders.Accept.Clear();
            client1.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client1.GetAsync("MovieCurrent");
            responseMessage.Wait();

            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listCurMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listCurMovieJA.Children<JObject>())
                {
                    listCurMovie.Add(o);
                }
                ViewBag.current = listCurMovie;
            }
            ViewBag.date = "2021-10-25";
            return View();
        }
        public ActionResult MovieFuture()
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            /*Call CurMovie API*/
            List<JObject> listCurMovie = new List<JObject>(9999);
            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri("http://localhost:8085/api/MovieAPI/");
            client1.DefaultRequestHeaders.Accept.Clear();
            client1.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client1.GetAsync("MovieFuture");
            responseMessage.Wait();

            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listCurMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listCurMovieJA.Children<JObject>())
                {
                    listCurMovie.Add(o);
                }
                ViewBag.future = listCurMovie;
            }
            return View();
        }
    }
}