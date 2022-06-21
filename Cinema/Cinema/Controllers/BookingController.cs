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
        // GET: Booking
        public ActionResult Booking()
        {
            return View();
        }
        /*public JsonResult Location(string location)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Movies/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetMOVIE/MV0002");
            responseMessage.Wait();

            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                ViewBag.detail = JObject.Parse(readTask.Result);
            }

            var listcinema = db.Database.SqlQuery<CINEMA>($"exec GetListCinemaFromLocation N'{location}'").ToList();
            ViewBag.checklocation = location;
            ViewBag.listcinema = listcinema;
            ViewBag.checkcinema = "none";
            return Json(listcinema, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Cinema(string cinema)
        {
            var listmovie = db.Database.SqlQuery<MovieItem>($"exec GetListFilmFromCinema N'{cinema}','2021-10-25'").ToList();
            ViewBag.listmovie = listmovie;

            ViewBag.checkcinema = cinema;
            return Json(listmovie, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ShowTime(string movie, string cinemaname)
        {
            var type1 = db.Database.SqlQuery<ShowTime>($"exec GetList2DRoomFromFilm N'{cinemaname}',N'{movie}', N'2021-10-25'").ToList();
            var type2 = db.Database.SqlQuery<ShowTime>($"exec GetList3DRoomFromFilm N'{cinemaname}',N'{movie}', N'2021-10-25'").ToList();
            var type3 = db.Database.SqlQuery<ShowTime>($"exec GetList4DRoomFromFilm N'{cinemaname}',N'{movie}', N'2021-10-25'").ToList();
            foreach (var item in type1)
            {
                item.screentype = "2D";
            }
            foreach (var item in type2)
            {
                item.screentype = "3D";
            }
            foreach (var item in type3)
            {
                item.screentype = "4Dx";
            }
            type1.AddRange(type2);
            type1.AddRange(type3);
            return Json(type1, JsonRequestBehavior.AllowGet);
        }*/
    }
}