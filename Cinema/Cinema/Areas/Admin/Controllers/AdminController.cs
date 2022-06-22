using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
namespace Cinema.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Homepage()
        {
            //if (Session["AdminID"] != null)
            //{
                ViewBag.name = Session["AdminID"];

                List<JObject> locations = new List<JObject>();
                List<JObject> cinemas = new List<JObject>();
                List<JObject> rooms = new List<JObject>(9999);
                List<JObject> admins = new List<JObject>(9999);
                List<JObject> departments = new List<JObject>(9999);
                List<JObject> sobaidang = new List<JObject>(9999);
                List<JObject> soblog = new List<JObject>(9999);
                List<JObject> sobaibl = new List<JObject>(9999);
                List<JObject> sobaitin = new List<JObject>(9999);
                List<JObject> phanhoi = new List<JObject>(9999);
                List<JObject> dangchieu = new List<JObject>(9999);
                List<JObject> sapchieu = new List<JObject>(9999);
                List<JObject> phim = new List<JObject>(9999);
                List<JObject> payment = new List<JObject>(9999);
                List<JObject> ticketType = new List<JObject>(9999);
                List<JObject> service = new List<JObject>(9999);
                List<JObject> discount = new List<JObject>(9999);




                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/Admin/"); // ???
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetLOCATIONs"); // api  GET_CinemaLocation
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
                    ViewBag.locations = locations.Count;
                }

                var responseMessage2 = client.GetAsync("GetCinemaInfo");
                responseMessage2.Wait();
                result = responseMessage2.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        cinemas.Add(o);
                    }
                    ViewBag.cinemas = cinemas.Count();
                }

                var responseMessage3 = client.GetAsync("GetRoomInfo");
                responseMessage3.Wait();
                result = responseMessage3.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        rooms.Add(o);
                    }
                    ViewBag.rooms = rooms.Count;
                }

                responseMessage = client.GetAsync("GetAdminInfo");
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        admins.Add(o);
                    }
                    ViewBag.admins = admins.Count();
                }

                responseMessage = client.GetAsync("GetDepartment");
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        departments.Add(o);
                    }
                    ViewBag.departments = departments.Count;
                }

                responseMessage = client.GetAsync("GetPost");
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        sobaidang.Add(o);
                    }
                    ViewBag.sobaidang = sobaidang.Count;
                }

                responseMessage = client.GetAsync("GetAllBlog");
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        soblog.Add(o);
                    }
                    ViewBag.soblog = soblog.Count;
                }

                responseMessage = client.GetAsync("GetAllReview");
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        sobaibl.Add(o);
                    }
                    ViewBag.sobaibl = sobaibl.Count;
                }

                responseMessage = client.GetAsync("GetAllSale");
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        sobaitin.Add(o);
                    }
                    ViewBag.sobaitin = sobaitin.Count;
                }

                responseMessage = client.GetAsync("GetFeedBack");
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        phanhoi.Add(o);
                    }
                    ViewBag.phanhoi = phanhoi.Count;
                }

                responseMessage = client.GetAsync("GetCurrentFilm");
                result = responseMessage.Result;
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

                responseMessage = client.GetAsync("GetPayment");   
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        payment.Add(o);
                    }
                    ViewBag.payment = payment;
                }

                responseMessage = client.GetAsync("GetTicketTypes");
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        ticketType.Add(o);
                    }
                    ViewBag.ticketType = ticketType.Count;
                }

                responseMessage = client.GetAsync("GetServices");
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        service.Add(o);
                    }
                    ViewBag.service = service.Count;
                }

                responseMessage = client.GetAsync("GetDiscount");
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        discount.Add(o);
                    }
                    ViewBag.discount = discount.Count;
                }

                return View();
            }
        //    else
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }
           
        //}
    }
}