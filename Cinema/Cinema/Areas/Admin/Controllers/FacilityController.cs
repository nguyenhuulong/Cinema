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
    public class FacilityController : Controller
    {
        private readonly RestClient _client;
        public FacilityController()
        {
            _client = new RestClient("http://localhost:8085/Help");
        }
        // GET: Admin/Facility
        public ActionResult Facility()
        {
            var ID = (Session["Role"]).ToString();
            if (ID == "1")
            {
                ViewBag.name = Session["AdminID"];

                List<JObject> locations = new List<JObject>(9999);
                List<JObject> cinemas = new List<JObject>(9999);
                List<JObject> rooms = new List<JObject>(9999);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetLocation"); // 
                responseMessage.Wait();
                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        locations.Add(o);
                    }
                    ViewBag.locations = locations;
                }
                //////////////////////
                responseMessage = client.GetAsync("GetCinemaInfo"); // 
                responseMessage.Wait();
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        cinemas.Add(o);
                    }
                    ViewBag.cimemas = cinemas;
                }

                responseMessage = client.GetAsync("GetRoomInfo");
                responseMessage.Wait();
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        rooms.Add(o);
                    }
                    ViewBag.rooms = rooms;
                }
                return View();
            }
            else
                return RedirectToAction("Homepage", "Admin");
        }
        public ActionResult AddCinemaView()
        {
            List<JObject> locations = new List<JObject>(9999);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetLocation"); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    locations.Add(o);
                }
                ViewBag.locations = locations;
            }
            return View();
        }
        public ActionResult AddCinema()
        {
            List<JObject> locations = new List<JObject>(9999);
            List<JObject> cinemas = new List<JObject>(9999);
            List<JObject> max1 = new List<JObject>(9999);


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetLocation"); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    locations.Add(o);
                }
                ViewBag.locations = locations;
            }
 
            responseMessage = client.GetAsync("GetCinema"); // 
            responseMessage.Wait();
            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    cinemas.Add(o);
                }
                ViewBag.cinemas = cinemas;
            }
            string cinemaName = Request.Form["cinema-name"];
            string locationId = Request.Form["location-id"];
            string cinemaAddress = Request.Form["cinema-address"];
            string cinemaNumber = Request.Form["cinema-number"];
            
            responseMessage = client.GetAsync("GetMaxCinemaID"); // 
            responseMessage.Wait();
            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    max1.Add(o);
                }
                ViewBag.max1 = max1[0];
                
            }
            string max = ViewBag.max1.ElementAt(0);
            int intMax = Int32.Parse(ViewBag.max.Substring(3, max.Length - 3)) + 1;
            string sign = "CNM";
            for (int i = 1; i <= (max.Length - (Int32.Parse(max.Substring(3, max.Length - 3))).ToString().Length) - 3; i++)
            {
                sign += "0";
            }
            string newCinemaId = sign + intMax.ToString();
            foreach (var cinema in cinemas)
            {
                if (cinemaName == cinema.) ////  khong biet xử lí với Jobject
                {
                    ViewBag.error = "Tên rạp đã tồn tại";
                    return View("~/Areas/Admin/Views/Facility/AddCinemaView.cshtml");
                }
            }
            CINEMA newCinema = new CINEMA();
            newCinema.CinemaID = newCinemaId;
            newCinema.CinemaName = cinemaName;
            newCinema.LocationID = locationId;
            newCinema.CinemaAddress = cinemaAddress;
            newCinema.PhoneNumber = cinemaNumber;
            var request = new RestRequest($"api/Facility/AddCinema", Method.Post).AddObject(newCinema);
            _client.Execute(request);
            return RedirectToAction("/Facility");
        }
        public ActionResult EditCinemaView(string cinemaId)
        {
            List<JObject> locations = new List<JObject>(9999);
            List<JObject> cinema = new List<JObject>(9999);


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetLocation"); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    locations.Add(o);
                }
                ViewBag.locations = locations;
            }

            responseMessage = client.GetAsync("FindCinema?id=" + cinemaId); // 
            responseMessage.Wait();
            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    cinema.Add(o);
                }
                ViewBag.cinema = cinema;
            }
            ViewBag.cinemaId = cinemaId;
            return View();
        }

        public ActionResult EditCinema(string cinemaId)
        {
            List<JObject> cinemas = new List<JObject>(9999);
 
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetCinema"); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    cinemas.Add(o);
                }
                ViewBag.cinemas = cinemas;
            }
            string cinemaName = Request.Form["cinema-name"];
            string locationId = Request.Form["location-id"];
            string cinemaAddress = Request.Form["cinema-address"];
            string cinemaNumber = Request.Form["cinema-number"];
            foreach (var cinema in cinemas)
            {
                if (cinemaName == cinema.CinemaName)
                {
                    ViewBag.error = "Tên rạp đã tồn tại";
                    return View("~/Areas/Admin/Views/Facility/EditCinemaView.cshtml");
                }
            }
            ViewBag.error = null;
            CINEMA c = new CINEMA();
            c.CinemaName = cinemaName;
            c.LocationID = locationId;
            c.CinemaAddress = cinemaAddress;
            c.PhoneNumber = cinemaNumber;
            var request = new RestRequest($"api/Facility/EditCinema/{cinemaId}", Method.Put).AddObject(c);
            _client.Execute(request);
            return RedirectToAction("/Facility");
        }
        public ActionResult DeleteCinema(string cinemaId)
        {
            var request = new RestRequest($"api/Facility/DeleteCinema/{cinemaId}", Method.Delete);
            _client.Execute(request);
            return RedirectToAction("/Facility");
        }
        public ActionResult AddLocationView()
        {
            List<JObject> locations = new List<JObject>(9999);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetLocation"); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    locations.Add(o);
                }
                ViewBag.locations = locations;
            }
            return View();
        }
        public ActionResult AddLocation()
        {
            List<JObject> locations = new List<JObject>(9999);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetLocation"); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    locations.Add(o);
                }
                ViewBag.locations = locations;
            }
            string locationName = Request.Form["location-name"];
            List<JObject> max1 = new List<JObject>(9999);

            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client1.DefaultRequestHeaders.Accept.Clear();
            client1.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            responseMessage = client.GetAsync("GetMaxLocationId"); // 
            responseMessage.Wait();
            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    max1.Add(o);
                }
                ViewBag.max1 = max1;
            }
            string max = max1.ToString();
            int intMax = Int32.Parse(max.Substring(2, max.Length - 2)) + 1;
            string sign = "LC";
            for (int i = 1; i <= (max.Length - (Int32.Parse(max.Substring(2, max.Length - 2))).ToString().Length) - 2; i++)
            {
                sign += "0";
            }
            string newLocationId = sign + intMax.ToString();
            foreach (var location in locations)
            {
                if (locationName == location.LocationName)
                {
                    ViewBag.error = "Tên khu vực đã tồn tại";
                    return View("~/Areas/Admin/Views/Facility/AddLocationView.cshtml");
                }
            }
            CINEMA_LOCATION newLocation = new CINEMA_LOCATION();
            newLocation.LocationID = newLocationId;
            newLocation.LocationName = locationName;
            var request = new RestRequest($"api/Facility/AddLocation", Method.Post).AddObject(newLocation);
            _client.Execute(request);
            return RedirectToAction("/Facility");

        }
        public ActionResult EditLocationView(string locationId)
        {
            List<JObject> locations = new List<JObject>(9999);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("FindLocation?id=" + locationId); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    locations.Add(o);
                }
                ViewBag.locations = locations;
            }
            ViewBag.locationId = locationId;
            return View();
        }

        public ActionResult EditLocation(string locationId)
        {
            var locations = db.CINEMA_LOCATION.ToList();
            ViewBag.locations = locations;
            string locationName = Request.Form["location-name"];
            CINEMA_LOCATION editedLocation = db.CINEMA_LOCATION.Find(locationId);
            editedLocation.LocationName = locationName;
            db.SaveChanges();
            return RedirectToAction("/Facility");
        }
        public ActionResult DeleteLocation(string locationId)
        {
            var request = new RestRequest($"api/Facility/DeleteLocation/{locationId}", Method.Delete);
            _client.Execute(request);
            return RedirectToAction("/Facility");

        }
        public ActionResult AddRoomView()
        {

            List<JObject> cinemas = new List<JObject>(9999);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetCinema"); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    cinemas.Add(o);
                }
                ViewBag.cinemas = cinemas;
            }
            return View();
        }
        public ActionResult AddRoom()
        {
            var rooms = db.ROOMs.ToList();
            string roomName = Request.Form["room-name"];
            string cinemaId = Request.Form["cinema-id"];
            string screenType = Request.Form["room-screentype"];
            string max = db.Database.SqlQuery<String>("exec GetMaxRoomId").ToList()[0];
            int intMax = Int32.Parse(max.Substring(1, max.Length - 1)) + 1;
            string sign = "R";
            for (int i = 1; i <= (max.Length - (Int32.Parse(max.Substring(1, max.Length - 1))).ToString().Length) - 1; i++)
            {
                sign += "0";
            }
            string newRoomId = sign + intMax.ToString();
            foreach (var room in rooms)
            {
                if (roomName == room.RoomName && cinemaId == room.CinemaID)
                {
                    ViewBag.error = "Tên phòng đã tồn tại";
                    return View("~/Areas/Admin/Views/Facility/AddRoomView.cshtml");
                }
            }
            ROOM newRoom = new ROOM();
            newRoom.RoomID = newRoomId;
            newRoom.CinemaID = cinemaId;
            newRoom.RoomName = roomName;
            newRoom.ScreenType = screenType;
            db.ROOMs.Add(newRoom);
            db.SaveChanges();
            return RedirectToAction("/Facility");
        }
        public ActionResult EditRoomView(string roomId)
        {
            List<JObject> cinemas = new List<JObject>(9999);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetCinema"); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    cinemas.Add(o);
                }
                ViewBag.cinemas = cinemas;
            }
            List<JObject> rooms = new List<JObject>(9999);

            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client1.DefaultRequestHeaders.Accept.Clear();
            client1.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage1 = client.GetAsync("FindRoom?id" + roomId); // 
            responseMessage.Wait();
            var result1 = responseMessage.Result;
            if (result1.IsSuccessStatusCode)
            {
                var readTask = result1.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    rooms.Add(o);
                }
                ViewBag.rooms = rooms;
            }
            ViewBag.roomId = roomId;
            return View();
        }

        public ActionResult EditRoom(string roomId)
        {
            var rooms = db.ROOMs.ToList();
            string screenType = Request.Form["room-screentype"];
            string roomName = Request.Form["room-name"];
            string cinemaId = Request.Form["cinema-id"];
            ROOM editedRoom = db.ROOMs.Find(roomId);
            editedRoom.RoomName = roomName;
            editedRoom.CinemaID = cinemaId;
            editedRoom.ScreenType = screenType;
            db.SaveChanges();
            return RedirectToAction("/Facility");
        }
        public ActionResult DeleteRoom(string roomId)
        {
            var request = new RestRequest($"api/Facility/DeleteRoom/{roomId}", Method.Delete);
            _client.Execute(request);
            return RedirectToAction("/Facility");
        }
    }
}