using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
namespace Cinema.Areas.Admin.Controllers
{
    public class MovieAdminController : Controller
    {
        private readonly RestClient _client;
        public MovieAdminController()
        {
            _client = new RestClient("http://localhost:8085/Help");
        }
        // GET: Admin/Movie
        public ActionResult MovieAdmin()
        {
            var ID = (Session["Role"]).ToString();
            if (ID == "2" || ID == "1")
            {
                ViewBag.name = Session["AdminID"];
                
                List<JObject> dangchieu = new List<JObject>(9999);
                List<JObject> sapchieu = new List<JObject>(9999);
                List<JObject> phim = new List<JObject>(9999);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/Admin/"); // ???
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetCurrentFilm");
                responseMessage.Wait();
                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        dangchieu.Add(o);
                    }
                    ViewBag.dangchieu = dangchieu.Count;
                }

                responseMessage = client.GetAsync("GetFutureFilm");
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        sapchieu.Add(o);
                    }
                    ViewBag.sapchieu = sapchieu.Count;
                }

                responseMessage = client.GetAsync("GetMovieInfo");
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        phim.Add(o);
                    }
                    ViewBag.phim = phim.Count;
                }

 

                return View();
            }
            else

                return RedirectToAction("Homepage", "Admin");
        }
        public ActionResult AddMovieView()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult AddMovieAdmin()
        {
           
            string movieName = Request.Form["movie-name"];
            string moviedirector = Request.Form["movie-director"];
            string movieactor = Request.Form["movie-actor"];
            string movienation = Request.Form["movie-nation"];
            string movielength = Request.Form["movie-length"];
            string moviestatus = Request.Form["movie-status"];


            string MovieID = db.Database.SqlQuery<String>("exec GetMaxMovieID").ToList()[0];
            MOVIE mv = new MOVIE();
            mv.MovieID = "MV";
            for (int i = 1; i <= (MovieID.Length - (Int32.Parse(MovieID.Substring(2, MovieID.Length - 2))).ToString().Length) - 2; i++)
            {
                mv.MovieID += "0";
            }
            mv.MovieID = mv.MovieID + (Int32.Parse(MovieID.Substring(2, MovieID.Length - 2)) + 1).ToString();
            mv.MovieName = movieName;
            mv.Director = moviedirector;
            mv.Actor = movieactor;
            mv.MovieLength = Byte.Parse(movielength);
            mv.MovieStatus = Byte.Parse(moviestatus);
            db.MOVIEs.Add(mv);
            db.SaveChanges();
            return RedirectToAction("/MovieAdmin");
        }
        public ActionResult EditMovieView(string movieId)
        {
            List<JObject> movie = new List<JObject>(9999);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Media/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));



            var responseMessage = client.GetAsync("FindMovieById?id=" + movieId);
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    movie.Add(o);
                }
                ViewBag.movie = movie;
            }
            
            ViewBag.movieId = movieId;
            return View();
        }

        public ActionResult EditMovieAdmin(string movieId)
        {
            string movieName = Request.Form["movie-name"];
            string moviedirector = Request.Form["movie-director"];
            string movieactor = Request.Form["movie-actor"];
            string moviecategory = Request.Form["movie-category"];
            string movielength = Request.Form["movie-length"];
            Byte moviestatus = Convert.ToByte(Request.Form["movie-status"]);
            MOVIE mv = db.MOVIEs.Find(movieId);
            mv.MovieName = movieName;
            mv.Director = moviedirector;
            mv.Actor = movieactor;
            mv.Category = moviecategory;
            mv.MovieLength = Byte.Parse(movielength);
            mv.MovieStatus = moviestatus;
            db.SaveChanges();
            return RedirectToAction("/MovieAdmin");
        }
        public ActionResult DeleteMovieAdmin(string movieId)
        {
            var request = new RestRequest($"api/MovieAdmin/DeleteMovieAdmin/{movieId}", Method.Delete);
            _client.Execute(request);
            return RedirectToAction("/MovieAdmin");
        }
    }
}