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
    public class BookingController : Controller
    {
        public ActionResult Booking(string name, string location, string dayx)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }

            /*Call GetCurrentFilm API*/
            List<JObject> listMovie = new List<JObject>(9999);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/BookingAPI/");
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
                    listMovie.Add(o);
                }
                ViewBag.movie = listMovie;
            }

            /*Call GetLocationInfo API*/
            List<JObject> listLC = new List<JObject>(9999);
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
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    listLC.Add(o);
                }
                ViewBag.LC = listLC;
            }
            List<string> AllDay = new List<string>();
            List<string> Now = new List<string>();
            AllDay.Add("2021-10-25");
            AllDay.Add("2021-10-26");
            AllDay.Add("2021-10-27");
            AllDay.Add("2021-10-28");
            AllDay.Add("2021-10-29");
            AllDay.Add("2021-10-30");
            AllDay.Add("2021-10-31");
            ViewBag.AllDay = AllDay;
            Now.Add("23/6/2022");
            Now.Add("24/6/2022");
            Now.Add("25/6/2022");
            Now.Add("26/6/2022");
            Now.Add("27/6/2022");
            Now.Add("28/6/2022");
            Now.Add("29/6/2022");
            
            ViewBag.Now = Now;
            if (location == "all")
            {
                /*Call GetListCinemaFromFilm API*/
                List<JObject> listcine = new List<JObject>(9999);
                HttpClient client2 = new HttpClient();
                client2.BaseAddress = new Uri("http://localhost:8085/api/BookingAPI/");
                client2.DefaultRequestHeaders.Accept.Clear();
                client2.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                responseMessage = client2.GetAsync("GetListCinemaFromFilm?MovieName=" + name + "&date=" + dayx);
                responseMessage.Wait();

                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        listcine.Add(o);
                    }
                    ViewBag.cinema = listcine;
                }

                /*ViewBag.cinema = db.Database.SqlQuery<CinemaItem1>($"exec GetListCinemaFromFilm N'{name}', N'2021-10-25'").ToList();*/
            }
            else
            {
                /*Call GetListCinemaFromFilm API*/
                List<JObject> listcine = new List<JObject>(9999);
                HttpClient client2 = new HttpClient();
                client2.BaseAddress = new Uri("http://localhost:8085/api/BookingAPI/");
                client2.DefaultRequestHeaders.Accept.Clear();
                client2.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                responseMessage = client2.GetAsync("GetListCinemaFromFilmAndLocation?MovieName=" + name +"&Location=" + location + "&date=" + dayx);
                responseMessage.Wait();

                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        listcine.Add(o);
                    }
                    ViewBag.cinema = listcine;
                }
                /*ViewBag.cinema = db.Database.SqlQuery<CinemaItem1>($"exec GetListCinemaFromFilmAndLocation N'{name}', N'2021-10-25',N'{location}'").ToList();*/
            }
            foreach (var item in ViewBag.cinema)
            {
                /*Call GetList2DRoomFromFilm API*/
                List<JObject> list2d = new List<JObject>(9999);
                HttpClient client5 = new HttpClient();
                client5.BaseAddress = new Uri("http://localhost:8085/api/BookingAPI/");
                client5.DefaultRequestHeaders.Accept.Clear();
                client5.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                responseMessage = client5.GetAsync("GetList2DRoomFromFilm?CinemaName=" + item.CinemaName + "&MovieName=" + name + "&date=" + dayx);
                responseMessage.Wait();

                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listFilmJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listFilmJA.Children<JObject>())
                    {
                        list2d.Add(o);
                    }
                    item.Add("typeA", JArray.FromObject(list2d));
                }

                /*item.type1 = db.Database.SqlQuery<ShowTime>($"exec GetList2DRoomFromFilm N'{cinemaname}',N'{item.MovieName}', N'2021-10-25'").ToList();*/

                foreach (var item1 in item.typeA)
                {
                    /*Call QuantityLeft API*/
                    List<JObject> left = new List<JObject>(9999);
                    HttpClient client6 = new HttpClient();
                    client6.BaseAddress = new Uri("http://localhost:8085/api/CinemaAPI/");
                    client6.DefaultRequestHeaders.Accept.Clear();
                    client6.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    responseMessage = client6.GetAsync("QuantityLeft?MovieName=" + name + "&RoomID=" + item1.roomid + "&ShowTime=" + item1.showtime);
                    responseMessage.Wait();

                    result = responseMessage.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        string str = readTask.Result;
                        str = str.Substring(1, str.Length - 2);
                        item1.seat_available = str;
                    }
                    /*item1.seat_available = db.Database.SqlQuery<int>($"exec QuantityLeft N'{item.MovieName}', '{item1.roomid}',N'{item1.showtime}'").ToList()[0];*/
                }

                /*Call GetListFilmFromCinema API*/
                List<JObject> list3d = new List<JObject>(9999);
                HttpClient client7 = new HttpClient();
                client7.BaseAddress = new Uri("http://localhost:8085/api/BookingAPI/");
                client7.DefaultRequestHeaders.Accept.Clear();
                client7.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                responseMessage = client7.GetAsync("GetList3DRoomFromFilm?CinemaName=" + item.CinemaName + "&MovieName=" + name + "&date=" + dayx);
                responseMessage.Wait();

                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listFilmJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listFilmJA.Children<JObject>())
                    {
                        list3d.Add(o);
                    }
                    item.Add("typeB", JArray.FromObject(list3d));
                }

                /*item.type2 = db.Database.SqlQuery<ShowTime>($"exec GetList3DRoomFromFilm N'{cinemaname}',N'{item.MovieName}', N'2021-10-25'").ToList();*/

                foreach (var item1 in item.typeB)
                {
                    /*Call QuantityLeft API*/
                    List<JObject> left = new List<JObject>(9999);
                    HttpClient client8 = new HttpClient();
                    client8.BaseAddress = new Uri("http://localhost:8085/api/BookingAPI/");
                    client8.DefaultRequestHeaders.Accept.Clear();
                    client8.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    responseMessage = client8.GetAsync("QuantityLeft?MovieName=" + name + "&RoomID=" + item1.roomid + "&ShowTime=" + item1.showtime);
                    responseMessage.Wait();

                    result = responseMessage.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        /*JArray listFilmJA = JArray.Parse(readTask.Result);
                        foreach (JObject o in listFilmJA.Children<JObject>())
                        {
                            left.Add(o);
                        }
                        item1.seat_available = left[0];*/
                        string str = readTask.Result;
                        str = str.Substring(1, str.Length - 2);
                        item1.seat_available = str;
                    }
                    /*item1.seat_available = db.Database.SqlQuery<int>($"exec QuantityLeft N'{item.MovieName}', '{item1.roomid}',N'{item1.showtime}'").ToList()[0];*/
                }

                /*Call GetListFilmFromCinema API*/
                List<JObject> list4d = new List<JObject>(9999);
                HttpClient client9 = new HttpClient();
                client9.BaseAddress = new Uri("http://localhost:8085/api/BookingAPI/");
                client9.DefaultRequestHeaders.Accept.Clear();
                client9.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                responseMessage = client9.GetAsync("GetList4DRoomFromFilm?CinemaName=" + item.CinemaName + "&MovieName=" + name + "&date="+ dayx);
                responseMessage.Wait();

                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listFilmJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listFilmJA.Children<JObject>())
                    {
                        list4d.Add(o);
                    }
                    item.Add("typeC", JArray.FromObject(list4d));
                }

                /*item.type3 = db.Database.SqlQuery<ShowTime>($"exec GetList4DRoomFromFilm N'{cinemaname}',N'{item.MovieName}', N'2021-10-25'").ToList();*/

                foreach (var item1 in item.typeC)
                {
                    /*Call QuantityLeft API*/
                    List<JObject> left = new List<JObject>(9999);
                    HttpClient client10 = new HttpClient();
                    client10.BaseAddress = new Uri("http://localhost:8085/api/CinemaAPI/");
                    client10.DefaultRequestHeaders.Accept.Clear();
                    client10.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    responseMessage = client10.GetAsync("QuantityLeft?MovieName=" + name + "&RoomID=" + item1.roomid + "&ShowTime=" + item1.showtime);
                    responseMessage.Wait();

                    result = responseMessage.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        /*JArray listFilmJA = JArray.Parse(readTask.Result);
                        foreach (JObject o in listFilmJA.Children<JObject>())
                        {
                            left.Add(o);
                        }
                        item1.seat_available = left[0];*/
                        string str = readTask.Result;
                        str = str.Substring(1, str.Length - 2);
                        item1.seat_available = str;

                    }
                    /*item1.seat_available = db.Database.SqlQuery<int>($"exec QuantityLeft N'{item.MovieName}', '{item1.roomid}',N'{item1.showtime}'").ToList()[0];*/
                }
            }
            ViewBag.check = name;
            ViewBag.location = location;
            ViewBag.day = dayx;
            return View();
        }
    }
}