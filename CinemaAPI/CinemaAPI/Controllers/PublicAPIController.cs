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
    public class PublicAPIController : ApiController
    {
        private CinemaDB db = new CinemaDB();

        

        [HttpGet]
        [ResponseType(typeof(AD))]
        public async Task<IHttpActionResult> GetListSlide()
        {
            var ads = db.Database.SqlQuery<AD>("exec GetListSlide");
            await ads.ToListAsync();
            if (ads == null)
            {
                return NotFound();
            }

            return Json(ads);
        }

        [HttpGet]
        [ResponseType(typeof(CINEMA))]
        public async Task<IHttpActionResult> GetListCinemaFromLocation(string location)
        {
            var cinemas = db.Database.SqlQuery<CINEMA>($"exec GetListCinemaFromLocation N'{location}'");
            await cinemas.ToListAsync();
            if (cinemas == null)
            {
                return NotFound();
            }
            return Json(cinemas);
        }
    }
}