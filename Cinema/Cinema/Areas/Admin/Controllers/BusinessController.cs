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
    public class BusinessController : Controller
    {
        private readonly RestClient _client;
        public BusinessController()
        {
            _client = new RestClient("http://localhost:8085/Help");
        }
        // GET: Admin/Business
        public ActionResult Business()
        {
            //var ID = (Session["Role"].ToString());
            //if (ID == "4" || ID == "1")
            //{
                ViewBag.name = Session["AdminID"];

                List<JObject> payment = new List<JObject>(9999);
                List<JObject> ticketType = new List<JObject>(9999);
                List<JObject> service = new List<JObject>(9999);
                List<JObject> discount = new List<JObject>(9999);
                List<JObject> room = new List<JObject>(9999);
                List<JObject> bill = new List<JObject>(9999);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetTicketTypes"); // 
                responseMessage.Wait();
                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        ticketType.Add(o);
                    }
                    ViewBag.ticketType = ticketType;
                }
                //////////////////////
                responseMessage = client.GetAsync("GetServices"); // 
                responseMessage.Wait();
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
                    ViewBag.service = service;
                }

                responseMessage = client.GetAsync("GetDiscount");
                responseMessage.Wait();
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
                    ViewBag.discount = discount;
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
                        room.Add(o);
                    }
                    ViewBag.room = room;
                }

                responseMessage = client.GetAsync("GetBill");
                responseMessage.Wait();
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        bill.Add(o);
                    }
                    ViewBag.bill = bill;
                }

                responseMessage = client.GetAsync("GetBill");
                responseMessage.Wait();
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
                    ViewBag.payment = payment.Count;
                }

                return View();
            //}
            //else
            //    return RedirectToAction("Homepage", "Admin");

        }

        public ActionResult AddTicketTypeView()
        {
            //var admins = db.Database.SqlQuery<AdminInfo>("exec GetAdminInfo").ToList();
            //var departments = db.DEPARTMENTs.ToList();
            //ViewBag.admins = admins;
            //ViewBag.departments = departments;
            List<JObject> admins = new List<JObject>(9999);
            List<JObject> departments = new List<JObject>(9999);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetAdminInfo"); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    admins.Add(o);
                }
                ViewBag.admins = admins;
            }

            responseMessage = client.GetAsync("GetDepartments"); // 
            responseMessage.Wait();
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
                ViewBag.departments = departments;
            }

            return View();
        }
        [HttpPost]
        public ActionResult AddTicketType()
        {
            string ID = Request.Form["ticket_type1"];
            string name = Request.Form["tickettype_name"];
            string price = Request.Form["tickettype_price"];
            List<JObject> ticketType = new List<JObject>(9999);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Bussiness/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetTicketType??id=" + ID); /////////////////// truyen bien 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    ticketType.Add(o);
                }
                ViewBag.ticketType = ticketType;
            }

            

            if (ticketType.Count() > 0)
            {
                ViewBag.error = "Mã đã đã tồn tại";
                return View("~/Areas/Admin/Views/Business/AddTicketTypeView.cshtml");
            }
            else
            {
                //TICKET_TYPE x = new TICKET_TYPE();
                //x.TicketTypeID = ID;
                //x.TicketTypeName = name;
                //x.Price = price;
                //db.TICKET_TYPE.Add(x);
                //db.SaveChanges();
                //List<JObject> ticketType = new List<JObject>(9999);
                //HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri("http://localhost:8085/api/Bussiness/"); // ???
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(
                //    new MediaTypeWithQualityHeaderValue("application/json"));

                //var responseMessage = client.GetAsync("GetTicketType??id=" + ID); /////////////////// truyen bien 
                //responseMessage.Wait();
                //var result = responseMessage.Result;
                //if (result.IsSuccessStatusCode)
                //{
                //    var readTask = result.Content.ReadAsStringAsync();
                //    readTask.Wait();
                //    JArray listMovieJA = JArray.Parse(readTask.Result);
                //    foreach (JObject o in listMovieJA.Children<JObject>())
                //    {
                //        ticketType.Add(o);
                //    }
                //    ViewBag.ticketType = ticketType;
                //}
                return RedirectToAction("/Business");
            }

        }
        [HttpGet]
        public ActionResult EditTicketTypeView(string id)
        {
            //ViewBag.ticket = db.TICKET_TYPE.Find(id);
            //ViewBag.tickettypeID = id;

            List<JObject> ticket = new List<JObject>(9999);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Bussiness/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetTicketType??id=" + id); /////////////////// truyen bien 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    ticket.Add(o);
                }
                ViewBag.ticket = ticket;
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditTicketType(string id)
        {
            TICKET_TYPE t = new TICKET_TYPE();
            string name = Request.Form["tickettype_name"];
            string price = Request.Form["tickettype_price"];
            t.TicketTypeName = name;
            t.Price = price;
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:8085/api/Bussiness/"); // ???
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));

            //var responseMessage = client.GetAsync("PutTicketType??id=" + id, t); /////////////////// truyen bien 
            //responseMessage.Wait();
            //var result = responseMessage.Result;
            //if (result.IsSuccessStatusCode)
            //{
            //    var readTask = result.Content.ReadAsStringAsync();
            //    readTask.Wait();
            //}
            var request = new RestRequest($"api/Business/PutTICKET/{id}", Method.Put).AddObject(t);
            _client.Execute(request);
            return RedirectToAction("/Business");
        }
        public ActionResult DeleteTicketType(string id)
        {
            var request = new RestRequest($"api/Business/DeleteTICKET/{id}", Method.Delete);
            _client.Execute(request);
            return RedirectToAction("/Business");
        }

        public ActionResult AddServiceView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddService()
        {
            SERVICE s = new SERVICE();
            string ID = Request.Form["serviceID"];
            string name = Request.Form["service_name"];
            string price = Request.Form["service_price"];
            s.ServiceID = ID;
            s.ServiceName = name;
            s.ServicePrice = price;
            var request = new RestRequest($"api/Business/PostService/", Method.Post).AddObject(s);
            _client.Execute(request);
            return RedirectToAction("/Business");
        }
        [HttpGet]
        public ActionResult EditServiceView(string id)
        {

            List<JObject> service = new List<JObject>(9999);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Bussiness/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetService??id=" + id); /////////////////// truyen bien 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    service.Add(o);
                }
                ViewBag.service = service[0];
            }
            ViewBag.ServiceID = id;
            return View();
        }
        [HttpPost]
        public ActionResult EditService(string id)
        {
            SERVICE s = new SERVICE();
            string name = Request.Form["service_name"];
            string price = Request.Form["service_price"];
            s.ServiceName = name;
            s.ServicePrice = price;
            var request = new RestRequest($"api/Business/PutService/{id}", Method.Put).AddObject(s);
            _client.Execute(request);
            return RedirectToAction("/Business");
        }
        public ActionResult DeleteService(string id)
        {
            var request = new RestRequest($"api/Business/DeleteService/{id}", Method.Delete);
            _client.Execute(request);
            return RedirectToAction("/Business");
        }

        public ActionResult AddDiscountView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDiscount()
        {
            DISCOUNT_CODE d = new DISCOUNT_CODE();
            string ID = Request.Form["discount_ID"];
            string t = Request.Form["discount_t"];
            string stt = Request.Form["discount_stt"];
            d.CodeID = ID;
            d.State = int.Parse(t);
            d.DiscountNumber = int.Parse(stt);
            var request = new RestRequest($"api/Business/AddDiscount", Method.Post).AddObject(d);
            _client.Execute(request);
            return RedirectToAction("/Business");
        }

        
        [HttpGet]
        public ActionResult EditDiscountView(string id)
        {
            ViewBag.DiscountID = id;
            List<JObject> discount = new List<JObject>(9999);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Bussiness/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetDiscount??id=" + id);  
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    discount.Add(o);
                }
                ViewBag.discount = discount[0];
            }
            responseMessage = client.GetAsync("GetDiscount");
            responseMessage.Wait();
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
                ViewBag.discountTotal = discount;
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditDiscount(string id)
        {
            DISCOUNT_CODE d = new DISCOUNT_CODE();
            string t = Request.Form["discount_t"];
            string stt = Request.Form["discount_stt"];
            d.State = Int32.Parse(t);
            d.DiscountNumber = Int32.Parse(stt);
            var request = new RestRequest($"api/Business/PutDiscount/{id}", Method.Put).AddObject(d);
            _client.Execute(request);
            return RedirectToAction("/Business");
        }
        public ActionResult DeleteDiscount(string id)
        {
            var request = new RestRequest($"api/Business/DeleteDiscount/{id}", Method.Delete);
            _client.Execute(request);
            return RedirectToAction("/Business");
        }

    }
}