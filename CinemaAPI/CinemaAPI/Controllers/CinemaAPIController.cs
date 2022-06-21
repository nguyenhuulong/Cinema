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
    public class CinemaAPIController : ApiController
    {
        private CinemaDB db = new CinemaDB();

        [HttpGet]
        [ResponseType(typeof(CINEMA))]
        public async Task<IHttpActionResult> GetListCinemaFromName(string cinema)
        {
            var cinemas = db.Database.SqlQuery<CINEMA>($"exec GetListCinemaFromName N'{cinema}'");
            await cinemas.ToListAsync();
            if (cinemas == null)
            {
                return NotFound();
            }
            return Json(cinemas);
        }

        [HttpGet]
        [ResponseType(typeof(MovieItem))]
        public async Task<IHttpActionResult> GetListFilmFromCinema(string cinema, DateTime daytime)
        {
            var films = db.Database.SqlQuery<MovieItem>($"exec GetListFilmFromCinema N'{cinema}',N'{daytime}'");
            await films.ToListAsync();
            if (films == null)
            {
                return NotFound();
            }
            return Json(films);
        }

        [HttpGet]
        [ResponseType(typeof(ShowTime))]
        public async Task<IHttpActionResult> GetList2DRoomFromFilm(string cinema, string movie, DateTime daytime)
        {
            var rooms = db.Database.SqlQuery<ShowTime>($"exec GetList2DRoomFromFilm N'{cinema}',N'{movie}',N'{daytime}'");
            await rooms.ToListAsync();
            if (rooms == null)
            {
                return NotFound();
            }
            return Json(rooms);
        }

        [HttpGet]
        [ResponseType(typeof(ShowTime))]
        public async Task<IHttpActionResult> GetList3DRoomFromFilm(string cinema, string movie, DateTime daytime)
        {
            var rooms = db.Database.SqlQuery<ShowTime>($"exec GetList3DRoomFromFilm N'{cinema}',N'{movie}',N'{daytime}'");
            await rooms.ToListAsync();
            if (rooms == null)
            {
                return NotFound();
            }
            return Json(rooms);
        }

        [HttpGet]
        [ResponseType(typeof(ShowTime))]
        public async Task<IHttpActionResult> GetList4DRoomFromFilm(string cinema, string movie, DateTime daytime)
        {
            var rooms = db.Database.SqlQuery<ShowTime>($"exec GetList4DRoomFromFilm N'{cinema}',N'{movie}',N'{daytime}'");
            await rooms.ToListAsync();
            if (rooms == null)
            {
                return NotFound();
            }
            return Json(rooms);
        }

        [HttpGet]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> QuantityLeft(string movie, string room, string showtime)
        {
            var rooms = db.Database.SqlQuery<int>($"exec QuantityLeft N'{movie}',N'{room}',N'{showtime}'");
            await rooms.ToListAsync();
            if (rooms == null)
            {
                return NotFound();
            }
            return Json(rooms);
        }

        [HttpGet]
        [ResponseType(typeof(CINEMA_IMAGE))]
        public async Task<IHttpActionResult> GetListImgFromCinema(string cinema)
        {
            var images = db.Database.SqlQuery<CINEMA_IMAGE>($"exec GetListImgFromCinema N'{cinema}'");
            await images.ToListAsync();
            if (images == null)
            {
                return NotFound();
            }
            return Json(images);
        }
    }
}
