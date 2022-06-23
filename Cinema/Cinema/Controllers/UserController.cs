using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Cinema.Controllers
{
    public class UserController : Controller
    {

        public ActionResult Index()
        {
            return RedirectToRoute(new { controller = "HomePage", action = "HomePage" });
        }
        // GET: User
        [HttpGet]
        public ActionResult Register(string index)
        {
            ViewBag.index = index;
            return PartialView();

        }
        public ActionResult Login()
        {
            string name = Request.Form["user"];
            string pass = Request.Form["password"];
            return RedirectToAction("Index");
        }
        public ActionResult TestLogin(string tk, string mk)
        {
            string name = tk;
            string password = mk;

            List<JObject> listacc = new List<JObject>(9999);
            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri("http://localhost:8085/api/UserAPI/");
            client1.DefaultRequestHeaders.Accept.Clear();
            client1.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client1.GetAsync("CheckLogin?Username=" + name + "&Password=" + password);
            responseMessage.Wait();

            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listCurMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listCurMovieJA.Children<JObject>())
                {
                    listacc.Add(o);
                }
                ViewBag.listaccount = listacc;
            }
            Session["email"] = ViewBag.listaccount[0].email;
            Session["UserID"] = ViewBag.listaccount[0].UserID;

            ViewBag.error = "Login oke";
            ViewBag.index = 1;
            Session["name_user"] = ViewBag.listaccount[0].Username;
            return RedirectToRoute(new { controller = "HomePage", action = "HomePage" });
        }
        public JsonResult CheckLogin(string name, string password)
        {
            List<JObject> listacc = new List<JObject>(9999);
            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri("http://localhost:8085/api/UserAPI/");
            client1.DefaultRequestHeaders.Accept.Clear();
            client1.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client1.GetAsync("CheckLogin?Username=" + name + "&Password=" + password);
            responseMessage.Wait();

            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listCurMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listCurMovieJA.Children<JObject>())
                {
                    listacc.Add(o);
                }
                ViewBag.listacc = listacc;
            }
            if (listacc.Count > 0)
            {
                return Json(new
                {
                    redirectUrl = Url.Action("TestLogin", "User", new { tk = name, mk = password }),
                    isRedirect = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    isRedirect = "Đăng nhập thất bại, kiểm tra lại thông tin !!!"
                }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult CheckSignup(string name, string pass, string repass, string email)
        {
            JObject _user = new JObject();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/UserAPI/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("CheckID");
            responseMessage.Wait();

            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                ViewBag.detail = JObject.Parse(readTask.Result);
            }
            string a = ViewBag.detail.UserID;
            string b = a.Substring(4, 4);
            string c = "";
            int _id = Int32.Parse(b);
            _user.Add("email", email);
            if (_id < 10)
            {
                _id += 1;
                c = string.Concat("USER000", _id.ToString());
                _user.Add("UserID", string.Concat("USER000", _id.ToString()));

            }
            else if (_id < 100 && _id >= 10)
            {
                _id += 1;
                c = string.Concat("USER00", _id.ToString());
                _user.Add("UserID", string.Concat("USER00", _id.ToString()));
            }
            else if (_id < 1000 && _id >= 100)
            {
                _id += 1;
                c = string.Concat("USER0", _id.ToString());
                _user.Add("UserID", string.Concat("USER0", _id.ToString()));
            }
            else if (_id < 10000 && _id >= 1000)
            {
                _id += 1;
                c = string.Concat("USER", _id.ToString());
                _user.Add("UserID", string.Concat("USER", _id.ToString()));
            }

            List<JObject> listMovie = new List<JObject>(9999);
            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri("http://localhost:8085/api/UserAPI/");
            client1.DefaultRequestHeaders.Accept.Clear();
            client1.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            responseMessage = client1.GetAsync("CheckDataUserAccount?email=" + email + "&Username=" + c + "&UserID=" + name);
            responseMessage.Wait();

            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                if (pass == repass && pass != "")
                {
                    _user.Add("UserPassword", GetMD5(pass)); //
                    _user.Add("Username", name);
                    HttpClient client2 = new HttpClient();
                    client2.BaseAddress = new Uri("http://localhost:8085/api/UserAPI/");
                    client2.DefaultRequestHeaders.Accept.Clear();
                    client2.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    responseMessage = client2.PostAsync("AddUser", new StringContent(_user.ToString(), Encoding.UTF8, "application/json"));
                    responseMessage.Wait();
                    return
                    Json(new
                    {
                        data = 1,
                        msg = "Đăng ký thành công !!"
                    }, JsonRequestBehavior.AllowGet);
                }
                return
                     Json(new
                     {
                         data = 2,
                         msg = "Đăng ký thất bại !! Kiểm tra lại thông tin"
                     }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return
                    Json(new
                    {
                        data = 2,
                        msg = "Đăng ký thất bại !! Kiểm tra lại thông tin"
                    }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Logout()
        {
            Session["email"] = null;
            Session["UserID"] = null;
            return RedirectToRoute(new { controller = "HomePage", action = "HomePage" });
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }
        public ActionResult UserInfo(string id)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/UserAPI/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("FindUserByID?id=" + id);
            responseMessage.Wait();

            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                ViewBag.user = JObject.Parse(readTask.Result);
            }
            List<JObject> listbill = new List<JObject>(9999);
            HttpClient client4 = new HttpClient();
            client4.BaseAddress = new Uri("http://localhost:8085/api/UserAPI/");
            client4.DefaultRequestHeaders.Accept.Clear();
            client4.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            responseMessage = client4.GetAsync("GetListBillFromUser?UserID=" + id);
            responseMessage.Wait();

            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listFilmJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listFilmJA.Children<JObject>())
                {
                    listbill.Add(o);
                }
                ViewBag.bill = listbill;
            }
            /*List<Bill_Info> result = _db.Database.SqlQuery<Bill_Info>($"exec GetListBillFromUser N'{user.UserID}'").ToList();*/
            foreach (var item in ViewBag.bill)
            {

                List<JObject> listtickettype = new List<JObject>(9999);
                HttpClient client5 = new HttpClient();
                client5.BaseAddress = new Uri("http://localhost:8085/api/UserAPI/");
                client5.DefaultRequestHeaders.Accept.Clear();
                client5.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                responseMessage = client5.GetAsync("GetTicketsFromTicketSession?TicketSession=" + item.GetValue("TicketSession"));
                responseMessage.Wait();

                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listFilmJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listFilmJA.Children<JObject>())
                    {
                        listtickettype.Add(o);
                    }
                    /*item.Add("TicketType", listtickettype);*/
                    item["TicketType"] = new JObject(listtickettype);
                    /*item.Add("TicketType", new JObject(listtickettype));*/
                }

                List<JObject> listservice = new List<JObject>(9999);
                HttpClient client6 = new HttpClient();
                client6.BaseAddress = new Uri("http://localhost:8085/api/UserAPI/");
                client6.DefaultRequestHeaders.Accept.Clear();
                client6.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                responseMessage = client6.GetAsync("GetListBillFromUser?ServiceSession=" + item.GetValue("ServiceSession"));
                responseMessage.Wait();

                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listFilmJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listFilmJA.Children<JObject>())
                    {
                        listservice.Add(o);
                    }
                    /*item.Add("TicketType", listtickettype);*/
                    item["Service"] = new JObject(listservice);
                    /*item.Add("Service", new JObject(listservice));*/
                }

                List<JObject> listdate = new List<JObject>(9999);
                HttpClient client7 = new HttpClient();
                client7.BaseAddress = new Uri("http://localhost:8085/api/UserAPI/");
                client7.DefaultRequestHeaders.Accept.Clear();
                client7.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                responseMessage = client7.GetAsync("GetInfoBillFromTicketSession?TicketSession=" + item.GetValue("TicketSession"));
                responseMessage.Wait();

                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listFilmJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listFilmJA.Children<JObject>())
                    {
                        listdate.Add(o);
                    }
                    /*item.Add("TicketType", listtickettype);*/
                    item["data"] = new JObject(listdate);
                    /*item.Add("data", new JObject(listdate));*/
                }

                /*item.Add("PayDay", item.GetValue("PayDay"));

                item.TicketType = _db.Database.SqlQuery<TICKET_2>($"exec GetTicketsFromTicketSession N'{item.TicketSession}'").ToList();
                item.Service = _db.Database.SqlQuery<SERVICE_TO_CASH>($"exec GetServicesFromServiceSession N'{item.ServiceSession}'").ToList();
                item.data = _db.Database.SqlQuery<Bill_data>($"exec GetInfoBillFromTicketSession N'{item.TicketSession}'").ToList()[0];
                item.PayDay = item.PayDay.Value.Date;*/
            }
            return View();
        }
    }
}