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
    public class CinemaController : Controller
    {
        // GET: Cinema
        public ActionResult Cinema(string location, string cinemaname, string change)
        {

            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }

            /*Call GetLocationInfo API*/
            List<JObject> listLocation = new List<JObject>(9999);
            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri("http://localhost:8085/api/PublicAPI/");
            client1.DefaultRequestHeaders.Accept.Clear();
            client1.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client1.GetAsync("GetLocationInfo");
            responseMessage.Wait();

            var result = responseMessage.Result;
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

            if (location != "none")
            {
                /*Call GetListCinemaFromLocation API*/
                List<JObject> listCinema = new List<JObject>(9999);
                HttpClient client2 = new HttpClient();
                client2.BaseAddress = new Uri("http://localhost:8085/api/PublicAPI/");
                client2.DefaultRequestHeaders.Accept.Clear();
                client2.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                responseMessage = client2.GetAsync("GetListCinemaFromLocation?location=" + location);
                responseMessage.Wait();

                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listCinemaJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listCinemaJA.Children<JObject>())
                    {
                        listCinema.Add(o);
                    }
                    ViewBag.listcinema = listCinema;
                }

                if (cinemaname != "none")
                {
                    /*Call GetListCinemaFromName API*/
                    List<JObject> listCinemaByName = new List<JObject>(9999);
                    HttpClient client3 = new HttpClient();
                    client3.BaseAddress = new Uri("http://localhost:8085/api/CinemaAPI/");
                    client3.DefaultRequestHeaders.Accept.Clear();
                    client3.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    responseMessage = client3.GetAsync("GetListCinemaFromName?cinema=" + cinemaname);
                    responseMessage.Wait();

                    result = responseMessage.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        JArray listCinemaJA = JArray.Parse(readTask.Result);
                        foreach (JObject o in listCinemaJA.Children<JObject>())
                        {
                            listCinemaByName.Add(o);
                        }
                        ViewBag.cinemax = listCinemaByName[0];
                    }

                    /*result = db.Database.SqlQuery<CINEMA>($"exec GetListCinemaFromName N'{cinemaname}'").ToList();
                    ViewBag.cinemax = result[0];*/

                    /*Call GetListFilmFromCinema API*/
                    List<JObject> listFilmByCinema = new List<JObject>(9999);
                    HttpClient client4 = new HttpClient();
                    client4.BaseAddress = new Uri("http://localhost:8085/api/CinemaAPI/");
                    client4.DefaultRequestHeaders.Accept.Clear();
                    client4.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    responseMessage = client4.GetAsync("GetListFilmFromCinema?cinema=" + cinemaname + "&daytime=2021-10-25");
                    responseMessage.Wait();

                    result = responseMessage.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        JArray listFilmJA = JArray.Parse(readTask.Result);
                        foreach (JObject o in listFilmJA.Children<JObject>())
                        {
                            listFilmByCinema.Add(o);
                        }
                        ViewBag.listmovie = listFilmByCinema;
                    }

                    /*ViewBag.listmovie = db.Database.SqlQuery<MovieItem>($"exec GetListFilmFromCinema N'{cinemaname}','2021-10-25'").ToList();*/

                    foreach (var item in ViewBag.listmovie)
                    {
                        /*Call GetList2DRoomFromFilm API*/
                        List<JObject> list2d = new List<JObject>(9999);
                        HttpClient client5 = new HttpClient();
                        client5.BaseAddress = new Uri("http://localhost:8085/api/CinemaAPI/");
                        client5.DefaultRequestHeaders.Accept.Clear();
                        client5.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        responseMessage = client5.GetAsync("GetList2DRoomFromFilm?cinema=" + cinemaname + "&movie=" + item.MovieName + "&daytime=2021-10-25");
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
                            item.type1 = list2d.ToArray();
                        }

                        /*item.type1 = db.Database.SqlQuery<ShowTime>($"exec GetList2DRoomFromFilm N'{cinemaname}',N'{item.MovieName}', N'2021-10-25'").ToList();*/

                        foreach (var item1 in item.type1)
                        {
                            /*Call QuantityLeft API*/
                            List<JObject> left = new List<JObject>(9999);
                            HttpClient client6 = new HttpClient();
                            client6.BaseAddress = new Uri("http://localhost:8085/api/CinemaAPI/");
                            client6.DefaultRequestHeaders.Accept.Clear();
                            client6.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));

                            responseMessage = client6.GetAsync("QuantityLeft?movie=" + item.MovieName + "&room=" + item1.roomid + "&showtime=" + item1.showtime);
                            responseMessage.Wait();

                            result = responseMessage.Result;
                            if (result.IsSuccessStatusCode)
                            {
                                var readTask = result.Content.ReadAsStringAsync();
                                readTask.Wait();
                                JArray listFilmJA = JArray.Parse(readTask.Result);
                                foreach (JObject o in listFilmJA.Children<JObject>())
                                {
                                    left.Add(o);
                                }
                                item1.seat_available = left[0];
                            }
                            /*item1.seat_available = db.Database.SqlQuery<int>($"exec QuantityLeft N'{item.MovieName}', '{item1.roomid}',N'{item1.showtime}'").ToList()[0];*/
                        }

                        /*Call GetListFilmFromCinema API*/
                        List<JObject> list3d = new List<JObject>(9999);
                        HttpClient client7 = new HttpClient();
                        client7.BaseAddress = new Uri("http://localhost:8085/api/CinemaAPI/");
                        client7.DefaultRequestHeaders.Accept.Clear();
                        client7.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        responseMessage = client7.GetAsync("GetList3DRoomFromFilm?cinema=" + cinemaname + "&movie=" + item.MovieName + "&daytime=2021-10-25");
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
                            item.type2 = list3d;
                        }

                        /*item.type2 = db.Database.SqlQuery<ShowTime>($"exec GetList3DRoomFromFilm N'{cinemaname}',N'{item.MovieName}', N'2021-10-25'").ToList();*/
                        
                        foreach (var item1 in item.type2)
                        {
                            /*Call QuantityLeft API*/
                            List<JObject> left = new List<JObject>(9999);
                            HttpClient client8 = new HttpClient();
                            client8.BaseAddress = new Uri("http://localhost:8085/api/CinemaAPI/");
                            client8.DefaultRequestHeaders.Accept.Clear();
                            client8.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));

                            responseMessage = client8.GetAsync("QuantityLeft?movie=" + item.MovieName + "&room=" + item1.roomid + "&showtime=" + item1.showtime);
                            responseMessage.Wait();

                            result = responseMessage.Result;
                            if (result.IsSuccessStatusCode)
                            {
                                var readTask = result.Content.ReadAsStringAsync();
                                readTask.Wait();
                                JArray listFilmJA = JArray.Parse(readTask.Result);
                                foreach (JObject o in listFilmJA.Children<JObject>())
                                {
                                    left.Add(o);
                                }
                                item1.seat_available = left[0];
                            }
                            /*item1.seat_available = db.Database.SqlQuery<int>($"exec QuantityLeft N'{item.MovieName}', '{item1.roomid}',N'{item1.showtime}'").ToList()[0];*/
                        }

                        /*Call GetListFilmFromCinema API*/
                        List<JObject> list4d = new List<JObject>(9999);
                        HttpClient client9 = new HttpClient();
                        client9.BaseAddress = new Uri("http://localhost:8085/api/CinemaAPI/");
                        client9.DefaultRequestHeaders.Accept.Clear();
                        client9.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        responseMessage = client9.GetAsync("GetList4DRoomFromFilm?cinema=" + cinemaname + "&movie=" + item.MovieName + "&daytime=2021-10-25");
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
                            item.type3 = list4d;
                        }

                        /*item.type3 = db.Database.SqlQuery<ShowTime>($"exec GetList4DRoomFromFilm N'{cinemaname}',N'{item.MovieName}', N'2021-10-25'").ToList();*/
                        
                        foreach (var item1 in item.type3)
                        {
                            /*Call QuantityLeft API*/
                            List<JObject> left = new List<JObject>(9999);
                            HttpClient client10 = new HttpClient();
                            client10.BaseAddress = new Uri("http://localhost:8085/api/CinemaAPI/");
                            client10.DefaultRequestHeaders.Accept.Clear();
                            client10.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));

                            responseMessage = client10.GetAsync("QuantityLeft?movie=" + item.MovieName + "&room=" + item1.roomid + "&showtime=" + item1.showtime);
                            responseMessage.Wait();

                            result = responseMessage.Result;
                            if (result.IsSuccessStatusCode)
                            {
                                var readTask = result.Content.ReadAsStringAsync();
                                readTask.Wait();
                                JArray listFilmJA = JArray.Parse(readTask.Result);
                                foreach (JObject o in listFilmJA.Children<JObject>())
                                {
                                    left.Add(o);
                                }
                                item1.seat_available = left[0];
                            }
                            /*item1.seat_available = db.Database.SqlQuery<int>($"exec QuantityLeft N'{item.MovieName}', '{item1.roomid}',N'{item1.showtime}'").ToList()[0];*/
                        }
                    }
                    /*Call QuantityLeft API*/
                    List<JObject> images = new List<JObject>(9999);
                    HttpClient client11 = new HttpClient();
                    client11.BaseAddress = new Uri("http://localhost:8085/api/CinemaAPI/");
                    client11.DefaultRequestHeaders.Accept.Clear();
                    client11.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    responseMessage = client11.GetAsync("GetListImgFromCinema?cinema=" + cinemaname);
                    responseMessage.Wait();

                    result = responseMessage.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        JArray listFilmJA = JArray.Parse(readTask.Result);
                        foreach (JObject o in listFilmJA.Children<JObject>())
                        {
                            images.Add(o);
                        }
                        ViewBag.cinemaimg = images;
                    }
                    /*ViewBag.cinemaimg = db.Database.SqlQuery<CINEMA_IMAGE>($"exec GetListImgFromCinema N'{cinemaname}'").ToList();*/
                }
            }
            ViewBag.checklocation = location;
            ViewBag.checkcinema = cinemaname;
            ViewBag.change = change;
            return View();
        }
    }
}