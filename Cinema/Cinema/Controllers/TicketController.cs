using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Controllers
{
    public class TicketController : Controller
    {
        public static string CinemaAddress, CinemaName, MovieName, DateTime1, ShowTime, ScreenType, Tickets, Image, Room, MVT;
        public static int Money;
        public static int TKT1 = 0, TKT2 = 0, TKT3 = 0, TKT4 = 0, SV1 = 0, SV2 = 0, SV3 = 0, SV4 = 0, SV5 = 0, SV6 = 0;
        public static JObject voucher = new JObject();
        /*public static DISCOUNT_CODE voucher = new DISCOUNT_CODE();*/
        public static List<string> arr_ticket, seat_arr, all_sv;
        public static JArray tk = new JArray();
        /*public static List<TICKET_TYPE> tk = new List<TICKET_TYPE>();*/
        public static JArray sv = new JArray();
        /*public static List<SERVICE_TO_CASH> sv = new List<SERVICE_TO_CASH>();*/

        public ActionResult SelectTicket(string mvtid, string cinemaname, string datetime, string showtime, string room, string screentype, string filmname)
        {
            if (Session["emnail"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            MVT = mvtid;

            /*Call GetCinemaFromName API*/
            List<JObject> listcinema = new List<JObject>(9999);
            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
            client1.DefaultRequestHeaders.Accept.Clear();
            client1.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client1.GetAsync("GetCinemaFromName?cinemaname=" + cinemaname);
            responseMessage.Wait();

            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listJA.Children<JObject>())
                {
                    listcinema.Add(o);
                }
                ViewBag.temp1 = listcinema;
            }

            var cinemaaddress = ViewBag.temp1[0];
            /*var cinemaaddress = db.Database.SqlQuery<CINEMA>($"exec GetCinemaFromName N'{cinemaname}'").ToList()[0];*/
            ViewBag.CinemaAddress = cinemaaddress.CinemaAddress;
            ViewBag.CinemaName = cinemaname;
            ViewBag.FilmName = filmname;
            ViewBag.showtime = showtime;
            ViewBag.datetime = datetime;
            ViewBag.screentype = screentype;


            /*Call GetFilmFromName API*/
            List<JObject> listfilm = new List<JObject>(9999);
            HttpClient client2 = new HttpClient();
            client2.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            responseMessage = client2.GetAsync("GetFilmFromName?filmname=" + filmname);
            responseMessage.Wait();

            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listJA.Children<JObject>())
                {
                    listfilm.Add(o);
                }
                ViewBag.temp2 = listfilm;
            }

            var img = ViewBag.temp2;
            /*var img = db.Database.SqlQuery<MOVIE>($"exec GetFilmFromName N'{filmname}'").ToList();*/


            /*Call GetRoomFromID API*/
            List<JObject> listroom = new List<JObject>(9999);
            HttpClient client3 = new HttpClient();
            client3.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
            client3.DefaultRequestHeaders.Accept.Clear();
            client3.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            responseMessage = client3.GetAsync("GetRoomFromID?ID=" + room);
            responseMessage.Wait();

            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listJA.Children<JObject>())
                {
                    listroom.Add(o);
                }
                ViewBag.temp3 = listroom;
            }

            /*ROOM room1 = db.ROOMs.Find(room);*/

            Room = ViewBag.temp3[0].RoomName;
            ViewBag.room = Room;

            Image = img[0].PosterLink;
            ViewBag.Image = Image;
            CinemaAddress = cinemaaddress.CinemaAddress;
            CinemaName = cinemaname;
            MovieName = filmname;
            ShowTime = showtime;
            DateTime1 = datetime;
            ScreenType = screentype;

            /*Call GetAllTicketType API*/
            List<JObject> listtype = new List<JObject>(9999);
            HttpClient client4 = new HttpClient();
            client4.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
            client4.DefaultRequestHeaders.Accept.Clear();
            client4.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            responseMessage = client4.GetAsync("GetAllTicketType");
            responseMessage.Wait();

            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listJA.Children<JObject>())
                {
                    listtype.Add(o);
                }
                ViewBag.tickettype = listtype;
            }
            return PartialView();
        }
        public ActionResult SelectSeat(List<string> data)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            ViewBag.CinemaAddress = CinemaAddress;
            ViewBag.CinemaName = CinemaName;
            ViewBag.FilmName = MovieName;
            ViewBag.showtime = ShowTime;
            ViewBag.screentype = ScreenType;
            ViewBag.Image = Image;
            ViewBag.room = Room;
            ViewBag.datetime = DateTime1;
            ViewBag.TicketCount = data.Count;
            arr_ticket = data;
            foreach (var item in data)
            {
                if (item == "TKT1")
                {
                    TKT1++;
                    Money += 80000;
                }
                if (item == "TKT2")
                {
                    TKT2++;
                    Money += 60000;
                }
                if (item == "TKT3")
                {
                    TKT3++;
                    Money += 40000;
                }
                if (item == "TKT4")
                {
                    TKT4++;
                    Money += 200000;
                }
            }

            /*Call GetListSeatBooked API*/
            List<JObject> listseat = new List<JObject>(9999);
            HttpClient client4 = new HttpClient();
            client4.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
            client4.DefaultRequestHeaders.Accept.Clear();
            client4.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client4.GetAsync("GetListSeatBooked?MovieName=" + MovieName + "&Room=" + Room + "&ShowTime=" + ShowTime);
            responseMessage.Wait();

            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listJA.Children<JObject>())
                {
                    listseat.Add(o);
                }
                ViewBag.tickettype = listseat;
            }
            ViewBag.ListSeatBooked = listseat;

            /*ViewBag.ListSeatBooked = db.Database.SqlQuery<string>($"exec GetListSeatBooked N'{MovieName}', '{Room}', '{ShowTime}', N'2021-10-25'").ToList();*/
            return PartialView();
        }
        public ActionResult SelectService(List<string> data)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            ViewBag.CinemaAddress = CinemaAddress;
            ViewBag.CinemaName = CinemaName;
            ViewBag.FilmName = MovieName;
            ViewBag.showtime = ShowTime;
            ViewBag.screentype = ScreenType;
            ViewBag.Image = Image;
            ViewBag.datetime = DateTime1;
            ViewBag.room = Room;

            /*Call GetAllTicketType API*/
            List<JObject> listservice = new List<JObject>(9999);
            HttpClient client4 = new HttpClient();
            client4.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
            client4.DefaultRequestHeaders.Accept.Clear();
            client4.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client4.GetAsync("GetListService");
            responseMessage.Wait();

            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listJA.Children<JObject>())
                {
                    listservice.Add(o);
                }
                ViewBag.service = listservice;
            }

            /*ViewBag.service = db.SERVICEs.ToList();*/
            ViewBag.count = 0;
            seat_arr = data;

            return PartialView();
        }
        public ActionResult Payment(List<string> data)
        {
            if (Session["email"] != null)
            {
                ViewBag.index = 1;
                ViewBag.name = Session["name_user"].ToString();
                ViewBag.userid = Session["UserID"].ToString();
            }
            ViewBag.CinemaAddress = CinemaAddress;
            ViewBag.CinemaName = CinemaName;
            ViewBag.FilmName = MovieName;
            ViewBag.showtime = ShowTime;
            ViewBag.datetime = DateTime1;
            ViewBag.screentype = ScreenType;
            ViewBag.Image = Image;
            ViewBag.room = Room;
            all_sv = data;
            if (TKT1 != 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetTicketType?id=TKT1");
                responseMessage.Wait();

                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    ViewBag.tempA = JObject.Parse(readTask.Result);
                }

                /*TICKET_TYPE result = db.TICKET_TYPE.Find("TKT1");*/
                var res = ViewBag.tempA;
                res["TicketNum"] = TKT1;
                tk.Add(res);
            }
            if (TKT2 != 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetTicketType?id=TKT2");
                responseMessage.Wait();

                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    ViewBag.tempB = JObject.Parse(readTask.Result);
                }

                /*TICKET_TYPE result = db.TICKET_TYPE.Find("TKT1");*/
                var res = ViewBag.tempB;
                res["TicketNum"] = TKT2;
                tk.Add(res);

            }
            if (TKT3 != 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetTicketType?id=TKT3");
                responseMessage.Wait();

                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    ViewBag.tempC = JObject.Parse(readTask.Result);
                }

                /*TICKET_TYPE result = db.TICKET_TYPE.Find("TKT1");*/
                var res = ViewBag.tempC;
                res["TicketNum"] = TKT3;
                tk.Add(res);
            }
            if (TKT4 != 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetTicketType?id=TKT4");
                responseMessage.Wait();

                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    ViewBag.tempD = JObject.Parse(readTask.Result);
                }

                /*TICKET_TYPE result = db.TICKET_TYPE.Find("TKT1");*/
                var res = ViewBag.tempD;
                res["TicketNum"] = TKT4;
                tk.Add(res);
            }
            if (data != null)
            {
                foreach (var item in data)
                {
                    if (item == "SV1")
                    {
                        SV1++;
                        Money += 25000;
                    }
                    if (item == "SV2")
                    {
                        SV2++;
                        Money += 45000;
                    }
                    if (item == "SV3")
                    {
                        SV3++;
                        Money += 60000;
                    }
                    if (item == "SV4")
                    {
                        SV4++;
                        Money += 80000;
                    }
                    if (item == "SV5")
                    {
                        SV5++;
                        Money += 20000;
                    }
                    if (item == "SV6")
                    {
                        SV6++;
                        Money += 55000;
                    }
                }
            }
            if (SV1 != 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetService?id=SV1");
                responseMessage.Wait();

                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    ViewBag.sv1 = JObject.Parse(readTask.Result);
                }
                var res = ViewBag.sv1;
                JObject svtc = new JObject();
                svtc["ServiceID"] = res["ServiceID"];
                svtc["ServiceName"] = res["ServiceName"];
                svtc["ServicePrice"] = res["ServicePrice"];
                svtc["ServiceToCashID"] = "SVTC" + res["ServiceID"];
                svtc["ServiceNum"] = SV1;
                sv.Add(svtc);

                /*SERVICE result = db.SERVICEs.Find("SV1");
                SERVICE_TO_CASH svtc = new SERVICE_TO_CASH();
                svtc.ServiceID = result.ServiceID;
                svtc.ServiceName = result.ServiceName;
                svtc.ServicePrice = result.ServicePrice;
                svtc.ServiceToCashID = "SVTC" + result.ServiceID;
                svtc.ServiceNum = SV1;
                sv.Add(svtc);*/
            }
            if (SV2 != 0)
            {

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetService?id=SV2");
                responseMessage.Wait();

                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    ViewBag.sv2 = JObject.Parse(readTask.Result);
                }
                var res = ViewBag.sv2;
                JObject svtc = new JObject();
                svtc["ServiceID"] = res["ServiceID"];
                svtc["ServiceName"] = res["ServiceName"];
                svtc["ServicePrice"] = res["ServicePrice"];
                svtc["ServiceToCashID"] = "SVTC" + res["ServiceID"];
                svtc["ServiceNum"] = SV2;
                sv.Add(svtc);
                /*
                SERVICE result = db.SERVICEs.Find("SV2");
                SERVICE_TO_CASH svtc = new SERVICE_TO_CASH();
                svtc.ServiceID = result.ServiceID;
                svtc.ServiceName = result.ServiceName;
                svtc.ServicePrice = result.ServicePrice;
                svtc.ServiceToCashID = "SVTC" + result.ServiceID;
                svtc.ServiceNum = SV2;
                sv.Add(svtc);*/
            }
            if (SV3 != 0)
            {

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetService?id=SV3");
                responseMessage.Wait();

                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    ViewBag.sv3 = JObject.Parse(readTask.Result);
                }
                var res = ViewBag.sv3;
                JObject svtc = new JObject();
                svtc["ServiceID"] = res["ServiceID"];
                svtc["ServiceName"] = res["ServiceName"];
                svtc["ServicePrice"] = res["ServicePrice"];
                svtc["ServiceToCashID"] = "SVTC" + res["ServiceID"];
                svtc["ServiceNum"] = SV3;
                sv.Add(svtc);
                /*SERVICE result = db.SERVICEs.Find("SV3");
                SERVICE_TO_CASH svtc = new SERVICE_TO_CASH();
                svtc.ServiceID = result.ServiceID;
                svtc.ServiceName = result.ServiceName;
                svtc.ServicePrice = result.ServicePrice;
                svtc.ServiceToCashID = "SVTC" + result.ServiceID;
                svtc.ServiceNum = SV3;
                sv.Add(svtc);*/
            }
            if (SV4 != 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetService?id=SV4");
                responseMessage.Wait();

                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    ViewBag.sv4 = JObject.Parse(readTask.Result);
                }
                var res = ViewBag.sv4;
                JObject svtc = new JObject();
                svtc["ServiceID"] = res["ServiceID"];
                svtc["ServiceName"] = res["ServiceName"];
                svtc["ServicePrice"] = res["ServicePrice"];
                svtc["ServiceToCashID"] = "SVTC" + res["ServiceID"];
                svtc["ServiceNum"] = SV4;
                sv.Add(svtc);
                /*SERVICE result = db.SERVICEs.Find("SV4");
                SERVICE_TO_CASH svtc = new SERVICE_TO_CASH();
                svtc.ServiceID = result.ServiceID;
                svtc.ServiceName = result.ServiceName;
                svtc.ServicePrice = result.ServicePrice;
                svtc.ServiceToCashID = "SVTC" + result.ServiceID;
                svtc.ServiceNum = SV4;
                sv.Add(svtc);*/
            }
            if (SV5 != 0)
            {

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetService?id=SV5");
                responseMessage.Wait();

                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    ViewBag.sv5 = JObject.Parse(readTask.Result);
                }
                var res = ViewBag.sv5;
                JObject svtc = new JObject();
                svtc["ServiceID"] = res["ServiceID"];
                svtc["ServiceName"] = res["ServiceName"];
                svtc["ServicePrice"] = res["ServicePrice"];
                svtc["ServiceToCashID"] = "SVTC" + res["ServiceID"];
                svtc["ServiceNum"] = SV5;
                sv.Add(svtc);
                /*SERVICE result = db.SERVICEs.Find("SV5");
                SERVICE_TO_CASH svtc = new SERVICE_TO_CASH();
                svtc.ServiceID = result.ServiceID;
                svtc.ServiceName = result.ServiceName;
                svtc.ServicePrice = result.ServicePrice;
                svtc.ServiceToCashID = "SVTC" + result.ServiceID;
                svtc.ServiceNum = SV5;
                sv.Add(svtc);*/
            }
            if (SV6 != 0)
            {

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetService?id=SV6");
                responseMessage.Wait();

                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    ViewBag.sv6 = JObject.Parse(readTask.Result);
                }
                var res = ViewBag.sv6;
                JObject svtc = new JObject();
                svtc["ServiceID"] = res["ServiceID"];
                svtc["ServiceName"] = res["ServiceName"];
                svtc["ServicePrice"] = res["ServicePrice"];
                svtc["ServiceToCashID"] = "SVTC" + res["ServiceID"];
                svtc["ServiceNum"] = SV6;
                sv.Add(svtc);
                /*SERVICE result = db.SERVICEs.Find("SV6");
                SERVICE_TO_CASH svtc = new SERVICE_TO_CASH();
                svtc.ServiceID = result.ServiceID;
                svtc.ServiceName = result.ServiceName;
                svtc.ServicePrice = result.ServicePrice;
                svtc.ServiceToCashID = "SVTC" + result.ServiceID;
                svtc.ServiceNum = SV6;
                sv.Add(svtc);*/
            }
            ViewBag.arr_ticket = tk;
            ViewBag.service = sv;
            if (voucher["CodeID"] == null)
            {
                ViewBag.Money = Money / 1000;
            }
            else
            {
                ViewBag.voucher = voucher;
                ViewBag.Money = Money * (100 - Int32.Parse(voucher["DiscountNumber"].ToString())) / 100000;
            }
            ViewBag.seat_arr = seat_arr;
            return PartialView();
        }/*
        public JsonResult CheckVoucher(string vc)
        {
            string data = "";
            var result = db.DISCOUNT_CODE.Find(vc);
            if (result != null)
            {
                if (result.State == 0)
                {
                    voucher = result;
                    data = "ok";
                }
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }*/
        public JsonResult Refresh()
        {
            Money = 0;
            TKT1 = 0; TKT2 = 0; TKT3 = 0; TKT4 = 0; SV1 = 0; SV2 = 0; SV3 = 0; SV4 = 0; SV5 = 0; SV6 = 0;
            /*DISCOUNT_CODE voucher = new DISCOUNT_CODE();*/
            voucher.RemoveAll();
            tk.Clear();
            sv.Clear();
            if(arr_ticket != null) arr_ticket.Clear();
            if(seat_arr != null) seat_arr.Clear();
            ViewBag.arr_ticket = null;
            ViewBag.service = null;
            ViewBag.Money = null;
            /*arr_ticket = null; seat_arr = null;*/
            /*tk = null;
            sv = null;*/
            return Json("Refresh", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Order()
        {
            //insert into ticket, seat, service to cash, bill
            string userid = Session["UserID"].ToString();
            if (userid != "")
            {
                string TicketSession = "TKS" + DateTime1;
                string ServiceSession = "SVS" + DateTime1;
                foreach (var item in seat_arr)
                {
                    JObject tk = new JObject();
                    JObject s = new JObject();
                    /*TICKET tk = new TICKET();
                    SEAT s = new SEAT();*/
                    //seat
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var responseMessage = client.GetAsync("GetMaxSeatID");
                    responseMessage.Wait();

                    var result = responseMessage.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        ViewBag.tema = JObject.Parse(readTask.Result);
                    }
                    string SeatID = ViewBag.tema["SeatID"];
                    s["SeatID"] = "SEAT";
                    for (int i = 1; i <= (SeatID.Length - (Int32.Parse(SeatID.Substring(4, SeatID.Length - 4))).ToString().Length) - 4; i++)
                    {
                        s["SeatID"] += "0";
                    }
                    s["SeatID"] = s["SeatID"] + (Int32.Parse(SeatID.Substring(4, SeatID.Length - 4)) + 1).ToString();
                    s["SeatName"] = item;

                    HttpClient client1 = new HttpClient();
                    client1.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                    client1.DefaultRequestHeaders.Accept.Clear();
                    client1.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    responseMessage = client1.GetAsync("FindMovieTime?id=" + MVT);
                    responseMessage.Wait();

                    result = responseMessage.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        ViewBag.temb = JObject.Parse(readTask.Result);
                    }
                    s["MovieTimeID"] = ViewBag.temb["MovieTimeID"];
                    /*_user.Add("UserPassword", GetMD5(pass)); //
                    _user.Add("Username", name);*/
                    HttpClient client2 = new HttpClient();
                    client2.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                    client2.DefaultRequestHeaders.Accept.Clear();
                    client2.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    responseMessage = client2.PostAsync("AddSeat", new StringContent(s.ToString(), Encoding.UTF8, "application/json"));
                    responseMessage.Wait();
                    /*                    db.SEATs.Add(s);
                                        db.SaveChanges();*/
                    //ticket

                    HttpClient client33 = new HttpClient();
                    client33.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                    client33.DefaultRequestHeaders.Accept.Clear();
                    client33.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    responseMessage = client33.GetAsync("GetMaxTicketID");
                    responseMessage.Wait();

                    result = responseMessage.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        ViewBag.temc = JObject.Parse(readTask.Result);
                    }
                    /*string Ticket = db.Database.SqlQuery<String>("exec GetMaxTicketID").ToList()[0];*/
                    string Ticket = ViewBag.temc["TicketID"];
                    tk["TicketID"] = "TK";
                    for (int i = 1; i <= (Ticket.Length - (Int32.Parse(Ticket.Substring(2, Ticket.Length - 2))).ToString().Length) - 2; i++)
                    {
                        tk["TicketID"] += "0";
                    }
                    tk["TicketID"] = tk["TicketID"] + (Int32.Parse(Ticket.Substring(2, Ticket.Length - 2)) + 1).ToString();
                    tk["SeatID"] = s["SeatID"];
                    tk["TicketSession"] = TicketSession;
                    tk["TicketType"] = arr_ticket[item.IndexOf(item)];
                    HttpClient client4 = new HttpClient();
                    client4.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                    client4.DefaultRequestHeaders.Accept.Clear();
                    client4.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    responseMessage = client4.PostAsync("AddTicket", new StringContent(s.ToString(), Encoding.UTF8, "application/json"));
                    responseMessage.Wait();
                    /*db.TICKETs.Add(tk);


                    db.SaveChanges();*/

                }
                foreach (var item in all_sv)
                {
                    JObject svc = new JObject();
                    /*SERVICE_TO_CASH svc = new SERVICE_TO_CASH();*/

                    HttpClient client34 = new HttpClient();
                    client34.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                    client34.DefaultRequestHeaders.Accept.Clear();
                    client34.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var responseMessage = client34.GetAsync("GetMaxSVCID");
                    responseMessage.Wait();

                    var result = responseMessage.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        ViewBag.temd = JObject.Parse(readTask.Result);
                    }
                    string SVC = ViewBag.temd["ServiceToCashID"];
                    /*string SVC = db.Database.SqlQuery<String>("exec GetMaxSVCID").ToList()[0];*/
                    svc["ServiceToCashID"] = "SVTC";
                    for (int i = 1; i <= (SVC.Length - (Int32.Parse(SVC.Substring(4, SVC.Length - 4))).ToString().Length) - 4; i++)
                    {
                        svc["ServiceToCashID"] += "0";
                    }
                    svc["ServiceToCashID"] = svc["ServiceToCashID"] + (Int32.Parse(SVC.Substring(4, SVC.Length - 4)) + 1).ToString();

                    HttpClient client4 = new HttpClient();
                    client4.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                    client4.DefaultRequestHeaders.Accept.Clear();
                    client4.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    responseMessage = client4.GetAsync("FindServiceName?id=" + item);
                    responseMessage.Wait();

                    result = responseMessage.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        ViewBag.teme = JObject.Parse(readTask.Result);
                    }
                    svc["ServiceName"] = ViewBag.teme["ServiceName"];
                    /*svc.ServiceName = db.SERVICEs.Find(item).ServiceName;*/
                    svc["ServiceSession"] = ServiceSession;
                    svc["ServiceID"] = item;

                    HttpClient client5 = new HttpClient();
                    client5.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                    client5.DefaultRequestHeaders.Accept.Clear();
                    client5.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    responseMessage = client5.GetAsync("FindServicePrice?id=" + item);
                    responseMessage.Wait();

                    result = responseMessage.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        ViewBag.temf = JObject.Parse(readTask.Result);
                    }
                    svc["ServicePrice"] = ViewBag.temf["ServicePrice"];
                    /*svc.ServicePrice = db.SERVICEs.Find(item).ServicePrice;*/

                    HttpClient client66 = new HttpClient();
                    client66.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                    client66.DefaultRequestHeaders.Accept.Clear();
                    client66.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    responseMessage = client66.PostAsync("AddServiceToCash", new StringContent(svc.ToString(), Encoding.UTF8, "application/json"));
                    responseMessage.Wait();
                    /*db.SERVICE_TO_CASH.Add(svc);
                    db.SaveChanges();*/
                }
                /*BILL b = new BILL();*/
                JObject b = new JObject();
                HttpClient client3 = new HttpClient();
                client3.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                client3.DefaultRequestHeaders.Accept.Clear();
                client3.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage3 = client3.GetAsync("GetMaxBillID");
                responseMessage3.Wait();

                var result3 = responseMessage3.Result;
                if (result3.IsSuccessStatusCode)
                {
                    var readTask = result3.Content.ReadAsStringAsync();
                    readTask.Wait();
                    ViewBag.temg = JObject.Parse(readTask.Result);
                }
                string BillID = ViewBag.temg["BillID"];
                /*string BillID = db.Database.SqlQuery<String>("exec GetMaxBillID").ToList()[0];*/
                b["BillID"] = "B";
                for (int i = 1; i <= (BillID.Length - (Int32.Parse(BillID.Substring(1, BillID.Length - 1))).ToString().Length) - 1; i++)
                {
                    b["BillID"] += "0";
                }
                b["BillID"] = b["BillID"] + (Int32.Parse(BillID.Substring(1, BillID.Length - 1)) + 1).ToString();
                b["UserID"] = userid;
                b["ServiceSession"] = ServiceSession;
                b["TicketSession"] = TicketSession;
                b["CodeID"] = voucher["CodeID"];

                HttpClient client6 = new HttpClient();
                client6.BaseAddress = new Uri("http://localhost:8085/api/TicketAPI/");
                client6.DefaultRequestHeaders.Accept.Clear();
                client6.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessagee = client6.PostAsync("AddBill", new StringContent(b.ToString(), Encoding.UTF8, "application/json"));
                responseMessagee.Wait();
                /*db.BILLs.Add(b);
                db.SaveChanges();*/
                return Redirect("/HomePage");
            }
            else
            {
                return Redirect("/HomePage");
            }
        }
    }
}