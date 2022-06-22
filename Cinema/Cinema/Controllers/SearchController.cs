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
    public class SearchController : Controller
    {
        static string static_keyword = "";
        public ActionResult Search(string category, string nation, string page)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            int p = Int32.Parse(page);
            string keyword = Request.Form["keyword"];
            if (static_keyword != keyword && keyword != null)
            {
                static_keyword = keyword;
            }
            /*SqlParameter x = new SqlParameter();
            x.Value = static_keyword;*/
            if (category == "all" && nation == "all")
            {
                /*Call SearchFilmByName API*/
                List<JObject> listMovie = new List<JObject>(9999);
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/SearchAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("SearchFilmByName?Name=" + static_keyword);
                responseMessage.Wait();

                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listCurMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listCurMovieJA.Children<JObject>())
                    {
                        listMovie.Add(o);
                    }
                    ViewBag.temp1 = listMovie;
                }
                /*var result = db.Database.SqlQuery<MOVIE>($"exec SearchFilm @name=N'{x.Value}'").ToList();*/
                var res = ViewBag.temp1;
                if ((5 * (p) - res.Count) < 5 && (5 * (p) - res.Count) > 0)
                {
                    ViewBag.result = res.GetRange(5 * (p - 1), res.Count - 5 * (p - 1));
                }
                else
                {
                    if (res.Count - 5 * (p - 1) < 5)
                    {
                        ViewBag.result = res.GetRange(5 * (p - 1), res.Count - 5 * (p - 1));
                    }
                    else
                    {
                        ViewBag.result = res.GetRange(5 * (p - 1), 5);
                    }
                }
                if (res.Count % 5 == 0)
                {
                    ViewBag.page = Int32.Parse((res.Count / 5).ToString());
                }
                else
                {
                    ViewBag.page = Int32.Parse((res.Count / 5).ToString()) + 1;
                }
            }
            if (category == "all" && nation != "all")
            {
                /*Call SearchFilmByNameAndNation API*/
                List<JObject> listMovie = new List<JObject>(9999);
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/SearchAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("SearchFilmByNameAndNation?Name=" + static_keyword + "&nation=" + nation);
                responseMessage.Wait();

                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listCurMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listCurMovieJA.Children<JObject>())
                    {
                        listMovie.Add(o);
                    }
                    ViewBag.temp1 = listMovie;
                }
                /*var result = db.Database.SqlQuery<MOVIE>($"exec SearchFilm @name=N'{x.Value}'").ToList();*/
                var res = ViewBag.temp1;
                if ((5 * (p) - res.Count) < 5 && (5 * (p) - res.Count) > 0)
                {
                    ViewBag.result = res.GetRange(5 * (p - 1), res.Count - 5 * (p - 1));
                }
                else
                {
                    if (res.Count - 5 * (p - 1) < 5)
                    {
                        ViewBag.result = res.GetRange(5 * (p - 1), res.Count - 5 * (p - 1));
                    }
                    else
                    {
                        ViewBag.result = res.GetRange(5 * (p - 1), 5);
                    }
                }
                if (res.Count % 5 == 0)
                {
                    ViewBag.page = Int32.Parse((res.Count / 5).ToString());
                }
                else
                {
                    ViewBag.page = Int32.Parse((res.Count / 5).ToString()) + 1;
                }
            }
            if (nation == "all" && category != "all")
            {
                /*Call SearchFilmByNameAndCategory API*/
                List<JObject> listMovie = new List<JObject>(9999);
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/SearchAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("SearchFilmByNameAndCategory?Name=" + static_keyword + "&Category=" + category);
                responseMessage.Wait();

                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listCurMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listCurMovieJA.Children<JObject>())
                    {
                        listMovie.Add(o);
                    }
                    ViewBag.temp1 = listMovie;
                }
                /*var result = db.Database.SqlQuery<MOVIE>($"exec SearchFilm @name=N'{x.Value}'").ToList();*/
                var res = ViewBag.temp1;
                if ((5 * (p) - res.Count) < 5 && (5 * (p) - res.Count) > 0)
                {
                    ViewBag.result = res.GetRange(5 * (p - 1), res.Count - 5 * (p - 1));
                }
                else
                {
                    if (res.Count - 5 * (p - 1) < 5)
                    {
                        ViewBag.result = res.GetRange(5 * (p - 1), res.Count - 5 * (p - 1));
                    }
                    else
                    {
                        ViewBag.result = res.GetRange(5 * (p - 1), 5);
                    }
                }
                if (res.Count % 5 == 0)
                {
                    ViewBag.page = Int32.Parse((res.Count / 5).ToString());
                }
                else
                {
                    ViewBag.page = Int32.Parse((res.Count / 5).ToString()) + 1;
                }
            }
            if (nation != "all" && category != "all")
            {
                /*Call SearchFilmByNameAndNation_Category API*/
                List<JObject> listMovie = new List<JObject>(9999);
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/SearchAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("SearchFilmByNameAndNation_Category?Name=" + static_keyword + "&Nation=" + nation + "&Category=" + category);
                responseMessage.Wait();

                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listCurMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listCurMovieJA.Children<JObject>())
                    {
                        listMovie.Add(o);
                    }
                    ViewBag.temp1 = listMovie;
                }
                /*var result = db.Database.SqlQuery<MOVIE>($"exec SearchFilm @name=N'{x.Value}'").ToList();*/
                var res = ViewBag.temp1;
                if ((5 * (p) - res.Count) < 5 && (5 * (p) - res.Count) > 0)
                {
                    ViewBag.result = res.GetRange(5 * (p - 1), res.Count - 5 * (p - 1));
                }
                else
                {
                    if (res.Count - 5 * (p - 1) < 5)
                    {
                        ViewBag.result = res.GetRange(5 * (p - 1), res.Count - 5 * (p - 1));
                    }
                    else
                    {
                        ViewBag.result = res.GetRange(5 * (p - 1), 5);
                    }
                }
                if (res.Count % 5 == 0)
                {
                    ViewBag.page = Int32.Parse((res.Count / 5).ToString());
                }
                else
                {
                    ViewBag.page = Int32.Parse((res.Count / 5).ToString()) + 1;
                }
            }
            ViewBag.keyword = static_keyword;

            /*Call FilmCategory API*/

            List<JObject> listcategory = new List<JObject>(9999);
            HttpClient client0 = new HttpClient();
            client0.BaseAddress = new Uri("http://localhost:8085/api/SearchAPI/");
            client0.DefaultRequestHeaders.Accept.Clear();
            client0.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage0 = client0.GetAsync("FilmCategory");
            responseMessage0.Wait();

            var result0 = responseMessage0.Result;
            if (result0.IsSuccessStatusCode)
            {
                var readTask = result0.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listCurMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listCurMovieJA.Children<JObject>())
                {
                    listcategory.Add(o);
                }
                ViewBag.category = listcategory;
            }
            /*ViewBag.category = db.Database.SqlQuery<string>($"exec FilmCategory ").ToList();*/

            /*Call FilmCategory API*/
            List<JObject> listnation = new List<JObject>(9999);
            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri("http://localhost:8085/api/SearchAPI/");
            client1.DefaultRequestHeaders.Accept.Clear();
            client1.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage1 = client1.GetAsync("FilmNation");
            responseMessage1.Wait();

            var result1 = responseMessage1.Result;
            if (result1.IsSuccessStatusCode)
            {
                var readTask = result1.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listCurMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listCurMovieJA.Children<JObject>())
                {
                    listnation.Add(o);
                }
                ViewBag.nation = listnation;
            }
            /*ViewBag.nation = db.Database.SqlQuery<string>($"exec FilmNation ").ToList();*/
            ViewBag.checkcategory = category;
            ViewBag.checknation = nation;
            return View();
        }
    }
}