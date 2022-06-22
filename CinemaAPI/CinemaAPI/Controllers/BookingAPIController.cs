
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;

using System.Web.Http.Description;

namespace CinemaAPI.Controllers
{

    public class BookingAPIController : ApiController
    {
        CinemaDB db = new CinemaDB();
        //GetListCinemaFromFilm
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{MovieName}")]
        public async Task<IHttpActionResult> GetListCinemaFromFilm([FromRoute] string MovieName)
        {
            var cinema = db.Database.SqlQuery<CinemaItem1>($"exec GetListCinemaFromFilm N'{MovieName}', N'2021-10-25'");
            await cinema.ToListAsync();
            if (cinema == null)
            {
                return NotFound();
            }

            return Json(cinema);
        }
        //GetListCinemaFromLocation
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{MovieName}")]
        public async Task<IHttpActionResult> GetListCinemaFromLocation([FromRoute] string Location)
        {
            var cinema = db.Database.SqlQuery<CINEMA>($"exec GetListCinemaFromLocation   N'{Location}', N'2021-10-25'");
            await cinema.ToListAsync();
            if (cinema == null)
            {
                return NotFound();
            }

            return Json(cinema);
        }


        //GetListFilmFromCinema
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{MovieName}")]
        public async Task<IHttpActionResult> GetListFilmFromCinema([FromRoute] string CinemaName)
        {
            var cinema = db.Database.SqlQuery<MovieItem>($"exec GetListFilmFromCinema   N'{CinemaName}', N'2021-10-25'");
            await cinema.ToListAsync();
            if (cinema == null)
            {
                return NotFound();
            }

            return Json(cinema);
        }

        //GetListCinemaFromFilmAndLocation
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{MovieName}/{Location}")]
        public async Task<IHttpActionResult> GetListCinemaFromFilmAndLocation([FromRoute] string MovieName, [FromRoute] string Location)
        {
            var cinema = db.Database.SqlQuery<CinemaItem1>($"exec GetListCinemaFromFilmAndLocation N'{MovieName}', N'2021-10-25', N'{Location}'");
            await cinema.ToListAsync();
            if (cinema == null)
            {
                return NotFound();
            }

            return Json(cinema);
        }


        //GetQuantityLeft

        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{MovieName}/{RoomID}/{ShowTime}")]
        public async Task<IHttpActionResult> QuntityLeft([FromRoute] string MovieName, [FromRoute] string RoomID, [FromRoute] string ShowTime)
        {
            var cinema = db.Database.SqlQuery<int>($"exec QuantityLeft N'{MovieName}', N'{RoomID}', N'{ShowTime}'");
            await cinema.ToListAsync();
            if (cinema == null)
            {
                return NotFound();
            }

            return Json(cinema);
        }
        //GetList2DRoom
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{CinemaName}/{MovieName}")]
        public async Task<IHttpActionResult> GetList2DRoomFromFilm([FromRoute] string CinemaName, [FromRoute] string MovieName)
        {
            var cinema = db.Database.SqlQuery<ShowTime>($"exec GetList2DRoomFromFilm N'{CinemaName}', N'2021-10-25', N'{MovieName}'");
            await cinema.ToListAsync();
            if (cinema == null)
            {
                return NotFound();
            }

            return Json(cinema);
        }

        //GetList3DRoom
        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{CinemaName}/{MovieName}")]
        public async Task<IHttpActionResult> GetList3DRoomFromFilm([FromRoute] string CinemaName, [FromRoute] string MovieName)
        {
            var cinema = db.Database.SqlQuery<ShowTime>($"exec GetList3DRoomFromFilm N'{CinemaName}', N'2021-10-25', N'{MovieName}'");
            await cinema.ToListAsync();
            if (cinema == null)
            {
                return NotFound();
            }

            return Json(cinema);
        }

        //GetList4DRoom

        [System.Web.Http.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{CinemaName}/{MovieName}")]
        public async Task<IHttpActionResult> GetList4DRoomFromFilm([FromRoute] string CinemaName, [FromRoute] string MovieName)
        {
            var cinema = db.Database.SqlQuery<ShowTime>($"exec GetList4DRoomFromFilm N'{CinemaName}', N'2021-10-25', N'{MovieName}'");
            await cinema.ToListAsync();
            if (cinema == null)
            {
                return NotFound();
            }

            return Json(cinema);
        }
    }
}