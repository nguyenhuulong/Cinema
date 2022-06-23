using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CinemaAPI.Models;


namespace CinemaAPI.Controllers
{
    public class BookingController : ApiController
    {
        private CinemaDB db = new CinemaDB();

       
        public IQueryable<CINEMA_LOCATION> GetLC()
        {
            return db.CINEMA_LOCATION;
        }

        [HttpGet]
        [ResponseType(typeof(CinemaItem1))]
        public async Task<IHttpActionResult> GetListCinemaFromFilm(string name, string date)
        {
            var cinema = db.Database.SqlQuery<CinemaItem1>($"exec GetListCinemaFromFilm N'{name}', N'{date}'");
            await cinema.ToListAsync();
            if (cinema == null)
            {
                return NotFound();
            }
            return Json(cinema);
        }

        [HttpGet]
        [ResponseType(typeof(CinemaItem1))]
        public async Task<IHttpActionResult> GetListCinemaFromFilmAndLocation(string name, DateTime date, string location)
        {
            var cinema = db.Database.SqlQuery<CinemaItem1>($"exec GetListCinemaFromFilmAndLocation N'{name}', N'{date}', N'{location}'");
            await cinema.ToListAsync();
            if (cinema == null)
            {
                return NotFound();
            }
            return Json(cinema);
        }
    }
}
